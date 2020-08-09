using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.SideEffects
{
    /// <summary>
    /// Dispatches side effect queueand implements enqueued actions
    /// </summary>
    public class SideEffectsDispatcher
    {
        private class EffectRunnerGateway
        {
            private readonly MethodInfo _run;
            private readonly MethodInfo _runAsync;
            private readonly object _runner;

            public EffectRunnerGateway(Type effectType, object runner)
            {
                _runner = runner;

                var runnerType = runner.GetType();
                _run = runnerType.GetMethod(nameof(ISideEffectRunner<SideEffectBase>.Run), new[] { effectType });
                _runAsync = runnerType.GetMethod(nameof(ISideEffectRunner<SideEffectBase>.RunAsync), new[] { effectType });

            }

            public void Run(SideEffectBase effect)
            {
                _run.Invoke(_runner, new[] { effect });
            }

            public async Task RunAsync(SideEffectBase effect)
            {
                var r = (Task)_runAsync.Invoke(_runner, new[] { effect });
                await r;
            }
        }

        private readonly Dictionary<Type, EffectRunnerGateway> _runners = new Dictionary<Type, EffectRunnerGateway>();
        private readonly Queue<ISideEffectSaver> _savers = new Queue<ISideEffectSaver>();

        /// <summary>
        /// Registers side effect runner for specified effect. Previous registration ruins all others
        /// </summary>
        /// <typeparam name="TSideEffect">Side effect runner type</typeparam>
        /// <param name="runner">Side effect runner</param>
        /// <returns>Fluent</returns>
        public SideEffectsDispatcher RegisterRunner<TSideEffect>(ISideEffectRunner<TSideEffect> runner) where TSideEffect : SideEffectBase
        {
            _runners[typeof(TSideEffect)] = new EffectRunnerGateway(typeof(TSideEffect), runner);
            return this;
        }

        /// <summary>
        /// Registers changes saver
        /// </summary>
        /// <param name="saver"></param>
        /// <returns></returns>
        public SideEffectsDispatcher RegisterSaver(ISideEffectSaver saver)
        {
            _savers.Enqueue(saver);
            return this;
        }

        internal Action<Exception> _exHandler;
        public SideEffectsDispatcher RegisterExceptionHandler(Action<Exception> ex)
        {
            _exHandler = ex;
            return this;
        }

        internal virtual void Dispatch(Effects queue, ActionsQueue postSave)
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

        internal virtual async Task DispatchAsync(Effects queue, ActionsQueue postSave)
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

        private EffectRunnerGateway GetRunner(SideEffectBase effect)
        {
            var effectType = effect.GetType();
            if (!_runners.ContainsKey(effectType)) throw new Exception($"Unknown side effect {effectType} - do not know how to dispatch");
            return _runners[effectType];
        }

        protected void RunEffect(SideEffectBase effect)
        {
            GetRunner(effect).Run(effect);
        }

        protected Task RunEffectAsync(SideEffectBase effect)
        {
            return GetRunner(effect).RunAsync(effect);
        }

        protected virtual void DispatchInternal(IEnumerable<SideEffectBase> effects)
        {
            foreach (var sideEffectBase in effects)
            {
                if (!(sideEffectBase is CommentSideEffect)) RunEffect(sideEffectBase);
            }
        }

        protected virtual async Task DispatchInternalAsync(IEnumerable<SideEffectBase> effects)
        {
            foreach (var sideEffectBase in effects)
            {
                if (!(sideEffectBase is CommentSideEffect)) await RunEffectAsync(sideEffectBase);
            }
        }
    }
}
