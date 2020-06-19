using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Methodics.Orm.Commands.Delete
{
    /// <summary>
    /// Delete extensions
    /// </summary>
    public static partial class Extensions
    {
        private static Delete DeleteCore(ServicePipeline ppl, object entity)
        {
            if (entity == null)
                throw new TectureOrmFeatureException("Entity going to be deleted cannot be null");

            var t = entity.GetType();

            if (!ppl.IsSubject(t))
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for deletion in corresponding service");

            return ppl.Enqueue(new Delete()
            {
                EntityType = t,
                Entity = entity
            });
        }

        /// <summary>
        /// Adds entity to storage
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <returns>Add command instance</returns>
        public static Delete Delete<TEntity>(this ServicePipeline<TEntity> pipeline, TEntity entity)
        {
            return DeleteCore(pipeline, entity);
        }
    }
}
