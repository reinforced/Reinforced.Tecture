using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Runners
{
    public class AddAssumptionArgument<T>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public AddAssumptionArgument(T entity, Add sideEffect, ICollectionProvider collectionProvider)
        {
            Entity = entity;
            SideEffect = sideEffect;
            CollectionProvider = collectionProvider;
        }

        public T Entity { get; private set; }

        public Add SideEffect { get; private set; }

        public ICollectionProvider CollectionProvider { get; private set; }
    }

    class AddCommandRunner : ICommandRunner<Add>
    {
        private readonly ICollectionProvider _env;
        private readonly Dictionary<Type, List<Delegate>> _assumedActions = new Dictionary<Type, List<Delegate>>();

        public AddCommandRunner(ICollectionProvider env)
        {
            _env = env;
        }

        public AddCommandRunner Assume<T>(Action<AddAssumptionArgument<T>> ua)
        {
            if (!_assumedActions.ContainsKey(typeof(T))) _assumedActions[typeof(T)] = new List<Delegate>();
            _assumedActions[typeof(T)].Add(ua);
            return this;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        public void Run(Add cmd)
        {
            
            if (_assumedActions.ContainsKey(cmd.EntityType))
            {
                var l = _assumedActions[cmd.EntityType];
                var inst = Activator.CreateInstance(typeof(AddAssumptionArgument<>).MakeGenericType(cmd.EntityType), new[] { cmd.Entity, cmd, _env });
                foreach (var del in l)
                {
                    del.DynamicInvoke(inst);
                }
            }
            else
            {
                var coll = _env.GetCollection(cmd.EntityType);
                coll.Add(cmd.Entity);
            }
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(Add cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
