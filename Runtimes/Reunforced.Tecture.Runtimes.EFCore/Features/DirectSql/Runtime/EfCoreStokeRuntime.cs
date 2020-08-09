using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.SchemaInterpolate;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime
{
    class EfCoreStokeRuntime : IStrokeRuntime
    {
        public EfCoreStokeRuntime(LazyDisposable<DbContext> dbContext, Type channel, InterpolatorFactory fac)
        {

            Mapper = new EfCoreMapper(dbContext);
            _servingTypes = new Lazy<IEnumerable<Type>>(() => dbContext.Value.Model.GetEntityTypes().Select(x => x.ClrType));
            Channel = channel;
            _fac = fac;
        }

        private readonly InterpolatorFactory _fac;
        private readonly Lazy<IEnumerable<Type>> _servingTypes;
        public IMapper Mapper { get; }
        public IEnumerable<Type> ServingTypes
        {
            get { return _servingTypes.Value; }
        }
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
