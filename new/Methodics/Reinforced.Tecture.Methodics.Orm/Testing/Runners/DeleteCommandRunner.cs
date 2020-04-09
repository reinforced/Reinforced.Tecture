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
        public RemoveAssumptionArgument(T entity, Delete sideEffect, ICollectionProvider collectionProvider)
        {
            Entity = entity;
            SideEffect = sideEffect;
            CollectionProvider = collectionProvider;
        }

        public T Entity { get; private set; }

        public Delete SideEffect { get; private set; }

        public ICollectionProvider CollectionProvider { get; private set; }
    }
    class DeleteCommandRunner : ICommandRunner<Delete>
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
        /// <param name="cmd">Side effect</param>
        public void Run(Delete cmd)
        {
            var coll = _env.GetCollection(cmd.EntityType);
            coll.Remove(cmd.Entity);
            if (_assumedActions.ContainsKey(cmd.EntityType))
            {
                var l = _assumedActions[cmd.EntityType];
                var inst = Activator.CreateInstance(typeof(RemoveAssumptionArgument<>).MakeGenericType(cmd.EntityType), new[] { cmd.Entity, cmd, _env });
                foreach (var del in l)
                {
                    del.DynamicInvoke(inst);
                }
            }
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(Delete cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
