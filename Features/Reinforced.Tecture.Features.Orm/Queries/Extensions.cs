using System.Linq;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Features.Orm.Queries.Fake;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Queries
{
    public static partial class Extensions
    {
        /// <summary>
        /// Retrieves query builder for ORM query channel
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="qr">Query channel</param>
        /// <returns>Query builder</returns>
        public static IQueryFor<T> Get<T>(this Read<QueryChannel<Query>> qr) where T : class
        {
            var pr = qr.Feature();
            return new QueryBuilder<T>(pr);
        }

        /// <summary>
        /// Retrieves query builder for ORM query channel
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="qr">Query channel</param>
        /// <returns>Query builder</returns>
        public static IQueryable<T> All<T>(this Read<QueryChannel<Query>> qr) where T : class
        {
            var pr = qr.Feature();
            return pr.GetSet<T>();
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
            if (q is HookQueryable<T> ht)
            {
                ht._description.Description = description;
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
        public static T Key<T>(this Read<QueryChannel<Query>> qr, IAddition<IPrimaryKey<T>> keyedAddition)
        {
            var pr = qr.Feature();
            return pr.Key(keyedAddition);
        }

    }
}
