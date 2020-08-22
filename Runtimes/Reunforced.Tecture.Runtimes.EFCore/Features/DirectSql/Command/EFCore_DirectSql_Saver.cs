using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class EFCore_DirectSql_Saver : Saver<Sql>
    {
        private readonly EFCore_DirectSql_CommandFeature _feature;

        public EFCore_DirectSql_Saver(EFCore_DirectSql_CommandFeature feature)
        {
            _feature = feature;
            
        }

        protected override void OnRegister()
        {
            _runner = new DirectSqlRunner(_feature, Aux);
        }

        protected override void Save()
        {
            if (Aux.IsSavingNeeded)
            {
                _feature.Context.Value.SaveChanges();
            }
        }

        protected override Task SaveAsync()
        {
            if (Aux.IsSavingNeeded)
            {
                return _feature.Context.Value.SaveChangesAsync();
            }

            return Task.FromResult(0);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _feature.Dispose();
            _runner.Dispose();
        }

        private DirectSqlRunner _runner;

        /// <summary>
        /// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
        /// </summary>
        /// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
        /// <returns>Command runner</returns>
        protected override CommandRunner<Sql> GetRunner1(Sql command)
        {
            return _runner;
        }
    }
}
