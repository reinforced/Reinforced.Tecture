using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Querу
{
    public static class Extensions
    {
        public static IQueryFor<T> Get<T>(this Read<QueryChannel<Orm>> qr) where T : class
        {
            var pr = qr.Feature();

            return new QueryBuilder<T>(pr);
        }
    }
}
