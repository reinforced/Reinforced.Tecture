using System;
using System.Threading.Tasks;

namespace Reinforced.Tecture.Services
{
    /// <summary>
    /// Base class for storage service
    /// </summary>
    public partial class TectureServiceBase
    {
        /// <summary>
        /// Defers action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        [Unexplainable]
        protected virtual void Finally(Action action)
        {
            Pipeline.FinallyActions.Enqueue(action);
        }

        /// <summary>
        /// Defers action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        [Unexplainable]
        protected virtual void Finally(Func<Task> action)
        {
            Pipeline.FinallyActions.Enqueue(action);
        }
    }
}
