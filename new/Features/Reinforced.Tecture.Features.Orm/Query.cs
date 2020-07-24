using System.Linq;
using Reinforced.Tecture.Features.Orm.Queries;
using Reinforced.Tecture.Features.Orm.Queries.Fake;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Features.Orm
{
    public abstract class Query : QueryFeature
    {
        internal IQueryable<T> GetSet<T>()
        {
            if (QueryStore != null)
            {
                return new HookQueryable<T>(Set<T>(), QueryStore);
            }
            return Set<T>();
        }

        /// <summary>
        /// Retrieves queryable set
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>Queryable set of entities</returns>
        protected abstract IQueryable<T> Set<T>();

        /// <summary>
        /// Obtains query stats
        /// </summary>
        public QueryStats Stats { get; } = new QueryStats();

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();

        internal void SetQueryStore(IQueryStore qs)
        {
            if (QueryStore == null) QueryStore = qs;
        }

        protected IQueryStore QueryStore { get; private set; }
    }
}
