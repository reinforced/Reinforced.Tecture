using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class DirectSqlSaver : Saver<Sql>
    {
        private readonly LazyDisposable<DbContext> _context;

        public DirectSqlSaver(LazyDisposable<DbContext> context)
        {
            _context = context;
            _runner = new DirectSqlRunner(context);
        }

        protected override void Save()
        {
            _context.Value.SaveChanges();
        }

        protected override Task SaveAsync()
        {
            return _context.Value.SaveChangesAsync();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _context.Dispose();
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
