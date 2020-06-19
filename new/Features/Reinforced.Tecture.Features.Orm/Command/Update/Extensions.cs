using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Methodics.Orm.Commands.Update
{
    public static partial class Extensions
    {
        private static Update UpdateCore(ServicePipeline ppl, object entity, LambdaExpression[] properties)
        {
            if (entity == null)
                throw new TectureOrmFeatureException("Entity going to be updated cannot be null");

            var t = entity.GetType();

            if (!ppl.IsSubject(t))
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for updating in corresponding service");

            return ppl.Enqueue(new Update(entity, t, properties));
        }

        private static Update UpdateCore(ServicePipeline ppl, object entity)
        {
            if (entity == null)
                throw new TectureOrmFeatureException("Entity going to be updated cannot be null");

            var t = entity.GetType();

            if (!ppl.IsSubject(t))
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for updating in corresponding service");

            return ppl.Enqueue(new Update(entity, t));
        }


        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <returns>Update command instance</returns>
        public static Update Update<TEntity>(this ServicePipeline<TEntity> pipeline, TEntity entity)
        {
            return UpdateCore(pipeline, entity);
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <returns>Update command instance</returns>
        public static Update Update<TEntity>(this ServicePipeline<TEntity> pipeline, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            return UpdateCore(pipeline, entity, properties);
        }
    }
}
