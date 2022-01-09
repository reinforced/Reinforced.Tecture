using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Tracing.Commands.Cycles;
// ReSharper disable ArrangeAccessorOwnerBody


namespace Reinforced.Tecture.Services
{
    public partial class TectureServiceBase : IDisposable
    {
        internal TectureServiceBase() { }

        #region Auto-injected

        internal ServiceManager ServiceManager;
        internal ChannelMultiplexer ChannelMultiplexer;
        internal Pipeline Pipeline;
        internal TestingContextContainer Aux;

        internal void CallOnSave() => OnSave();
        internal void CallOnFinally(Exception exceptionHappened) => OnFinally(exceptionHappened);
        internal Task CallOnSaveAsync() => OnSaveAsync();
        internal Task CallOnFinallyAsync(Exception exceptionHappened) => OnFinallyAsync(exceptionHappened);
        internal void CallInit() => Init();
        #endregion

        /// <summary>
        /// Determines whether particular channel is bound or not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected bool IsBound<T>() where T : Channel => ChannelMultiplexer.IsKnown(typeof(T));

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data before save changes call. Use await Save; if necessary
        /// </summary>
        protected virtual void OnSave() { }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data after all save changes calls.
        /// </summary>
        protected virtual void OnFinally(Exception exceptionHappened) { }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data before save changes call. Use await Save; if necessary
        /// </summary>
#pragma warning disable 1998
        protected virtual async Task OnSaveAsync() { }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data after all save changes calls.
        /// </summary>
        protected virtual async Task OnFinallyAsync(Exception exceptionHappened) { }
#pragma warning restore 1998
        /// <summary>
        /// Called right after service initialization. Use it to do things right after service is created
        /// </summary>
        protected virtual void Init() { }
        
        /// <summary>
        /// Called right before service disposal
        /// </summary>
        protected virtual void OnDispose(){ }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            OnDispose();
            ServiceManager.DestroyService(this);
        }

        /// <summary>
        /// Comments some activity. Comment goes directly to pipeline queue as fake side-effect
        /// </summary>
        /// <param name="comment">Comment text</param>
        [Unexplainable]
        protected void Comment(string comment)
        {
            Pipeline.Enqueue(new Comment() { Annotation = comment });
        }

        /// <summary>
        /// Begins cycle description region
        /// </summary>
        /// <param name="annotation">Cycle begin annotation</param>
        /// <returns>Cycle context</returns>
        protected ICycleTraceContext Cycle(string annotation = null)
        {
            if (Aux.TraceCollector != null)
            {
                return new CycleTraceContext(Pipeline, annotation);
            }

            return new FakeCycleTraceContext();
        }

        /// <summary>
        /// Traverses set of items with correct cycle declaration in commands trace
        /// </summary>
        /// <param name="source">Set of element to traverse</param>
        /// <param name="description">Cycle description</param>
        /// <param name="action">Action to be done on each element</param>
        /// <typeparam name="T">Element type</typeparam>
        protected void ForEach<T>(IEnumerable<T> source, string description, Action<T> action)
        {
            using (var cc = Cycle(description))
            {
                foreach (var element in source)
                {
                    action(element);
                    cc.Iteration();
                }
            }
        }
        
        /// <summary>
        /// Traverses set of items with correct cycle declaration in commands trace
        /// </summary>
        /// <param name="source">Set of element to traverse</param>
        /// <param name="description">Cycle description</param>
        /// <param name="action">Action to be done on each element</param>
        /// <typeparam name="T">Element type</typeparam>
        protected async Task ForEach<T>(IEnumerable<T> source, string description, Func<T,Task> action)
        {
            using (var cc = Cycle(description))
            {
                foreach (var element in source)
                {
                    await action(element);
                    cc.Iteration();
                }
            }
        }
    }



}
