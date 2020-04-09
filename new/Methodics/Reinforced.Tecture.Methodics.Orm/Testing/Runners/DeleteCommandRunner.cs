using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Runners
{
    public class RemoveAssumptionArgument<T>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public RemoveAssumptionArgument(T entity, DeleteCommand sideEffect, ICollectionProvider collectionProvider)
        {
            Entity = entity;
            SideEffect = sideEffect;
            CollectionProvider = collectionProvider;
        }

        public T Entity { get; private set; }

        public DeleteCommand SideEffect { get; private set; }

        public ICollectionProvider CollectionProvider { get; private set; }
    }
    class DeleteCommandRunner : ICommandRunner<DeleteCommand>
    {
        private readonly ICollectionProvider _env;
        private readonly Dictionary<Type, List<Delegate>> _assumedActions = new Dictionary<Type, List<Delegate>>();

        public DeleteCommandRunner Assume<T>(Action<RemoveAssumptionArgument<T>> ua)
        {
            if (!_assumedActions.ContainsKey(typeof(T))) _assumedActions[typeof(T)] = new List<Delegate>();
            _assumedActions[typeof(T)].Add(ua);
            return this;
        }

        public DeleteCommandRunner(ICollectionProvider env)
        {
            _env = env;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(DeleteCommand effect)
        {
            var coll = _env.GetCollection(effect.EntityType);
            coll.Remove(effect.Entity);
            if (_assumedActions.ContainsKey(effect.EntityType))
            {
                var l = _assumedActions[effect.EntityType];
                var inst = Activator.CreateInstance(typeof(RemoveAssumptionArgument<>).MakeGenericType(effect.EntityType), new[] { effect.Entity, effect, _env });
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
        public Task RunAsync(DeleteCommand effect)
        {
            Run(effect);
            return Task.FromResult(0);
        }
    }
}
