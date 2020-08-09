using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime
{
    class EfCoreStokeRuntime : IStrokeRuntime
    {
        public EfCoreStokeRuntime(LazyDisposable<DbContext> dbContext, Type channel)
        {

            Mapper = new EfCoreMapper(dbContext);
            _servingTypes = new Lazy<IEnumerable<Type>>(() => dbContext.Value.Model.GetEntityTypes().Select(x => x.ClrType));
            Channel = channel;
        }

        private readonly Lazy<IEnumerable<Type>> _servingTypes;
        public IMapper Mapper { get; }
        public IEnumerable<Type> ServingTypes
        {
            get { return _servingTypes.Value; }
        }
        public Type Channel { get; }
    }
}
