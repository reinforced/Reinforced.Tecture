using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands.Exact;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Integrate;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Dispatches commands queue and implements enqueued actions
    /// </summary>
    class CommandsDispatcher
    {
        private readonly RuntimeMultiplexer _mx;

        protected class CommandRunnerGateway
        {
            private readonly MethodInfo _run;
            private readonly MethodInfo _runAsync;

            public CommandRunnerGateway(Type effectType, Type runnerType)
            {
                _run = runnerType.GetRuntimeMethod(nameof(ICommandRunner<CommandBase>.Run), new[] { effectType });
                _runAsync = runnerType.GetRuntimeMethod(nameof(ICommandRunner<CommandBase>.RunAsync), new[] { effectType });

            }

            public void Run(ICommandRunner runner, CommandBase effect)
            {
                _run.Invoke(runner, new[] { effect });
            }

            public async Task RunAsync(ICommandRunner runner, CommandBase effect)
            {
                var r = (Task)_runAsync.Invoke(runner, new[] { effect });
                await r;
            }
        }

        private readonly Dictionary<Type, CommandRunnerGateway> _runnerGateways = new Dictionary<Type, CommandRunnerGateway>();

        internal CommandsDispatcher(RuntimeMultiplexer mx)
        {
            _mx = mx;
            _savers = new Lazy<ISaver[]>(() => _mx.GetSavers());
        }


        private readonly Lazy<ISaver[]> _savers;

        protected virtual void Save()
        {
            foreach (var sideEffectSaver in _savers.Value)
            {
                sideEffectSaver.Save();
            }
        }

        protected virtual async Task SaveAsync()
        {
            foreach (var sideEffectSaver in _savers.Value)
            {
                await sideEffectSaver.SaveAsync();
            }
        }


        public virtual void Dispatch(Pipeline queue, ActionsQueue postSave)
        {
            do
            {
                if (queue.HasEffects) DispatchInternal(queue.GetEffects());

                Save();

                postSave.Run();
            } while (queue.HasEffects);
        }

        public virtual async Task DispatchAsync(Pipeline queue, ActionsQueue postSave)
        {
            do
            {
                if (queue.HasEffects) await DispatchInternalAsync(queue.GetEffects());

                await SaveAsync();

                await postSave.RunAsync();
            } while (queue.HasEffects);
        }

        protected virtual Tuple<CommandRunnerGateway, ICommandRunner> GetRunner(CommandBase command)
        {
            var commandType = command.GetType();
            var runner = _mx.GetRunner(command);
            if (runner == null) 
                throw new TectureException($"Runner for command {command} was not found");
            var runnerType = runner.GetType();

            if (!_runnerGateways.ContainsKey(runnerType))
            {
                _runnerGateways[runnerType] =
                    new CommandRunnerGateway(commandType, runnerType);
            }
            return new Tuple<CommandRunnerGateway, ICommandRunner>(_runnerGateways[runnerType], runner);
        }

        protected virtual void RunCommand(CommandBase effect)
        {
            var r = GetRunner(effect);
            r.Item1.Run(r.Item2, effect);
        }

        protected virtual Task RunCommandAsync(CommandBase effect)
        {
            var r = GetRunner(effect);
            return r.Item1.RunAsync(r.Item2, effect);
        }

        protected virtual void DispatchInternal(IEnumerable<CommandBase> commands)
        {
            foreach (var commandBase in commands)
            {
                if (!(commandBase is CommentCommand)) RunCommand(commandBase);
            }
        }

        protected virtual async Task DispatchInternalAsync(IEnumerable<CommandBase> commands)
        {
            foreach (var commandBase in commands)
            {
                if (!(commandBase is CommentCommand))
                {
                    var r = RunCommandAsync((commandBase));
                    if (r != null) await r;
                }
            }
        }
    }
}
