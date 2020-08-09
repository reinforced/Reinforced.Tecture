using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Services
{
    /// <summary>
    /// Base class for storage service
    /// </summary>
    public partial class StorageService
    {
        /// <summary>
        /// Enqueues operations to be performed with bulk-uploaded data before SaveChanges is called
        /// </summary>
        /// <typeparam name="T">Type of entity line to bulk upload and change</typeparam>
        /// <param name="dataToUpload">Set of object to upload</param>
        /// <param name="bo">Actions to be done with uploaded data</param>
        protected BulkSideEffect DeferBulk<T>(IEnumerable<T> dataToUpload, Action<BulkOperator> bo)
        {
            return Effects.Enqueue(new BulkSideEffect(StrokeProcessor, EntitiesUsed)
                {Data = dataToUpload, ElementType = typeof(T), Actions = bo});
        }

        /// <summary>
        /// Enqueues operations to be performed with bulk-uploaded data before SaveChanges is called
        /// </summary>
        /// <typeparam name="T">Type of entity line to bulk upload and change</typeparam>
        /// <param name="dataToUpload">Set of object to upload</param>
        /// <param name="bo">Actions to be done with uploaded data</param>
        protected AsyncBulkSideEffect DeferBulkAsync<T>(IEnumerable<T> dataToUpload, Func<AsyncBulkOperator,Task> bo)
        {
            return Effects.Enqueue(new AsyncBulkSideEffect(StrokeProcessor, EntitiesUsed)
                { Data = dataToUpload, ElementType = typeof(T), Actions = bo });
        }
    }
}
