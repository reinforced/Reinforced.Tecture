using System;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.Orm.Command.Update
{
    public static partial class Extensions
    {
     

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, TEntity>(this ServicePipeline<T1, TEntity> pipeline, TEntity entity)
        {
            return UpdateCore(pipeline, entity);
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, TEntity>(this ServicePipeline<T1, TEntity> pipeline, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            return UpdateCore(pipeline, entity, properties);
        }
     

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, TEntity>(this ServicePipeline<T1, T2, TEntity> pipeline, TEntity entity)
        {
            return UpdateCore(pipeline, entity);
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, TEntity>(this ServicePipeline<T1, T2, TEntity> pipeline, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            return UpdateCore(pipeline, entity, properties);
        }
     

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, TEntity>(this ServicePipeline<T1, T2, T3, TEntity> pipeline, TEntity entity)
        {
            return UpdateCore(pipeline, entity);
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, TEntity>(this ServicePipeline<T1, T2, T3, TEntity> pipeline, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            return UpdateCore(pipeline, entity, properties);
        }
     

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, T4, TEntity>(this ServicePipeline<T1, T2, T3, T4, TEntity> pipeline, TEntity entity)
        {
            return UpdateCore(pipeline, entity);
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, T4, TEntity>(this ServicePipeline<T1, T2, T3, T4, TEntity> pipeline, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            return UpdateCore(pipeline, entity, properties);
        }
     

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
        /// <typeparam name="T5">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, T4, T5, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, TEntity> pipeline, TEntity entity)
        {
            return UpdateCore(pipeline, entity);
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
        /// <typeparam name="T5">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, T4, T5, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, TEntity> pipeline, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            return UpdateCore(pipeline, entity, properties);
        }
     

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
        /// <typeparam name="T5">Not used</typeparam> 
        /// <typeparam name="T6">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, T4, T5, T6, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, T6, TEntity> pipeline, TEntity entity)
        {
            return UpdateCore(pipeline, entity);
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
        /// <typeparam name="T5">Not used</typeparam> 
        /// <typeparam name="T6">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, T4, T5, T6, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, T6, TEntity> pipeline, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            return UpdateCore(pipeline, entity, properties);
        }
     

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
        /// <typeparam name="T5">Not used</typeparam> 
        /// <typeparam name="T6">Not used</typeparam> 
        /// <typeparam name="T7">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, T4, T5, T6, T7, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, T6, T7, TEntity> pipeline, TEntity entity)
        {
            return UpdateCore(pipeline, entity);
        }

        /// <summary>
        /// Updates entity in storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
        /// <typeparam name="T5">Not used</typeparam> 
        /// <typeparam name="T6">Not used</typeparam> 
        /// <typeparam name="T7">Not used</typeparam> 
                
        /// <returns>Update command instance</returns>
        public static UpdateCommand Update<T1, T2, T3, T4, T5, T6, T7, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, T6, T7, TEntity> pipeline, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            return UpdateCore(pipeline, entity, properties);
        }
    
    }
}