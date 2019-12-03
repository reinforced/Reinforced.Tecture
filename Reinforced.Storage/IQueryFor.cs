using System;
using System.Linq;
using System.Linq.Expressions;

namespace Reinforced.Storage
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
        /// Instructs query builder to extract aggregated entities
        /// </summary>
        /// <param name="include">Property or collection to include</param>
        /// <returns>Fluent</returns>
        IQueryFor<TEntity> Include<TProp>(Expression<Func<TEntity, TProp>> include);

        /// <summary>
        /// Instructs query constructor do not to add entities to ORM changes tracker
        /// </summary>
        /// <returns></returns>
        IQueryFor<TEntity> NoTracking();
       
        /// <summary>
        /// Instructs query builder also include some aggregates of another entities to query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="include"></param>
        /// <returns></returns>
        IQueryFor<TEntity> AlsoInclude<T>(Expression<Func<T, object>> include);
       
        /// <summary>
        /// Additional Where clause that will be added to "All" set
        /// </summary>
        /// <param name="where">Where expression</param>
        /// <returns>Fluent</returns>
        IQueryFor<TEntity> That(Expression<Func<TEntity, bool>> where);
    }
}
