using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class DirectSqlRunner : CommandRunner<Sql>, IDisposable
    {
        private readonly EFCore_DirectSql_CommandFeature _feature;
        private readonly Auxilary _aux;
        public DirectSqlRunner(EFCore_DirectSql_CommandFeature feature, Auxilary aux)
        {
            _feature = feature;
            _aux = aux;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Sql cmd)
        {
            if (_aux.IsCommandRunNeeded)
            {
                var query = _feature.Compile(cmd);
                _feature.Context.Value.Database.ExecuteSqlRaw(query.Query, query.Parameters);
            }
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Sql cmd)
        {
            if (_aux.IsCommandRunNeeded)
            {
                var query = _feature.Compile(cmd);
                return _feature.Context.Value.Database.ExecuteSqlRawAsync(query.Query, query.Parameters);
            }

            return Task.FromResult(0);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _feature.Dispose();
        }
    }
}
