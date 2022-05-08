using System.Linq;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.TraceWrapping;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Aspects.Orm.Queries
{
    public static partial class Extensions
    {
        /// <summary>
        /// Retrieves query builder for ORM query channel
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="qr">Query channel</param>
        /// <returns>Query builder</returns>
        public static IQueryFor<T> Get<T>(this Read<QueryChannel<Orm.Query>> qr) where T : class
        {
            var pr = qr.Aspect();
            return new QueryBuilder<T>(pr, qr);
        }

        /// <summary>
        /// Retrieves query builder for ORM query channel
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="qr">Query channel</param>
        /// <returns>Query builder</returns>
        public static IQueryable<T> All<T>(this Read<QueryChannel<Orm.Query>> qr) where T : class
        {
            var pr = qr.Aspect();
            return pr.GetSet<T>(qr);
        }

        /// <summary>
        /// Stores query description for testing purposes
        /// </summary>
        /// <typeparam name="T">Query item type</typeparam>
        /// <param name="q">Query</param>
        /// <param name="description">Description text</param>
        /// <returns>Fluent</returns>
        public static IQueryable<T> Describe<T>(this IQueryable<T> q, string description)
        {
            if (q is TracedQueryable<T> ht)
            {
                ht.Description.Description = description;
            }

            return q;
        }

        /// <summary>
        /// Retrieves primary key of just added entity
        /// </summary>
        /// <typeparam name="T">Type of primary key</typeparam>
        /// <param name="qr">Channel</param>
        /// <param name="keyedAddition">Performed addition</param>
        /// <returns>Primary key</returns>
        public static T Key<T>(this Read<QueryChannel<Orm.Query>> qr, IAddition<IPrimaryKey<T>> keyedAddition)
        {
            var pr = qr.Aspect();
            return pr.Key(keyedAddition, qr);
        }
    }
}