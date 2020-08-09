using System;
using System.Threading.Tasks;
using Reinforced.Storage.Testing.Stories;

namespace Reinforced.Storage.Services
{
    /// <summary>
    /// Base class for storage service
    /// </summary>
    public partial class StorageService
    {
        /// <summary>
        /// Defers action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        [Unexplainable]
        protected virtual void Finally(Action action)
        {
            FinallyActions.Enqueue(action);
        }

        /// <summary>
        /// Defers action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        [Unexplainable]
        protected virtual void Finally(Func<Task> action)
        {
            FinallyActions.Enqueue(action);
        }
    }
}
