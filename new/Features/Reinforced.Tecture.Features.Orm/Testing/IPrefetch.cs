using System.Collections.Generic;
using System.Linq;

namespace Reinforced.Tecture.Methodics.Orm.Testing
{
    public interface IPrefetch
    {
        /// <summary>
        /// Adds prefetched data to test data collection
        /// </summary>
        IPrefetch Prefetched<T>(IEnumerable<T> collection);

        /// <summary>
        /// Adds prefetched data to test data collection
        /// </summary>
        IPrefetch Prefetched<T>(IQueryable<T> collection) where T : class;
    }
}