using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Reinforced.Storage.Services;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Transactions;

namespace Reinforced.Storage
{

    public interface IMasterStorage : IModifyableStorage
    {
        void SaveChanges();
        Task SaveChangesAsync();
        IOuterTransaction BeginOuterTransaction(OuterTransactionMode mode,
            OuterTransactionIsolationLevel isolationLevel = OuterTransactionIsolationLevel.ReadUncommitted);

    }
    /// <summary>
    /// Internal interface for modifyable storage
    /// Warning! This interface should be internal but it is public ONLY for unit testing purposes
    /// </summary>
    public interface IModifyableStorage : IStorage 
    {
        [Obsolete("Avoid using that")]
        Effects Effects { get; }

        SaveTask Save { get; }

        /// <summary>
        /// Adds entity to storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity to add</param>
        AddSideEffect Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Deletes entity from storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity to remove from storage</param>
        RemoveSideEffect Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity to update</param>
        UpdateSideEffect Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity to update</param>
        /// <param name="properties">Properties to update</param>
        UpdateSideEffect Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : class;

        /// <summary>
        /// Defers action that will be executed after .SaveChanges call.
        /// Warning! Dont use logic API that is using this method within bulk context. 
        /// Since EFBatchOperation has no support for resolving dangling IDs then calls to post-save actions 
        /// may lead to unexpected result. 
        /// To avoid this, any trying to perform post-save action in bulk mode will lead to exception.
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges completed</param>
        [Obsolete("Use await Save instead")]
        void QueuePostSaveAction(Action action);

        /// <summary>
        /// Defers async action that will be executed after .SaveChanges call.
        /// Warning! Dont use logic API that is using this method within bulk context. 
        /// Since EFBatchOperation has no support for resolving dangling IDs then calls to post-save actions 
        /// may lead to unexpected result. 
        /// To avoid this, any trying to perform post-save action in bulk mode will lead to exception.
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges completed</param>
        [Obsolete("Use await Save instead")]
        void QueueAsyncPostSaveAction(Func<Task> action);

        [Obsolete("Use Strokes")]
        void DeferDirectSqlBefore(string sql, params object[] parameters);

        [Obsolete("Use Strokes and await Save")]
        void DeferDirectSqlAfter(string sql, params object[] parameters);

        [Obsolete("Use IMasterStorage")]
        IOuterTransaction BeginOuterTransaction(OuterTransactionMode mode,
            OuterTransactionIsolationLevel isolationLevel = OuterTransactionIsolationLevel.ReadUncommitted);

        /// <summary>
        /// Defers action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        void Finally(Action action);

        /// <summary>
        /// Defers async action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        void FinallyAsync(Func<Task> action);

        /// <summary>
        /// Enqueues execution of direct SQL command BEFORE saving changes to DB
        /// this method is not suitable for fetching queries data.
        /// </summary>
        /// <param name="sql">SQL command</param>
        /// <param name="parameters">optional parameters</param>
        DirectSqlSideEffect DeferDirectSql(string sql, params object[] parameters);

        /// <summary>
        /// Saves changes
        /// </summary>
        [Obsolete("Just.. do not do so")]
        void SaveChanges();
        
        [Obsolete("Just.. do not do so")]
        Task SaveChangesAsync();

        void Defer(DirectSqlSideEffect cmd);
        
        /// <summary>
        /// Enqueues operations to be performed with bulk-uploaded data before SaveChanges is called
        /// </summary>
        /// <typeparam name="T">Type of entity line to bulk upload and change</typeparam>
        /// <param name="dataToUpload">Set of object to upload</param>
        /// <param name="bo">Actions to be done with uploaded data</param>
        BulkSideEffect DeferBulk<T>(IEnumerable<T> dataToUpload, Action<BulkOperator> bo);
    }

}
