using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Aspects.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Queries
{
    public static class CommonQueries
    {
        public static Task<T> ByIdAsync<T>(this IQueryFor<T> q, int id)
            where T : IEntity
        {
            var r = q.All.Describe($"Get {typeof(T).Name} by Id #{id}").Where(x => x.Id == id);
            return r.FirstOrDefaultAsync();
        }
    }
}
