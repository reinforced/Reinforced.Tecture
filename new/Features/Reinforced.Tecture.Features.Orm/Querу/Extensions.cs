using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Methodics.Orm.Queries;

namespace Reinforced.Tecture.Features.Orm.Queries
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
