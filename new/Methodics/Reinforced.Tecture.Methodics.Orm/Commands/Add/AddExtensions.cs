using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Methodics.Orm.Commands.Add
{
    public static partial class Extensions
    {
     

        /// <summary>
        /// Adds entity to storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
                
        /// <returns>Add command instance</returns>
        public static AddCommand Add<T1, TEntity>(this ServicePipeline<T1, TEntity> pipeline, TEntity entity)
        {
            return AddCore(pipeline, entity);
        }
     

        /// <summary>
        /// Adds entity to storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
                
        /// <returns>Add command instance</returns>
        public static AddCommand Add<T1, T2, TEntity>(this ServicePipeline<T1, T2, TEntity> pipeline, TEntity entity)
        {
            return AddCore(pipeline, entity);
        }
     

        /// <summary>
        /// Adds entity to storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
                
        /// <returns>Add command instance</returns>
        public static AddCommand Add<T1, T2, T3, TEntity>(this ServicePipeline<T1, T2, T3, TEntity> pipeline, TEntity entity)
        {
            return AddCore(pipeline, entity);
        }
     

        /// <summary>
        /// Adds entity to storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
                
        /// <returns>Add command instance</returns>
        public static AddCommand Add<T1, T2, T3, T4, TEntity>(this ServicePipeline<T1, T2, T3, T4, TEntity> pipeline, TEntity entity)
        {
            return AddCore(pipeline, entity);
        }
     

        /// <summary>
        /// Adds entity to storage
        /// </summary>
        /// <param name="pipeline">Tecture pipeline</param>
        /// <param name="entity">Entity</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T1">Not used</typeparam> 
        /// <typeparam name="T2">Not used</typeparam> 
        /// <typeparam name="T3">Not used</typeparam> 
        /// <typeparam name="T4">Not used</typeparam> 
        /// <typeparam name="T5">Not used</typeparam> 
                
        /// <returns>Add command instance</returns>
        public static AddCommand Add<T1, T2, T3, T4, T5, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, TEntity> pipeline, TEntity entity)
        {
            return AddCore(pipeline, entity);
        }
     

        /// <summary>
        /// Adds entity to storage
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
                
        /// <returns>Add command instance</returns>
        public static AddCommand Add<T1, T2, T3, T4, T5, T6, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, T6, TEntity> pipeline, TEntity entity)
        {
            return AddCore(pipeline, entity);
        }
     

        /// <summary>
        /// Adds entity to storage
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
                
        /// <returns>Add command instance</returns>
        public static AddCommand Add<T1, T2, T3, T4, T5, T6, T7, TEntity>(this ServicePipeline<T1, T2, T3, T4, T5, T6, T7, TEntity> pipeline, TEntity entity)
        {
            return AddCore(pipeline, entity);
        }
    
    }
}
