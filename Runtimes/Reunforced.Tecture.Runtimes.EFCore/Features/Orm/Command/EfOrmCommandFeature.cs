using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class EfOrmCommandFeature : Tecture.Features.Orm.Command
    {
        private readonly LazyDisposable<DbContext> _context;

        public EfOrmCommandFeature(LazyDisposable<DbContext> context)
        {
            _context = context;
        }

        protected override bool IsSubject(Type t)
        {
            return _context.Value.Model.FindEntityType(t) != null;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _context.Dispose();
        }
    }
}
