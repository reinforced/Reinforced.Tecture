using System;
using System.Linq;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.Orm.Querу
{
    public interface IQueryFor
    {
        /// <summary>
        /// Retrieves collection of another type from DataSource for Join operations
        /// </summary>
        /// <typeparam name="T">Another entity type</typeparam>
        /// <returns>Join-friendly collection</returns>
        IQueryable<T> Joined<T>() where T : class;
    }

    /// <summary>
    /// Query builder interface for entities. 
    /// All data source queries in system should go through this interface. 
    /// Please write extension methods for IQueryFor&lt;YourEntity&gt; and it will be accessible 
    /// via IStorage.Get&lt;YourEntity&gt;.YourQuery
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IQueryFor<TEntity> : IQueryFor
    {
        /// <summary>
        /// Retrieves query for all etities of current type presenting in data source
        /// </summary>
        IQueryable<TEntity> All { get; }

        /// <summary>
        /// Additional Where clause that will be added to "All" set
        /// </summary>
        /// <param name="where">Where expression</param>
        /// <returns>Fluent</returns>
        IQueryFor<TEntity> That(Expression<Func<TEntity, bool>> where);
    }
}
