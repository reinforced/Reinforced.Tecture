using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Query
{
    class EFCore_DirectSql_QueryFeature : Reinforced.Tecture.Features.SqlStroke.Query
    {
        public override IEnumerable<T> DoQuery<T>(string command, object[] parameters)
        {
            var set = _dbContext.Value.Set<T>();
            if (set==null)
                throw new EfCoreDirectSqlException($"Cannot locate set of type '{typeof(T)}' in DbContext");
            return set.FromSqlRaw(command, parameters).ToArray();
        }

        public override Task<IEnumerable<T>> DoQueryAsync<T>(string command, object[] parameters)
        {
            var set = _dbContext.Value.Set<T>();
            if (set == null)
                throw new EfCoreDirectSqlException($"Cannot locate set of type '{typeof(T)}' in DbContext");
            return Task.FromResult(set.FromSqlRaw(command, parameters).ToArray().AsEnumerable());
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _dbContext.Dispose();
        }

        private readonly ILazyDisposable<DbContext> _dbContext;

        public EFCore_DirectSql_QueryFeature(ILazyDisposable<DbContext> context, Type channel, InterpolatorFactory fac) : base(new EfCoreStokeRuntime(context, channel, fac))
        {
            _dbContext = context;
        }
    }
}
