using System;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class EFCore_DirectSql_CommandFeature : Reinforced.Tecture.Features.SqlStroke.Command
    {
        private LazyDisposable<DbContext> _context;

        internal LazyDisposable<DbContext> Context
        {
            get { return _context; }
        }

        public EFCore_DirectSql_CommandFeature(LazyDisposable<DbContext> context, Type channel, InterpolatorFactory fac) : base(new EfCoreStokeRuntime(context, channel, fac))
        {
            _context = context;
        }
    }
}
