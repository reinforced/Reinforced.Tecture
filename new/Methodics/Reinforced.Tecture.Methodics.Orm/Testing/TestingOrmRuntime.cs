using System;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Methodics.Orm.Commands.Update;
using Reinforced.Tecture.Methodics.Orm.Testing.Runners;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Methodics.Orm.Testing
{
    class TestingOrmRuntime : OrmRuntimeBase, ITestingRuntime
    {
        internal readonly TestingOrmSource _testingDataSource;
        private readonly AddCommandRunner _add;
        private readonly DeleteCommandRunner _remove;
        private readonly UpdateCommandRunner _update;
        public TestingOrmRuntime(bool strict)
        {
            _testingDataSource = new TestingOrmSource(this, strict);
            _add = new AddCommandRunner(_testingDataSource);
            _remove = new DeleteCommandRunner(_testingDataSource);
            _update = new UpdateCommandRunner(_testingDataSource);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            //no disposable components
        }

        private static readonly ISaver[] _empty = new ISaver[0];
        /// <summary>
        /// Override supplies savers set
        /// </summary>
        /// <returns>Savers</returns>
        public override ISaver[] GetSavers()
        {
            return _empty;
        }

        /// <summary>
        /// Gets whether runtime is in testing mode
        /// </summary>
        public override bool Testing
        {
            get { return true; }
        }

        protected override ICommandRunner<Add> ProvideAddRunner(Add command)
        {
            return _add;
        }

        protected override ICommandRunner<Delete> ProvideDeleteRunner(Delete command)
        {
            return _remove;
        }

        protected override ICommandRunner<Update> ProvideUpdateRunner(Update command)
        {
            return _update;
        }

        protected override OrmSourceBase ProvideSource(Type sourceType)
        {
            return _testingDataSource;
        }
    }
}
