using System;
using System.Collections.Generic;

namespace Reinforced.Storage.Testing.SideEffects
{
    
    public class EntityBasedSideEffect
    {
        protected readonly TestingEnvironment _env;

        private readonly Dictionary<Type, List<Delegate>> _assumedActions = new Dictionary<Type, List<Delegate>>();

        public EntityBasedSideEffect(TestingEnvironment env)
        {
            _env = env;
        }

        public EntityBasedSideEffect Assume<T>(Action<T, ICollectionProvider> ua)
        {
            if (!_assumedActions.ContainsKey(typeof(T))) _assumedActions[typeof(T)] = new List<Delegate>();
            _assumedActions[typeof(T)].Add(ua);
            return this;
        }
    }
}
