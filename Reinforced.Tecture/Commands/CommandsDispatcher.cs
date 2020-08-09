using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands.Exact;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Dispatches commands queue and implements enqueued actions
    /// </summary>
    class CommandsDispatcher
    {
        private readonly ChannelMultiplexer _mx;

        internal CommandsDispatcher(ChannelMultiplexer mx)
        {
            _mx = mx;
        }

        protected virtual void Save(IEnumerable<string> channels)
        {
            foreach (var sideEffectSaver in _mx.GetSavers(channels))
            {
                sideEffectSaver.SaveInternal();
            }
        }

        protected virtual async Task SaveAsync(IEnumerable<string> channels)
        {
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
                if (queue.HasEffects) DispatchInternal(queue.GetEffects(),usedChannels);

                Save(usedChannels);

                postSave.Run();
            } while (queue.HasEffects);
        }

        public async Task DispatchAsync(Pipeline queue, ActionsQueue postSave)
        {
            do
            {
                HashSet<string> usedChannels = new HashSet<string>();
                if (queue.HasEffects) await DispatchInternalAsync(queue.GetEffects(), usedChannels);

                await SaveAsync(usedChannels);

                await postSave.RunAsync();
            } while (queue.HasEffects);
        }

        private CommandRunner GetRunner(CommandBase command)
        {
            return _mx.GetRunner(command);
        }

        private void RunCommand(CommandBase command)
        {
            var r = GetRunner(command);
            r.RunInternal(command);
        }

        private Task RunCommandAsync(CommandBase command)
        {
            var r = GetRunner(command);
            return r.RunInternalAsync(command);
        }

        protected virtual void DispatchInternal(IEnumerable<CommandBase> commands, HashSet<string> usedChannels)
        {
            foreach (var commandBase in commands)
            {
                if (!(commandBase is Comment))
                {
                    if (!usedChannels.Contains(commandBase.ChannelId)) usedChannels.Add(commandBase.ChannelId);
                    RunCommand(commandBase);
                }
            }
        }

        protected virtual async Task DispatchInternalAsync(IEnumerable<CommandBase> commands, HashSet<string> usedChannels)
        {
            foreach (var commandBase in commands)
            {
                if (!(commandBase is Comment))
                {
                    if (!usedChannels.Contains(commandBase.ChannelId)) usedChannels.Add(commandBase.ChannelId);
                    var r = RunCommandAsync((commandBase));
                    if (r != null) await r;
                }
            }
        }
    }
}
