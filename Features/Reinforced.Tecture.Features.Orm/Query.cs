using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Features.Orm.Queries;
using Reinforced.Tecture.Features.Orm.Queries.Fake;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm
{
    public abstract partial class Query : QueryFeature
    {
        internal IQueryable<T> GetSet<T>() where T : class
        {
            IQueryable<T> set = Aux.IsEvaluationNeeded ? Set<T>() : new T[0].AsQueryable();
            if (Aux.IsHashRequired)
            {
                return new HookQueryable<T>(set, Aux, null);
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
        /// Returns key of just added entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
                throw new TectureOrmFeatureException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            T result;
            string hash = Aux.IsHashRequired ? $"ORM_AdditionPK_{a.Order}" : string.Empty;
            if (Aux.IsEvaluationNeeded)
            {
                result = (T)(GetKey(a, GetKeyProperties<T>(a)).First());
            }
            else
            {
                result = Aux.Get<T>(hash);
            }

            if (Aux.IsTracingNeeded)
            {
                if (!Aux.IsEvaluationNeeded)
                {
                    Aux.Query(hash, "test data", "ORM Addition PK retrieval");
                }
                else
                {
                    Aux.Query(hash, result, "ORM Addition PK retrieval");
                }
            }

            return result;
        }

        private IEnumerable<PropertyInfo> GetKeyProperties<T>(Add addition)
        {
            var e = (IPrimaryKey<T>)addition.Entity;
            yield return e.PrimaryKey.AsPropertyExpression();
        }

    }
}
