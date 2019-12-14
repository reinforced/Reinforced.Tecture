using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Storage.Services;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture
{
    class ActionsQueue
    {
        internal ServiceManager _serviceManager;
        private readonly Queue<object> _queue = new Queue<object>();
        private bool _isQueueRunning = false;
        private readonly bool _allowEnqueueWhileRunning;

        public bool HasAsyncActions { get; private set; }
        public ActionsQueue(bool allowEnqueueWhileRunning)
        {
            _allowEnqueueWhileRunning = allowEnqueueWhileRunning;
        }
        public bool IsRunning => _isQueueRunning;

        /// <summary>
        /// Defers action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        public void Enqueue(Action action)
        {
            if (_isQueueRunning && !_allowEnqueueWhileRunning)
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
            if (_isQueueRunning && !_allowEnqueueWhileRunning)
            {
                throw new Exception("Cannot enqueue more actions - one is already running");
            }

            HasAsyncActions = true;
            _queue.Enqueue(action);
        }

        public void Run()
        {
            _isQueueRunning = true;

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
                if (act is Func<Task> a2) throw new Exception("Cannot run async actions within sync SaveChanges");
            }

            _isQueueRunning = false;
        }

        public async Task RunAsync()
        {
            _isQueueRunning = true;

            var allItems = new Queue<object>(_queue);
            _queue.Clear();
            while (allItems.Count > 0)
            {
                var act = allItems.Dequeue();
                if (act is Func<Task> a2) await a2();
                else
                {
                    if (act is Action a) a();
                }
            }
            _isQueueRunning = false;
        }
    }
}
