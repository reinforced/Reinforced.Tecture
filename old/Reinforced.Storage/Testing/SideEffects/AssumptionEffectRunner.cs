using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.SideEffects
{
    /// <summary>
    /// Side effect runner mock based on assumtions
    /// </summary>
    /// <typeparam name="T">Side effect type</typeparam>
    public class AssumptionEffectRunner<T> : ISideEffectRunner<T> where T : SideEffectBase
    {
        protected readonly TestingEnvironment _env;
        private bool _skipMissing = false;
        public AssumptionEffectRunner<T> SkipMissingAssumptions(bool skip = true)
        {
            _skipMissing = skip;
            return this;
        }

        private readonly Dictionary<string, Action<T, ICollectionProvider>> _assumptions = new Dictionary<string, Action<T, ICollectionProvider>>();
        public AssumptionEffectRunner<T> Assume(string annotation,
            Action<T, ICollectionProvider> action)
        {
            _assumptions[annotation] = action;
            return this;
        }

        private readonly List<Tuple<Func<T, bool>, Action<T, ICollectionProvider>>> _predicateActions = new List<Tuple<Func<T, bool>, Action<T, ICollectionProvider>>>();

        public AssumptionEffectRunner(TestingEnvironment env)
        {
            _env = env;
        }

        public AssumptionEffectRunner<T> Assume(Func<T, bool> predicate,Action<T, ICollectionProvider> action)
        {
            _predicateActions.Add(new Tuple<Func<T, bool>, Action<T, ICollectionProvider>>(predicate, action));
            return this;
        }

        public bool HasAssumption(T effect)
        {
            bool hasAssumption = false;

            if (_assumptions.ContainsKey(effect.Annotation))
            {
                hasAssumption = true;
            }
            else
            {
                var assumed = _predicateActions.Where(d => d.Item1(effect)).Select(d => d.Item2).ToArray();
                hasAssumption = assumed.Any();
            }

            return hasAssumption;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(T effect)
        {
            bool hasAssumption = false;

            if (_assumptions.ContainsKey(effect.Annotation))
            {
                _assumptions[effect.Annotation](effect, _env);
                hasAssumption = true;
            }
            else
            {
                var assumed = _predicateActions.Where(d => d.Item1(effect)).Select(d => d.Item2).ToArray();
                foreach (var action in assumed)
                {
                    action(effect, _env);
                    hasAssumption = true;
                }
            }
            if (!hasAssumption)
                if (!_skipMissing)
                throw new Exception($"No assumption for {effect}. Please add assumption using .Assume(...)");

        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(T effect)
        {
            Run(effect);
            return Task.FromResult(0);
        }
    }
}
