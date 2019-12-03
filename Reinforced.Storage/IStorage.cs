using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reinforced.Storage.Services;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Strokes;

namespace Reinforced.Storage
{
    /// <summary>
    /// Storage interface
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Query storage interface
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Query builder</returns>
        IQueryFor<T> Get<T>() where T : class;

        /// <summary>
        /// Retrieves cache manager for specified entity. 
        /// Cacheable entity is not mandatory to be IEntity inheritor
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Cache manager</returns>
        EntityCache<T> Cached<T>() where T : class;

        /// <summary>
        /// Executes raw query and fetches data from it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL query to be executed</param>
        /// <returns>Set of results</returns>
        IEnumerable<T> Query<T>(DirectSqlSideEffect sql) where T : class;

        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        T Do<T>() where T : StorageService, INoContext;

        /// <summary>
        /// Obtains context service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Context service <typeparamref name="T"/></returns>
        LetBuilder<T> Let<T>() where T : StorageService, IWithContext;


        

        StrokeProcessor.RevealedQuery RevealQuery(LambdaExpression expr);

    }
}
