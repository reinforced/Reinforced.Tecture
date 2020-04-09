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
        private readonly TestingOrmSource _testingDataSource;
        private readonly AddCommandRunner _add;
        private readonly DeleteCommandRunner _remove;
        private readonly UpdateCommandRunner _update;
        public TestingOrmRuntime(TestingOrmSource testingDataSource)
        {
            _testingDataSource = testingDataSource;
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

        protected override ICommandRunner<AddCommand> ProvideAddRunner(AddCommand command)
        {
            return _add;
        }

        protected override ICommandRunner<DeleteCommand> ProvideDeleteRunner(DeleteCommand command)
        {
            return _remove;
        }

        protected override ICommandRunner<UpdateCommand> ProvideUpdateRunner(UpdateCommand command)
        {
            return _update;
        }

        protected override OrmSourceBase ProvideSource(Type sourceType)
        {
            return _testingDataSource;
        }
    }
}
