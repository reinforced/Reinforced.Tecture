using System;
using System.Runtime.CompilerServices;

namespace Reinforced.Tecture.Services
{
    /// <summary>
    /// Await syntax tooling for actions queue
    /// </summary>
    public struct ActionsQueueAwaiter : INotifyCompletion
    {
        private readonly ActionsQueue _exp;

        internal ActionsQueueAwaiter(ActionsQueue exp)
        {
            _exp = exp;
            IsCompleted = false;
        }

        /// <summary>
        /// GetResult
        /// </summary>
        public void GetResult() { }

        /// <summary>
        /// Gets or sets whether awaited task was executed
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Enqueue task continuation
        /// </summary>
        /// <param name="continuation">Continuation</param>
        public void OnCompleted(Action continuation)
        {
            _exp.Enqueue(continuation);
            IsCompleted = true;
        }
    }

    /// <summary>
    /// Save task to be awaited
    /// </summary>
    public struct ActionsQueueTask
    {
        private readonly ActionsQueue _exp;

        internal ActionsQueueTask(ActionsQueue exp)
        {
            _exp = exp;
        }

        /// <summary>
        /// GetAwaiter
        /// </summary>
        /// <returns>Awaiter</returns>
        public ActionsQueueAwaiter GetAwaiter() => new ActionsQueueAwaiter(_exp);

        /// <summary>
        /// Enqueue action to be executed after saving
        /// </summary>
        /// <param name="action"></param>
        public void ContinueWith(Action action)
        {
            _exp.Enqueue(action);
        }
    }
}
