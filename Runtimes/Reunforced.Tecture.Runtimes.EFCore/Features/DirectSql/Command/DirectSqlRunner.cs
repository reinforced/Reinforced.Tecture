using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Commands;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class DirectSqlRunner : CommandRunner<Sql>, IDisposable
    {
        private readonly LazyDisposable<DbContext> _context;

        public DirectSqlRunner(LazyDisposable<DbContext> context)
        {
            _context = context;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Sql cmd)
        {
            _context.Value.Database.ExecuteSqlRaw(cmd.Command, cmd.Parameters);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Sql cmd)
        {
            return _context.Value.Database.ExecuteSqlRawAsync(cmd.Command, cmd.Parameters);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
