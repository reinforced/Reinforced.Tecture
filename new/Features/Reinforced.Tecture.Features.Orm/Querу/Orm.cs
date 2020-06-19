using System.Linq;

namespace Reinforced.Tecture.Features.Orm.Querу
{
    public abstract class Orm : QueryFeature
    {
        internal IQueryable<T> GetSet<T>()
        {
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
    }
}
