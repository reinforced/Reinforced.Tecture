using System.Linq;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.Orm.Queries.Fake;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Features.Orm.Queries
{
    public static class Extensions
    {
        /// <summary>
        /// Retrieves query builder for ORM query channel
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="qr">Query channel</param>
        /// <returns>Query builder</returns>
        public static IQueryFor<T> Get<T>(this Read<QueryChannel<Query>> qr) where T : class
        {
            var pr = qr.Feature(out TestData qs);
            pr.SetQueryStore(qs);
            return new QueryBuilder<T>(pr);
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
    }
}
