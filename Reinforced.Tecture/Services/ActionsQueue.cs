using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Reinforced.Tecture
{
    /// <summary>
    /// Queue of delegates to be executed
    /// </summary>
    public class ActionsQueue
    {
        private readonly Queue<object> _queue = new Queue<object>();
        private readonly bool _allowEnqueueWhileRunning;

        /// <summary>
        /// Gets whether queue contains asynchronous actions
        /// </summary>
        public bool HasAsyncActions { get; private set; }

        internal ActionsQueue(bool allowEnqueueWhileRunning)
        {
            _allowEnqueueWhileRunning = allowEnqueueWhileRunning;
        }

        /// <summary>
        /// Gets whether queue is being currently executed
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Defers action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        public void Enqueue(Action action)
        {
            if (IsRunning && !_allowEnqueueWhileRunning)
            {
                throw new Exception("Cannot enqueue more actions - one is already running");
            }
            _queue.Enqueue(action);
        }

        /// <summary>
        /// Defers async action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        public void Enqueue(Func<Task> action)
        {
            if (IsRunning && !_allowEnqueueWhileRunning)
            {
                throw new Exception("Cannot enqueue more actions - one is already running");
            }

            HasAsyncActions = true;
            _queue.Enqueue(action);
        }
        
        /// <summary>
        /// Defers async action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        public void Enqueue(Func<CancellationToken,Task> action)
        {
            if (IsRunning && !_allowEnqueueWhileRunning)
            {
                throw new Exception("Cannot enqueue more actions - one is already running");
            }

            HasAsyncActions = true;
            _queue.Enqueue(action);
        }

        internal void Run()
        {
            IsRunning = true;

            var allItems = new Queue<object>(_queue);
            _queue.Clear();
            while (allItems.Count > 0)
            {
                var act = allItems.Dequeue();
                if (act is Action a)
                {
                    a();
                    continue;
                }
                if (act is Func<Task>) throw new Exception("Cannot run async actions within sync SaveChanges");
            }

            IsRunning = false;
        }

        internal async Task RunAsync(CancellationToken token = default)
        {
            IsRunning = true;

            var allItems = new Queue<object>(_queue);
            _queue.Clear();
            while (allItems.Count > 0)
            {
                var act = allItems.Dequeue();
                if (act is Func<Task> a2) await a2();
                else if (act is Func<CancellationToken,Task> a3) await a3(token);
                else
                {
                    if (act is Action a) a();
                }
            }
            IsRunning = false;
        }
    }
}
