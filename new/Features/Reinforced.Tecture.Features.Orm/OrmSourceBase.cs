using System.Linq;
using Reinforced.Tecture.Features.Orm.Querу;

namespace Reinforced.Tecture.Features.Orm
{
    public abstract class OrmSourceBase : IOrmSource
    {
        private readonly QueryStats _stats = new QueryStats();
        
        /// <summary>
        /// Retrieves query builder
        /// </summary>
        /// <typeparam name="T">Entity to query from</typeparam>
        /// <returns>Query builder</returns>
        public IQueryFor<T> Get<T>() where T : class
        {
            return new QueryBuilder<T>(this);
        }

        public string Stats()
        {
            return _stats.Stats();
        }

        protected abstract IQueryable<T> ProvideSet<T>();

        public IQueryable<T> Set<T>()
        {
            return ProvideSet<T>();
        }

        public abstract T Runtime<T>() where T : class, ITectureRuntime;
    }
}
