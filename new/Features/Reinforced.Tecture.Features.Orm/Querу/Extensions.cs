using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Querу
{
    public static class Extensions
    {
        /// <summary>
        /// Retrieves query builder for ORM query channel
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="qr">Query channel</param>
        /// <returns>Query builder</returns>
        public static IQueryFor<T> Get<T>(this Read<QueryChannel<Orm>> qr) where T : class
        {
            var pr = qr.Feature();

            return new QueryBuilder<T>(pr);
        }
    }
}
