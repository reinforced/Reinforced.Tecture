using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Runtimes.EFCore.Aspect.DirectSql.Runtime;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspect.DirectSql.Query
{
    class EFCore_DirectSql_QueryAspect : Aspects.DirectSql.Query
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

        public EFCore_DirectSql_QueryAspect(ILazyDisposable<DbContext> context, Type channel, InterpolatorFactory fac) : base(new EfCoreStokeRuntime(context, channel, fac))
        {
            _dbContext = context;
        }

        protected override HashSet<Type> ServingTypes
        {
            get
            {
                if (Aux.IsCommandRunNeeded || Aux.IsEvaluationNeeded)
                {
                    return new HashSet<Type>(_dbContext.Value.Model.GetEntityTypes().Select(x => x.ClrType));
                }
                else
                {
                    var tp = _dbContext.ValueType;
                    var allDbSets = tp.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

                    var typeQuery = allDbSets
                        .SelectMany(x => x.PropertyType.GetGenericArguments());
                    return new HashSet<Type>(typeQuery);
                }
            }
        }
    }
}
