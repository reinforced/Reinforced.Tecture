using System;
using System.Linq.Expressions;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage
{
    public partial class Storage
    {
        /// <summary>
        /// Adds entity to storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity to add</param>
        public AddSideEffect Add<TEntity>(TEntity entity) where TEntity : class
        {
            return _effectsQueue.Enqueue(new AddSideEffect() { Entity = entity, EntityType = typeof(TEntity) });
        }

        /// <summary>
        /// Deletes entity from storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity to remove from storage</param>
        public RemoveSideEffect Delete<TEntity>(TEntity entity) where TEntity : class
        {
            return _effectsQueue.Enqueue(new RemoveSideEffect() { Entity = entity, EntityType = typeof(TEntity) });
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity to update</param>
        public UpdateSideEffect Update<TEntity>(TEntity entity) where TEntity : class
        {
            return _effectsQueue.Enqueue(new UpdateSideEffect(entity, typeof(TEntity)));
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">Entity to update</param>
        /// <param name="properties">Properties to update</param>
        public UpdateSideEffect Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            return _effectsQueue.Enqueue(new UpdateSideEffect(entity, typeof(TEntity), properties));
        }
    }
}
