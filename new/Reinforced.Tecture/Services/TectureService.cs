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
        
        internal Pipeline _pipeline;
        protected Pipeline Q => _pipeline;
        #endregion

        /// <summary>
        /// Await point to split actions before/after savechanges call
        /// </summary>
        protected SaveTask Save { get => new SaveTask(PostSaveActions); }

        /// <summary>
        /// Set of entities types that are being touched by this service
        /// </summary> 
        public static IEnumerable<Type> UsedEntities { get { yield break; } }

        /// <summary>
        /// Set of entities types that are being touched by this service
        /// </summary> 
        protected virtual HashSet<Type> EntitiesUsed { get; } = new HashSet<Type>();

        internal void CallOnSave() { OnSave(); }
        internal void CallOnFinally() { OnFinally(); }
        internal void CallInit() { Init(); }

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
            Tecture.Commands.Pipeline.Enqueue(new CommentCommand() {Annotation = comment});
        }
    }


    /// <summary>
    /// Storage services that touches 1 entity
    /// </summary> 
    public class TectureService<T1> : TectureService
        where T1 : class
    {
        /// <summary>
        /// Adds entity <typeparamref name="T1"/>
        /// </summary>
        /// <param name="entity">Entity to be added to storage</param>
        [Unexplainable]
        protected virtual AddSideEffect Add(T1 entity) { return ControlledAdd(entity); }

        /// <summary>
        /// Updates entity <typeparamref name="T1"/>
        /// </summary>
        /// <param name="entity">Entity to be updated storage</param>
        [Unexplainable]
        protected virtual UpdateSideEffect Update(T1 entity) { return ControlledUpdate(entity); }

        /// <summary>
        /// Updates entity <typeparamref name="T1"/>
        /// </summary>
        /// <param name="entity">Entity to be updated storage</param>
        [Unexplainable]
        protected virtual UpdateSideEffect Update(T1 entity, params Expression<Func<T1, object>>[] properties) { return ControlledUpdate(entity, properties); }

        /// <summary>
        /// Removes entity <typeparamref name="T1"/>
        /// </summary>
        /// <param name="entity">Entity to be removed from storage</param>
        [Unexplainable]
        protected virtual RemoveSideEffect Remove(T1 entity) { return ControlledRemove(entity); }

        /// <summary>
        /// Set of entities types that are being touched by this service
        /// </summary>
        public new static IEnumerable<Type> UsedEntities { get { yield return typeof(T1); } }

        /// <summary>
        /// Set of entities types that are being touched by this service
        /// </summary> 
        protected override HashSet<Type> EntitiesUsed { get; } = new HashSet<Type>(new[] { typeof(T1) });
    }


}
