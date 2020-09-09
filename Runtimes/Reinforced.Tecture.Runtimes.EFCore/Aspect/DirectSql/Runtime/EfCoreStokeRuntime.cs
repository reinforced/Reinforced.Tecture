using System;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.DirectSql.Infrastructure;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspect.DirectSql.Runtime
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
        public Aspects.DirectSql.Reveal.LanguageInterpolate.LanguageInterpolator GetLanguageInterpolator()
        {
            return _fac.CreateLanguageInterpolator();
        }

        public SchemaInterpolator GetSchemaInterpolator()
        {
            return _fac.CreateSchemaInterpolator(Mapper);
        }
    }
}
