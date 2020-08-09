using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.Defaults.EntityFramework;

namespace Reinforced.Storage.Defaults.Redis
{
    public static class RedisCacheExtensions
    {
        public static T ById<T>(this EntityCache<T> c, int id) where T : class ,IEntity
        {
            return c.Get(string.Format("{0}#{1}", c.EntityName, id));
        }

        public async static Task<T> ByIdAsync<T>(this EntityCache<T> c, int id) where T : class ,IEntity
        {
            return await c.GetAsync(string.Format("{0}#{1}", c.EntityName, id));
        }

        public static T ByIdOrAdd<T>(this EntityCache<T> c, int id, TimeSpan? ttl = null) where T : class ,IEntity
        {
            return c.GetOrAdd(string.Format("#{0}", id), () => c.FromDatabase.ById(id), ttl);
        }

        public async static Task<T> ByIdOrAddAsync<T>(this EntityCache<T> c, int id, TimeSpan? ttl = null) where T : class ,IEntity
        {
            return await c.GetOrAddAsync(string.Format("#{0}", id), async () => await c.FromDatabase.ByIdAsync(id), ttl);
        }
    }
}
