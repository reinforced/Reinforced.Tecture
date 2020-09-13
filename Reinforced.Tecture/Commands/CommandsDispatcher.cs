using System;
using System.Collections.Generic;
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
        private readonly TraceCollector _tc;
        private readonly TransactionManager _transactionManager;
        internal CommandsDispatcher(ChannelMultiplexer mx, TraceCollector tc, TransactionManager transactionManager)
        {
            _mx = mx;
            _tc = tc;
            _transactionManager = transactionManager;
        }

        private void Save(IEnumerable<string> channels)
        {
            _tc?.Save();
            var trans = _transactionManager.GetSaveTransactions(channels, false);
            List<Exception> aggregate = new List<Exception>();
            try
            {
                foreach (var sideEffectSaver in _mx.GetSavers(channels))
                {
                    sideEffectSaver.SaveInternal();
                    trans[sideEffectSaver.Channel.FullName].Commit();
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

        private async Task SaveAsync(IEnumerable<string> channels)
        {
            _tc?.Save();
            var trans = _transactionManager.GetSaveTransactions(channels, true);
            List<Exception> aggregate = new List<Exception>();
            try
            {
                foreach (var sideEffectSaver in _mx.GetSavers(channels))
                {
                    await sideEffectSaver.SaveInternalAsync();
                    trans[sideEffectSaver.Channel.FullName].Commit();
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
                HashSet<string> usedChannels = new HashSet<string>();

                if (queue.HasEffects)
                {
                    DispatchInternal(queue.GetEffects(), usedChannels);
                    Save(usedChannels);
                    postSave.Run();
                }


            } while (queue.HasEffects);
        }

        public async Task DispatchAsync(Pipeline queue, ActionsQueue postSave)
        {
            do
            {
                HashSet<string> usedChannels = new HashSet<string>();
                if (queue.HasEffects)
                {
                    await DispatchInternalAsync(queue.GetEffects(), usedChannels);
                    await SaveAsync(usedChannels);
                    await postSave.RunAsync();
                }
            } while (queue.HasEffects);
        }

        private void DispatchInternal(IEnumerable<CommandBase> commands, HashSet<string> usedChannels)
        {
            foreach (var commandBase in commands)
            {
                if (!(commandBase is ITracingOnly))
                {
                    if (!usedChannels.Contains(commandBase.ChannelId)) usedChannels.Add(commandBase.ChannelId);
                    var r = _mx.GetRunner(commandBase);
                    var tran = _transactionManager.GetCommandTransaction(commandBase.ChannelId, commandBase, false);
                    
                    try
                    {
                        r.RunInternal(commandBase);
                        commandBase.IsExecuted = true;
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        throw new TectureCommandRunException(commandBase, e);
                    }
                    finally
                    {
                        tran.Dispose();
                    }
                }
            }
        }

        private async Task DispatchInternalAsync(IEnumerable<CommandBase> commands, HashSet<string> usedChannels)
        {
            foreach (var commandBase in commands)
            {
                if (!(commandBase is ITracingOnly))
                {
                    if (!usedChannels.Contains(commandBase.ChannelId)) usedChannels.Add(commandBase.ChannelId);
                    var r1 = _mx.GetRunner(commandBase);
                    ChannelTransaction tran = null;
                    try
                    {
                        var r = r1.RunInternalAsync(commandBase);
                        if (r != null)
                        {
                            tran = _transactionManager.GetCommandTransaction(commandBase.ChannelId, commandBase, false);
                            await r;
                            commandBase.IsExecuted = true;
                            tran.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new TectureCommandRunException(commandBase, e);
                    }
                    finally
                    {
                        tran?.Dispose();
                    }
                }
            }
        }
    }
}
