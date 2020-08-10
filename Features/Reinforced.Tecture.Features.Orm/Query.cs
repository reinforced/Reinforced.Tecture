using System.Linq;
using Reinforced.Tecture.Features.Orm.Queries;
using Reinforced.Tecture.Features.Orm.Queries.Fake;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm
{
    public abstract class Query : QueryFeature
    {
        internal IQueryable<T> GetSet<T>() where T : class
        {
            if (Aux.IsHashRequired)
            {
                return new HookQueryable<T>(Set<T>(), Aux, null);
            }
            return Set<T>();
        }

        /// <summary>
        /// Retrieves queryable set
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>Queryable set of entities</returns>
        protected abstract IQueryable<T> Set<T>() where T : class;

        /// <summary>
        /// Obtains query stats
        /// </summary>
        public QueryStats Stats { get; } = new QueryStats();

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();

        internal void SetAux(Auxilary qs)
        {
            if (Aux == null) Aux = qs;
        }

        protected Auxilary Aux { get; private set; }
    }
}
