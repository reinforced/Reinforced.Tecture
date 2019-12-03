using System.Collections.Generic;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Testing.Stories;

namespace Reinforced.Storage.Testing.SideEffects
{
    /// <summary>
    /// Class foe validation/simulation of bulk operations
    /// </summary>
    /// <typeparam name="T">Bulk uploading entity type</typeparam>
    public abstract class BulkSimulation<T>
    {
        /// <summary>
        /// Set of data supplied for upload
        /// </summary>
        public IEnumerable<T> Data { get; internal set; }

        /// <summary>
        /// Access to test collections
        /// </summary>
        public ICollectionProvider Collections { get; internal set; }

        /// <summary>
        /// Direct SQL runner
        /// </summary>
        public ISideEffectRunner<DirectSqlSideEffect> Sql { get; internal set; }

        /// <summary>
        /// Handler for SQL commands being supplied to database
        /// </summary>
        /// <param name="sql">SQL command</param>
        /// <returns>Integer as if SQL had been executed</returns>
        public virtual int HandleCommand(DirectSqlSideEffect sql) => 0;

        /// <summary>
        /// Bulk operation SQL story validator
        /// </summary>
        /// <param name="sqlStory">SQL story to validate</param>
        public virtual void ValidateSqlStory(StorageStory sqlStory) { }
    }
}