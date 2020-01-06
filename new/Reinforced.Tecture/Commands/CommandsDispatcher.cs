using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands.Exact;
using Reinforced.Tecture.Integrate;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Dispatches commands queue and implements enqueued actions
    /// </summary>
    public class CommandsDispatcher
    {
        private readonly Registry _registry;
        private class CommandRunnerGateway
        {
            private readonly MethodInfo _run;
            private readonly MethodInfo _runAsync;
            private readonly object _runner;

            public CommandRunnerGateway(Type effectType, object runner)
            {
                _runner = runner;

                var runnerType = runner.GetType();
                _run = runnerType.GetRuntimeMethod(nameof(ICommandRunner<CommandBase>.Run), new[] { effectType });
                _runAsync = runnerType.GetRuntimeMethod(nameof(ICommandRunner<CommandBase>.RunAsync), new[] { effectType });

            }

            public void Run(CommandBase effect)
            {
                _run.Invoke(_runner, new[] { effect });
            }

            public async Task RunAsync(CommandBase effect)
            {
                var r = (Task)_runAsync.Invoke(_runner, new[] { effect });
                await r;
            }
        }

        private readonly Dictionary<Type, CommandRunnerGateway> _runners = new Dictionary<Type, CommandRunnerGateway>();
        
        internal CommandsDispatcher(Registry registry)
        {
            _registry = registry;
            _savers = new Lazy<ISaver[]>(()=>_registry.GetSavers());
        }


        private readonly Lazy<ISaver[]> _savers;
        internal virtual void Dispatch(Pipeline queue, ActionsQueue postSave)
        {
            do
            {
                if (queue.HasEffects) DispatchInternal(queue.GetEffects());

                foreach (var sideEffectSaver in _savers.Value)
                {
                    sideEffectSaver.Save();
                }

                postSave.Run();
            } while (queue.HasEffects);
        }

        internal virtual async Task DispatchAsync(Pipeline queue, ActionsQueue postSave)
        {

            do
            {
                if (queue.HasEffects) await DispatchInternalAsync(queue.GetEffects());

                foreach (var sideEffectSaver in _savers.Value)
                {
                    await sideEffectSaver.SaveAsync();
                }

                await postSave.RunAsync();
            } while (queue.HasEffects);

        }

        private CommandRunnerGateway GetRunner(CommandBase command)
        {
            var effectType = command.GetType();
            if (!_runners.ContainsKey(effectType))
            {
                _runners[effectType] = 
                    new CommandRunnerGateway(effectType,_registry.GetRunner(effectType));
            }
            return _runners[effectType];
        }

        protected void RunEffect(CommandBase effect)
        {
            GetRunner(effect).Run(effect);
        }

        protected Task RunEffectAsync(CommandBase effect)
        {
            return GetRunner(effect).RunAsync(effect);
        }

        protected virtual void DispatchInternal(IEnumerable<CommandBase> commands)
        {
            foreach (var sideEffectBase in commands)
            {
                if (!(sideEffectBase is CommentCommand)) RunEffect(sideEffectBase);
            }
        }

        protected virtual async Task DispatchInternalAsync(IEnumerable<CommandBase> commands)
        {
            foreach (var sideEffectBase in commands)
            {
                if (!(sideEffectBase is CommentCommand)) await RunEffectAsync(sideEffectBase);
            }
        }
    }
}
