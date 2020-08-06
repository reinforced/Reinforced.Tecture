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
            if (TestData != null)
            {
                return new HookQueryable<T>(Set<T>(), TestData, null);
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

        internal void SetQueryStore(TestData qs)
        {
            if (TestData == null) TestData = qs;
        }

        protected TestData TestData { get; private set; }
    }
}
