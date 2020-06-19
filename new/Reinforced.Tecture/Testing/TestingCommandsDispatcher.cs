using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Assumptions;
using Reinforced.Tecture.Testing.Stories;

namespace Reinforced.Tecture.Testing
{
    class TestingCommandsDispatcher : CommandsDispatcher
    {
        private readonly Dictionary<Type, List<IAssumption>> _assumptions;
        private Queue<CommandBase> _story;
        private bool _isStoryActive = false;
        internal void BeginStory()
        {
            _isStoryActive = true;
            if (_story != null) _story.Clear();
            _story = new Queue<CommandBase>();
        }

        internal StorageStory EndStory(TestingEnvironment env)
        {
            _isStoryActive = false;
            return new StorageStory(_story, env);
        }
        
        protected override CommandRunner GetRunner(CommandBase command)
        {
            var commandType = command.GetType();
            if (_assumptions.ContainsKey(commandType))
            {
                var assumed = _assumptions[commandType];
                var suitable = assumed.FirstOrDefault(x => x.Should(command));
                if (suitable != null)
                {
                    var bs = base.GetRunner(command);
                    suitable.OriginalRunner = bs;
                   
                    return (CommandRunner) suitable;
                }
            }

            return base.GetRunner(command);
        }

        protected override void Save(IEnumerable<string> channels)
        {
            base.Save(channels);
            _story.Enqueue(new SaveCommand());
        }

        protected override async Task SaveAsync(IEnumerable<string> channels)
        {
            await base.SaveAsync(channels);
            _story.Enqueue(new SaveCommand());
        }

        protected override void DispatchInternal(IEnumerable<CommandBase> commands,HashSet<string> channels)
        {
            if (!_isStoryActive)
            {
                base.DispatchInternal(commands, channels);
                return;
            }
            var e = commands.ToArray();
            foreach (var cmd in e)
            {
                _story.Enqueue(cmd);
            }
            base.DispatchInternal(e, channels);
        }

        protected override Task DispatchInternalAsync(IEnumerable<CommandBase> commands, HashSet<string> channels)
        {
            if (!_isStoryActive) return base.DispatchInternalAsync(commands, channels);
            var e = commands.ToArray();
            foreach (var cmd in e)
            {
                _story.Enqueue(cmd);
            }
            return base.DispatchInternalAsync(e, channels);
        }

        internal TestingCommandsDispatcher(ChannelMultiplexer mx, Dictionary<Type, List<IAssumption>> assumptions) : base(mx)
        {
            _assumptions = assumptions;
        }
    }
}
