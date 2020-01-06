using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Commands.Exact;


namespace Reinforced.Tecture.Services
{
    public partial class TectureService : IDisposable
    {
        #region Auto-injected
        internal ActionsQueue PostSaveActions { get; set; }
        internal ActionsQueue FinallyActions { get; set; }
        internal ServiceManager ServiceManager { get; set; }
        #endregion

        /// <summary>
        /// Await point to split actions before/after savechanges call
        /// </summary>
        protected ActionsQueueTask Save
        {
            get { return new ActionsQueueTask(PostSaveActions); }
        }

        /// <summary>
        /// Await point to split actions that must happen after everything
        /// </summary>
        protected ActionsQueueTask Final
        {
            get { return new ActionsQueueTask(FinallyActions); }
        }

        internal void CallOnSave() { OnSave(); }
        internal void CallOnFinally() { OnFinally(); }

        internal  virtual  ServicePipeline Pipeline { get; private set; }

        internal virtual void CallInit(Pipeline pipeline)
        {
            Pipeline = new ServicePipeline(pipeline);
            Init();
        }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data before save changes call. Use await Save; if necessary
        /// </summary>
        protected virtual async void OnSave() { }

        /// <summary>
        /// Aggregating service pattern. Override this method to write aggregated data after all save changes calls.
        /// </summary>
        protected virtual void OnFinally() { }

        private void Validate<TEntity>() where TEntity : class
        {
            if (!EntitiesUsed.Contains(typeof(TEntity)))
            {
                throw new Exception($"{GetType().FullName} cannot add entity {typeof(TEntity).FullName} because has no power to do so");
            }
        }

        [Unexplainable]
        protected internal AddSideEffect ControlledAdd<TEntity>(TEntity entity) where TEntity : class
        {
            Validate<TEntity>();
            return Tecture.Commands.Pipeline.Enqueue(new AddSideEffect() { Entity = entity, EntityType = typeof(TEntity) });
        }

        [Unexplainable]
        protected internal RemoveSideEffect ControlledRemove<TEntity>(TEntity entity) where TEntity : class
        {
            Validate<TEntity>();
            return Tecture.Commands.Pipeline.Enqueue(new RemoveSideEffect() { Entity = entity, EntityType = typeof(TEntity) });
        }

        [Unexplainable]
        protected internal UpdateSideEffect ControlledUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            Validate<TEntity>();
            return Tecture.Commands.Pipeline.Enqueue(new UpdateSideEffect(entity, typeof(TEntity)));
        }

        [Unexplainable]
        protected internal UpdateSideEffect ControlledUpdate<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            Validate<TEntity>();
            return Tecture.Commands.Pipeline.Enqueue(new UpdateSideEffect(entity, typeof(TEntity), properties));
        }

        private string ControlledReveal(LambdaExpression expr, out object[] parameters)
        {
            var cmd = StrokeProcessor.RevealQuery(expr);
            foreach (var cmdUsedType in cmd.UsedTypes)
            {
                if (!EntitiesUsed.Contains(cmdUsedType))
                {
                    throw new Exception($"{GetType().FullName} cannot use entity {cmdUsedType.FullName} in SQL command because has no power to do so");
                }
            }

            parameters = cmd.CommandParameters;
            return cmd.CommandText;
        }

        /// <summary>
        /// Called right after service initialization. Use it to do things right after service is created
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Unexplainable]
        protected virtual DirectSqlSideEffect Sql(string command, params object[] parameters)
        {
            return Tecture.Commands.Pipeline.Enqueue(new DirectSqlSideEffect(command, parameters));
        }

        /// <summary>
        /// Query storage interface
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Query builder</returns>
        protected IQueryFor<T> Get<T>() where T : class
        {
            return SetManager.Get<T>();
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
            Pipeline.Enqueue(new CommentCommand() {Annotation = comment});
        }
    }



}
