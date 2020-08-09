using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Query
{
    class EfOrmQueryFeature : Tecture.Features.Orm.Query
    {
        private readonly LazyDisposable<DbContext> _context;

        public EfOrmQueryFeature(LazyDisposable<DbContext> context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves queryable set
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>Queryable set of entities</returns>
        protected override IQueryable<T> Set<T>()
        {
            return _context.Value.Set<T>();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _context.Dispose();
        }
    }
}
