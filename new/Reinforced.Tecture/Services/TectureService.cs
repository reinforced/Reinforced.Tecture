using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Commands.Exact;
using Reinforced.Tecture.Queries;


namespace Reinforced.Tecture.Services
{
    public partial class TectureService : IDisposable
    {
        #region Auto-injected
        internal ServiceManager ServiceManager { get; set; }
        #endregion

        /// <summary>
        /// Await point to split actions before/after savechanges call
        /// </summary>
        protected ActionsQueueTask Save
        {
            get { return Pipeline.Save; }
        }

        /// <summary>
        /// Await point to split actions that must happen after everything
        /// </summary>
        protected ActionsQueueTask Final
        {
            get { return Pipeline.Final; }
        }

        internal void CallOnSave() { OnSave(); }
        internal void CallOnFinally() { OnFinally(); }

        internal Task CallOnSaveAsync() { return OnSaveAsync(); }
        internal Task CallOnFinallyAsync() { return OnFinallyAsync(); }

        internal virtual ServicePipeline Pipeline { get; private set; }

        internal virtual void CallInit(Pipeline pipeline)
        {
            Pipeline = new ServicePipeline(pipeline);
            Init();
        }

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
        protected virtual async Task OnSaveAsync() { }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data after all save changes calls.
        /// </summary>
        protected virtual async Task OnFinallyAsync() { }

        /// <summary>
        /// Called right after service initialization. Use it to do things right after service is created
        /// </summary>
        protected virtual void Init() { }

        protected T From<T>() where T : class, ISource
        {
            return Pipeline.CorePipeline._locator.GetSource<T>();
        }

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
            Pipeline.Enqueue(new CommentCommand() { Annotation = comment });
        }
    }



}
