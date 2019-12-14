using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands.Exact;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Dispatches side effect queueand implements enqueued actions
    /// </summary>
    public class CommandsDispatcher
    {
        private class CommandRunnerGateway
        {
            private readonly MethodInfo _run;
            private readonly MethodInfo _runAsync;
            private readonly object _runner;

            public CommandRunnerGateway(Type effectType, object runner)
            {
                _runner = runner;

                var runnerType = runner.GetType();
                _run = runnerType.GetMethod(nameof(ICommandRunner<CommandBase>.Run), new[] { effectType });
                _runAsync = runnerType.GetMethod(nameof(ICommandRunner<CommandBase>.RunAsync), new[] { effectType });

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
        private readonly Queue<ISideEffectSaver> _savers = new Queue<ISideEffectSaver>();

        /// <summary>
        /// Registers side effect runner for specified effect. Previous registration ruins all others
        /// </summary>
        /// <typeparam name="TCommand">Side effect runner type</typeparam>
        /// <param name="runner">Side effect runner</param>
        /// <returns>Fluent</returns>
        public CommandsDispatcher RegisterRunner<TCommand>(ICommandRunner<TCommand> runner) where TCommand : CommandBase
        {
            _runners[typeof(TCommand)] = new CommandRunnerGateway(typeof(TCommand), runner);
            return this;
        }

        /// <summary>
        /// Registers changes saver
        /// </summary>
        /// <param name="saver"></param>
        /// <returns></returns>
        public CommandsDispatcher RegisterSaver(ISideEffectSaver saver)
        {
            _savers.Enqueue(saver);
            return this;
        }

        internal Action<Exception> _exHandler;
        public CommandsDispatcher RegisterExceptionHandler(Action<Exception> ex)
        {
            _exHandler = ex;
            return this;
        }

        internal virtual void Dispatch(Pipeline queue, ActionsQueue postSave)
        {

            do
            {
                if (queue.HasEffects) DispatchInternal(queue.GetEffects());

                foreach (var sideEffectSaver in _savers)
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

                foreach (var sideEffectSaver in _savers)
                {
                    await sideEffectSaver.SaveAsync();
                }

                await postSave.RunAsync();
            } while (queue.HasEffects);

        }

        private CommandRunnerGateway GetRunner(CommandBase effect)
        {
            var effectType = effect.GetType();
            if (!_runners.ContainsKey(effectType)) throw new Exception($"Unknown side effect {effectType} - do not know how to dispatch");
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
