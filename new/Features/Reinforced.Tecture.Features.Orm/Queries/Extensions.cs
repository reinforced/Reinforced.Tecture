using Reinforced.Tecture.Channels;
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
            var pr = qr.Feature(out IQueryStore qs);
            pr.SetQueryStore(qs);
            return new QueryBuilder<T>(pr);
        }
    }
}
