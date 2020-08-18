using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class EFCore_DirectSql_Saver : Saver<Sql>
    {
        private readonly EFCore_DirectSql_CommandFeature _feature;

        public EFCore_DirectSql_Saver(EFCore_DirectSql_CommandFeature feature)
        {
            _feature = feature;
            _runner = new DirectSqlRunner(feature);
        }

        protected override void Save()
        {
            _feature.Context.Value.SaveChanges();
        }

        protected override Task SaveAsync()
        {
            return _feature.Context.Value.SaveChangesAsync();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _feature.Dispose();
            _runner.Dispose();
        }

        private readonly DirectSqlRunner _runner;

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
