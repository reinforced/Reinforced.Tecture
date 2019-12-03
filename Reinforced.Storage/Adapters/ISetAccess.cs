using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Adapters
{
    public interface ISetAccess
    {
        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
        IEnumerable<TEntity> RawQuery<TEntity>(DirectSqlSideEffect sql) where TEntity : class;
        IQueryable<TEntity> NoTracking<TEntity>(IQueryable<TEntity> q) where TEntity : class;
        IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> q, Expression includeExpression) where TEntity : class;
    }
}