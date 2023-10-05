using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Dispatches commands queue and implements enqueued actions
    /// </summary>
    sealed class CommandsDispatcher
    {
        private readonly ChannelMultiplexer _mx;
        private readonly TraceCollector _traceCollector;
        private readonly TransactionManager _transactionManager;

        internal CommandsDispatcher(ChannelMultiplexer mx, TraceCollector traceCollector,
            TransactionManager transactionManager)
        {
            _mx = mx;
            _traceCollector = traceCollector;
            _transactionManager = transactionManager;
        }

        private void Save(IEnumerable<string> channels)
        {
            var trans = _transactionManager.GetSaveTransactions(channels, false);
            var aggregate = new List<Exception>();
            try
            {
                foreach (var commandAspect in _mx.GetCommandAspectsForChannels(channels))
                {
                    commandAspect.SaveInternal();
                    trans[commandAspect._channel.FullName].Commit();
                }
            }
            catch (Exception e)
            {
                aggregate.Add(e);
            }
            finally
            {
                foreach (var channelTransaction in trans)
                {
                    try
                    {
                        channelTransaction.Value.Dispose();
                    }
                    catch (Exception e)
                    {
                        aggregate.Add(e);
                    }
                }
            }

            if (aggregate.Count > 0) throw new AggregateException(aggregate);
        }

        private async Task SaveAsync(IEnumerable<string> channels, CancellationToken token = default)
        {
            var trans = _transactionManager.GetSaveTransactions(channels, true);
            var aggregate = new List<Exception>();
            try
            {
                foreach (var commandAspect in _mx.GetCommandAspectsForChannels(channels))
                {
                    await commandAspect.SaveInternalAsync(token);
                    trans[commandAspect._channel.FullName].Commit();
                }
            }
            catch (Exception e)
            {
                aggregate.Add(e);
            }
            finally
            {
                foreach (var channelTransaction in trans)
                {
                    try
                    {
                        channelTransaction.Value.Dispose();
                    }
                    catch (Exception e)
                    {
                        aggregate.Add(e);
                    }
                }
            }

            if (aggregate.Count > 0) throw new AggregateException(aggregate);
        }


        public void Dispatch(Pipeline queue, ActionsQueue postSave)
        {
            do
            {
                var usedChannels = new HashSet<string>();
                if (queue.HasEffects)
                {
                    RunCommands(queue.GetEffects(), usedChannels);
                    Stopwatch sw = null;
                    Exception thrown = null;

                    try
                    {
                        if (_traceCollector != null && _traceCollector.Profiling)
                        {
                            sw = new Stopwatch();
                            sw.Start();
                        }

                        Save(usedChannels);
                    }
                    catch (Exception ex)
                    {
                        thrown = ex;
                        throw new TectureSaveException(ex);
                    }
                    finally
                    {
                        if (sw != null) sw.Stop();
                        _traceCollector?.Save(sw?.Elapsed ?? TimeSpan.Zero, thrown);
                    }
                }
                else
                {
                    _traceCollector?.Save(TimeSpan.Zero, null);
                }

                postSave?.Run();
            } while (queue.HasEffects);
        }

        public async Task DispatchAsync(Pipeline queue, ActionsQueue postSave, CancellationToken token = default)
        {
            do
            {
                var usedChannels = new HashSet<string>();
                if (queue.HasEffects)
                {
                    await RunCommandsAsync(queue.GetEffects(), usedChannels, token);

                    Stopwatch sw = null;
                    Exception thrown = null;
                    if (_traceCollector != null && _traceCollector.Profiling)
                    {
                        sw = new Stopwatch();
                        sw.Start();
                    }

                    try
                    {
                        await SaveAsync(usedChannels, token);
                    }
                    catch (Exception ex)
                    {
                        thrown = ex;
                        throw new TectureSaveException(ex);
                    }
                    finally
                    {
                        if (sw != null) sw.Stop();
                        _traceCollector?.Save(sw?.Elapsed ?? TimeSpan.Zero, thrown);
                    }
                }
                else
                {
                    _traceCollector?.Save(TimeSpan.Zero, null);
                }


                if (postSave != null) await postSave.RunAsync(token);
            } while (queue.HasEffects);
        }

        private void RunCommands(IEnumerable<CommandBase> commands, HashSet<string> usedChannels)
        {
            foreach (var commandBase in commands)
            {
                if (commandBase is ITracingOnly) continue;
                usedChannels.Add(commandBase.ChannelId);
                var r = _mx.GetRunner(commandBase);
                var tran = _transactionManager.GetCommandTransaction(commandBase.ChannelId, commandBase, false);
                Stopwatch sw = null;
                if (_traceCollector != null && _traceCollector.Profiling)
                {
                    sw = new Stopwatch();
                    sw.Start();
                }

                try
                {
                    r.RunInternal(commandBase);
                    commandBase.IsExecuted = true;
                    tran.Commit();
                }
                catch (Exception e)
                {
                    commandBase.Exception = e;
                    throw new TectureCommandRunException(commandBase, e);
                }
                finally
                {
                    tran.Dispose();
                    if (_traceCollector != null && _traceCollector.Profiling)
                    {
                        sw?.Stop();
                        commandBase.TimeTaken = sw?.Elapsed ?? TimeSpan.Zero;
                    }
                }
            }
        }

        private async Task RunCommandsAsync(IEnumerable<CommandBase> commands, HashSet<string> usedChannels,
            CancellationToken token = default)
        {
            foreach (var commandBase in commands)
            {
                if (commandBase is ITracingOnly) continue;
                
                usedChannels.Add(commandBase.ChannelId);
                var r1 = _mx.GetRunner(commandBase);
                ChannelTransaction tran = null;
                Stopwatch sw = null;
                if (_traceCollector != null && _traceCollector.Profiling)
                {
                    sw = new Stopwatch();
                    sw.Start();
                }

                try
                {
                    var r = r1.RunInternalAsync(commandBase, token);
                    if (r != null)
                    {
                        tran = _transactionManager.GetCommandTransaction(commandBase.ChannelId, commandBase, true);
                        await r;
                        commandBase.IsExecuted = true;
                        tran.Commit();
                    }
                }
                catch (Exception e)
                {
                    commandBase.Exception = e;
                    throw new TectureCommandRunException(commandBase, e);
                }
                finally
                {
                    tran?.Dispose();
                    if (_traceCollector != null && _traceCollector.Profiling)
                    {
                        sw.Stop();
                        commandBase.TimeTaken = sw.Elapsed;
                    }
                }
            }
        }
    }
}