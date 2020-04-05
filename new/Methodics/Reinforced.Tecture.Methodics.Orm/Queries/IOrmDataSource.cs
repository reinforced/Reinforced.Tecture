using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Methodics.Orm.Queries
{
    /// <summary>
    /// ORM methodics data source
    /// </summary>
    public interface IOrmSource : ISource
    {
        /// <summary>
        /// Retrieves query builder
        /// </summary>
        /// <typeparam name="T">Entity to query from</typeparam>
        /// <returns>Query builder</returns>
        IQueryFor<T> Get<T>() where T:class;
    }
}
