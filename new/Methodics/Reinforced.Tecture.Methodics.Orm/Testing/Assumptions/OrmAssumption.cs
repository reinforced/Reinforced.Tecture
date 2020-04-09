using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Testing.Assumptions;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Assumptions
{
    class OrmAssumption<TCommand> : AssumptionBase<TCommand> where TCommand : CommandBase, IOrmCommand
    {
        internal OrmAssumption(Type entityType, Delegate predicate, Delegate action, bool shouldRunOriginal)
        {
            _entityType = entityType;
            _predicate = predicate;
            _action = action;
            _shouldRunOriginal = shouldRunOriginal;
        }

        private readonly bool _shouldRunOriginal;
        private readonly Type _entityType;
        private readonly Delegate _predicate;
        private readonly Delegate _action;

        /// <summary>
        /// Determines whether assumption must be applied for command instead of
        /// honest command run
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns>True when this assumption should be applied for command, false otherwise</returns>
        protected override bool ShouldActually(TCommand cmd)
        {
            if (cmd.EntityType != _entityType) return false;
            if (_predicate!=null) return (bool)_predicate.DynamicInvoke(cmd.Entity);
            return true;
        }

        /// <summary>
        /// Performs assumed actions. Use Runtimes to locate your runtime and proceed
        /// </summary>
        /// <param name="cmd">Command</param>
        protected override void AssumeActually(TCommand cmd)
        {
            var rt = Locate.GetRuntimes<TestingOrmRuntime>().FirstOrDefault();
            if (rt == null) throw new TectureException($"Cannot locate {nameof(TestingOrmRuntime)}");
            var ds = (ICollectionProvider)rt._testingDataSource;
            _action.DynamicInvoke(cmd.Entity, ds);
            if (_shouldRunOriginal && Runner != null)
                Runner.Run(cmd);
        }
    }
}
