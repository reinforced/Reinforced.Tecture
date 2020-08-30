using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Dispatches commands queue and implements enqueued actions
    /// </summary>
    sealed class CommandsDispatcher
    {
        private readonly ChannelMultiplexer _mx;
        private readonly TraceCollector _tc;
        internal CommandsDispatcher(ChannelMultiplexer mx, TraceCollector tc)
        {
            _mx = mx;
            _tc = tc;
        }

        private void Save(IEnumerable<string> channels)
        {
            _tc?.Save();
            foreach (var sideEffectSaver in _mx.GetSavers(channels))
            {
                sideEffectSaver.SaveInternal();
            }
        }

        private async Task SaveAsync(IEnumerable<string> channels)
        {
            _tc?.Save();
            foreach (var sideEffectSaver in _mx.GetSavers(channels))
            {
                await sideEffectSaver.SaveInternalAsync();
            }
        }


        public void Dispatch(Pipeline queue, ActionsQueue postSave)
        {
            do
            {
                HashSet<string> usedChannels = new HashSet<string>();

                if (queue.HasEffects)
                {
                    DispatchInternal(queue.GetEffects(),usedChannels);
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
                    r.RunInternal(commandBase);
                    commandBase.IsExecuted = true;
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
                    var r = r1.RunInternalAsync(commandBase);
                    if (r != null)
                    {
                        await r;
                        commandBase.IsExecuted = true;
                    }
                }
            }
        }
    }
}
