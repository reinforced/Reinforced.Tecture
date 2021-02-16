using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Reinforced.Tecture.Aspects.Orm.Queries.Wrapped.Queryables;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Orm
{
    /// <summary>
    /// ORM query aspect
    /// </summary>
    public abstract partial class Query : QueryAspect
    {
        internal Auxiliary Aux
        {
            get { return base.Aux; }
        }

        internal IQueryable<T> GetSet<T>() where T : class
        {
            IQueryable<T> set = Aux.IsEvaluationNeeded ? Set<T>() : new T[0].AsQueryable();
            return new WrappedQueryable<T>(set,this, new DescriptionHolder());
        }

        /// <summary>
        /// Retrieves queryable set
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>Queryable set of entities</returns>
        protected abstract IQueryable<T> Set<T>() where T : class;

        internal IAsyncQueryExecutor AsyncExecutorActually
        {
            get { return AsyncExecutor; }
        }

        /// <summary>
        /// Gets asynchronous query executor instance
        /// </summary>
        protected abstract IAsyncQueryExecutor AsyncExecutor { get; }

        /// <summary>
        /// Returns key of just added entity
        /// </summary>
        /// <param name="addCommand">Addition command</param>
        /// <param name="keyProperties">Key property</param>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetKey(Add addCommand, IEnumerable<PropertyInfo> keyProperties);

        /// <summary>
        /// Obtains query stats
        /// </summary>
        public QueryStats Stats { get; } = new QueryStats();

        internal T Key<T>(IAddition<IPrimaryKey<T>> keyedAddition)
        {
            var a = (Add)keyedAddition;
            if (!a.IsExecuted)
                throw new TectureOrmAspectException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            string explanation = $"Get primary key of added {a.EntityType.Name}";

            var p = Aux.Promise<T>();
            if (p is Containing<T> c)
                return c.Get($"ORM_AdditionPK_{a.Order}", explanation);

            var result = (T)(GetKey(a, GetKeyProperties<T>(a)).First());
            
            if (p is Demanding<T> d)
                d.Fullfill(result, $"ORM_AdditionPK_{a.Order}", explanation);

            return result;
        }

        private IEnumerable<PropertyInfo> GetKeyProperties<T>(Add addition)
        {
            var e = (IPrimaryKey<T>)addition.Entity;
            yield return e.PrimaryKey.AsPropertyExpression();
        }

    }
}
