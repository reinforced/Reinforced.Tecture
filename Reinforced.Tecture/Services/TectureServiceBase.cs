using System;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;
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
        internal AuxiliaryContainer Aux;

        internal void CallOnSave() => OnSave();
        internal void CallOnFinally() => OnFinally();
        internal Task CallOnSaveAsync() => OnSaveAsync();
        internal Task CallOnFinallyAsync() => OnFinallyAsync();
        internal void CallInit() => Init();
        #endregion

        /// <summary>
        /// Determines whether particular channel is bound or not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected bool IsBound<T>() where T : Channel => ChannelMultiplexer.IsKnown(typeof(T));

        /// <summary>
        /// Await point to split actions before/after savechanges call
        /// </summary>
        protected ActionsQueueTask Save => new ActionsQueueTask(Pipeline.PostSaveActions);

        /// <summary>
        /// Await point to split actions that must happen after everything
        /// </summary>
        protected ActionsQueueTask Final => new ActionsQueueTask(Pipeline.FinallyActions);

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data before save changes call. Use await Save; if necessary
        /// </summary>
        protected virtual void OnSave() { }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data after all save changes calls.
        /// </summary>
        protected virtual void OnFinally() { }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data before save changes call. Use await Save; if necessary
        /// </summary>
#pragma warning disable 1998
        protected virtual async Task OnSaveAsync() { }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data after all save changes calls.
        /// </summary>
        protected virtual async Task OnFinallyAsync() { }
#pragma warning restore 1998
        /// <summary>
        /// Called right after service initialization. Use it to do things right after service is created
        /// </summary>
        protected virtual void Init() { }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
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
    }



}
