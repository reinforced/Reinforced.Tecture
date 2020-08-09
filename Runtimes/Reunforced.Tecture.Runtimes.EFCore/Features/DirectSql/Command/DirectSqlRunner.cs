using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Commands;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class DirectSqlRunner : CommandRunner<Sql>, IDisposable
    {
        private readonly EFCore_DirectSql_CommandFeature _feature;

        public DirectSqlRunner(EFCore_DirectSql_CommandFeature feature)
        {
            _feature = feature;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Sql cmd)
        {
            var query = _feature.Compile(cmd);
            _feature.Context.Value.Database.ExecuteSqlRaw(query.Query, query.Parameters);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Sql cmd)
        {
            var query = _feature.Compile(cmd);
            return _feature.Context.Value.Database.ExecuteSqlRawAsync(query.Query, query.Parameters);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _feature.Dispose();
        }
    }
}
