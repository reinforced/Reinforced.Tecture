using System;
using System.Runtime.CompilerServices;

namespace Reinforced.Storage.Services
{
    public struct SaveAwaiter : INotifyCompletion
    {
        private readonly ActionsQueue _exp;

        internal SaveAwaiter(ActionsQueue exp)
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
    public struct SaveTask
    {
        private readonly ActionsQueue _exp;

        internal SaveTask(ActionsQueue exp)
        {
            _exp = exp;
        }

        public SaveAwaiter GetAwaiter() => new SaveAwaiter(_exp);

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
