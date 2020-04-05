using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Methodics.Orm.Commands.Add
{
    /// <summary>
    /// Addition extensions
    /// </summary>
    public static partial class Extensions
    {
        private static AddCommand AddCore(ServicePipeline ppl, object entity)
        {
            if (entity==null)
                throw new TectureOrmMethodicsException("Entity going to be added cannot be null");

            var t = entity.GetType();

            if (!ppl.IsSubject(t)) 
                throw new TectureOrmMethodicsException($"Entity {entity} is not a subject for addition in corresponding service");

            return ppl.Enqueue(new AddCommand()
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
        public static AddCommand Add<TEntity>(this ServicePipeline<TEntity> pipeline, TEntity entity)
        {
            return AddCore(pipeline, entity);
        }
    }
}
