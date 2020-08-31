using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.SchemaInterpolate;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime
{
    class EfCoreStokeRuntime : IStrokeRuntime
    {
        protected Auxilary Aux { get; set; }

        public EfCoreStokeRuntime(ILazyDisposable<DbContext> dbContext, Type channel, InterpolatorFactory fac)
        {
            Mapper = new EfCoreMapper(dbContext);
            Channel = channel;
            _fac = fac;
        }

        private readonly InterpolatorFactory _fac;
        public IMapper Mapper { get; }
        public Type Channel { get; }
        public LanguageInterpolator GetLanguageInterpolator()
        {
            return _fac.CreateLanguageInterpolator();
        }

        public SchemaInterpolator GetSchemaInterpolator()
        {
            return _fac.CreateSchemaInterpolator(Mapper);
        }
    }
}
