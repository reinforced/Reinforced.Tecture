using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.SideEffects
{
    public class AddAssumptionArgument<T>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public AddAssumptionArgument(T entity, AddSideEffect sideEffect, ICollectionProvider collectionProvider)
        {
            Entity = entity;
            SideEffect = sideEffect;
            CollectionProvider = collectionProvider;
        }

        public T Entity { get; private set; }

        public AddSideEffect SideEffect { get; private set; }

        public ICollectionProvider CollectionProvider { get; private set; }
    }

    public class AddSideEffectRunner : ISideEffectRunner<AddSideEffect>
    {
        private readonly TestingEnvironment _env;
        private readonly Dictionary<Type, List<Delegate>> _assumedActions = new Dictionary<Type, List<Delegate>>();

        public AddSideEffectRunner(TestingEnvironment env)
        {
            _env = env;
        }

        public AddSideEffectRunner Assume<T>(Action<AddAssumptionArgument<T>> ua)
        {
            if (!_assumedActions.ContainsKey(typeof(T))) _assumedActions[typeof(T)] = new List<Delegate>();
            _assumedActions[typeof(T)].Add(ua);
            return this;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(AddSideEffect effect)
        {
            var coll = _env.GetCollection(effect.EntityType);
            coll.Add(effect.Entity);
            if (_assumedActions.ContainsKey(effect.EntityType))
            {
                var l = _assumedActions[effect.EntityType];
                var inst = Activator.CreateInstance(typeof(AddAssumptionArgument<>).MakeGenericType(effect.EntityType), new[] { effect.Entity, effect, _env });
                foreach (var del in l)
                {
                    del.DynamicInvoke(inst);
                }
            }
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(AddSideEffect effect)
        {
            Run(effect);
            return Task.FromResult(0);
        }
    }
}
