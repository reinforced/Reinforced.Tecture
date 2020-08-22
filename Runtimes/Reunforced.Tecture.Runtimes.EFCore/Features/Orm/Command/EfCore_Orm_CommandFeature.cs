using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class EfCore_Orm_CommandFeature : Tecture.Features.Orm.Command
    {
        private readonly ILazyDisposable<DbContext> _context;

        public EfCore_Orm_CommandFeature(ILazyDisposable<DbContext> context)
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
