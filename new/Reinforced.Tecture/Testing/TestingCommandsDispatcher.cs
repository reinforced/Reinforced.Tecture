using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Testing.Assumptions;
using Reinforced.Tecture.Testing.Stories;

namespace Reinforced.Tecture.Testing
{
    class TestingCommandsDispatcher : CommandsDispatcher
    {
        private readonly Dictionary<Type, List<AssumptionBase>> _assumptions;
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

        internal TestingCommandsDispatcher(RuntimeMultiplexer mx, Dictionary<Type, List<AssumptionBase>> assumptions) : base(mx)
        {
            _assumptions = assumptions;
        }

        protected override Tuple<CommandRunnerGateway, CommandRunner> GetRunner(CommandBase command)
        {
            var commandType = command.GetType();
            if (_assumptions.ContainsKey(commandType))
            {
                var assumed = _assumptions[commandType];
                var suitable = assumed.FirstOrDefault(x => x.Should(command));
                if (suitable != null)
                {
                    var bs = base.GetRunner(command);
                    suitable.OriginalRunner = bs.Item2;
                    var gateway = new CommandRunnerGateway(commandType, suitable.GetType());
                    return new Tuple<CommandRunnerGateway, CommandRunner>(gateway, suitable);
                }
            }

            return base.GetRunner(command);
        }

        protected override void Save()
        {
            base.Save();
            _story.Enqueue(new SaveCommand());
        }

        protected override async Task SaveAsync()
        {
            await base.SaveAsync();
            _story.Enqueue(new SaveCommand());
        }

        protected override void DispatchInternal(IEnumerable<CommandBase> commands)
        {
            if (!_isStoryActive)
            {
                base.DispatchInternal(commands);
                return;
            }
            var e = commands.ToArray();
            foreach (var cmd in e)
            {
                _story.Enqueue(cmd);
            }
            base.DispatchInternal(e);
        }

        protected override Task DispatchInternalAsync(IEnumerable<CommandBase> commands)
        {
            if (!_isStoryActive) return base.DispatchInternalAsync(commands);
            var e = commands.ToArray();
            foreach (var cmd in e)
            {
                _story.Enqueue(cmd);
            }
            return base.DispatchInternalAsync(e);
        }
    }
}
