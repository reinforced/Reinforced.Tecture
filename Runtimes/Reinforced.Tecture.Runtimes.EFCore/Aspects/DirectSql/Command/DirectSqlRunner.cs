using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Command
{
    class DirectSqlRunner : CommandRunner<Sql>, IDisposable
    {
        private readonly EFCore_DirectSql_CommandAspect _aspect;
        private readonly TestingContext _aux;
        public DirectSqlRunner(EFCore_DirectSql_CommandAspect aspect, TestingContext aux)
        {
            _aspect = aspect;
            _aux = aux;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Sql cmd)
        {
            if (!_aux.ProvidesTestData)
            {
                var query = _aspect.Compile(cmd);
                try
                {
                    _aspect.DbContext.Value.Database.ExecuteSqlRaw(query.Query, query.Parameters);
                }
                catch (Exception ex)
                {
                    throw new EfCoreDirectSqlException($"Error executing query:\r\n{query.Query}\r\n", ex);
                }
            }
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Sql cmd,CancellationToken token = default)
        {
            if (!_aux.ProvidesTestData)
            {
                var query = _aspect.Compile(cmd);
                try
                {
                    return _aspect.DbContext.Value.Database.ExecuteSqlRawAsync(query.Query, query.Parameters,token);
                }
                catch (Exception ex)
                {
                    throw new EfCoreDirectSqlException($"Error executing query:\r\n{query.Query}\r\n", ex);
                }
            }

            return Task.FromResult(0);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            //_aspect.Dispose(); violation of Demeter Law. I played with not my own toys
        }
    }
}
