using System.Threading.Tasks;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Command
{
    class EFCore_DirectSql_Saver : Saver<Sql>
    {
        private readonly EFCore_DirectSql_CommandAspect _aspect;

        public EFCore_DirectSql_Saver(EFCore_DirectSql_CommandAspect aspect)
        {
            _aspect = aspect;
            
        }


        /// <inheritdoc />
        protected override void OnRegister()
        {
            _runner = new DirectSqlRunner(_aspect, Aux);
        }

        /// <inheritdoc />
        protected override void Save()
        {
            if (Aux.IsSavingNeeded)
            {
                _aspect.Context.Value.SaveChanges();
            }
        }

        /// <inheritdoc />
        protected override Task SaveAsync()
        {
            if (Aux.IsSavingNeeded)
            {
                return _aspect.Context.Value.SaveChangesAsync();
            }

            return Task.FromResult(0);
        }


        /// <inheritdoc />
        public override void Dispose()
        {
            _aspect.Dispose();
            _runner.Dispose();
        }

        private DirectSqlRunner _runner;


        /// <inheritdoc />
        protected override CommandRunner<Sql> GetRunner1(Sql command)
        {
            return _runner;
        }
    }
}
