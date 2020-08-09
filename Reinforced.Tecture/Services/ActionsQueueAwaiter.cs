using System;
using System.Runtime.CompilerServices;

namespace Reinforced.Tecture.Services
{
    public struct ActionsQueueAwaiter : INotifyCompletion
    {
        private readonly ActionsQueue _exp;

        internal ActionsQueueAwaiter(ActionsQueue exp)
        {
            _exp = exp;
            IsCompleted = false;
        }

        public void GetResult() { }
        public bool IsCompleted { get; private set; }

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

        public ActionsQueueAwaiter GetAwaiter() => new ActionsQueueAwaiter(_exp);

        /// <summary>
        /// Enqueues action to be executed after saving
        /// </summary>
        /// <param name="action"></param>
        public void ContinueWith(Action action)
        {
            _exp.Enqueue(action);
        }
    }
}
