

using Reinforced.Tecture.Aspects.Orm.PrimaryKey;

namespace Reinforced.Tecture.Aspects.Orm.Commands.DeletePk
{
    /// <summary>
    /// Primary key deletion extensions
    /// </summary>
    public static partial class Extensions
    {
     

        /// <summary>
        /// Deletes entity by primary key
        /// </summary> 
        /// <typeparam name="T1"></typeparam>          
        /// <param name="v1">Primary key field value</param> 
        /// <param name="c">Delete primary key operation</param>
        /// <returns>DeletePk command instance</returns>
        public static DeletePk ByPk<T1>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1>> c, T1 v1)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1);
        }
     

        /// <summary>
        /// Deletes entity by primary key
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>          
        /// <param name="v1">Primary key field value</param>  
        /// <param name="v2">Primary key field value</param> 
        /// <param name="c">Delete primary key operation</param>
        /// <returns>DeletePk command instance</returns>
        public static DeletePk ByPk<T1, T2>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2>> c, T1 v1, T2 v2)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2);
        }
     

        /// <summary>
        /// Deletes entity by primary key
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>          
        /// <param name="v1">Primary key field value</param>  
        /// <param name="v2">Primary key field value</param>  
        /// <param name="v3">Primary key field value</param> 
        /// <param name="c">Delete primary key operation</param>
        /// <returns>DeletePk command instance</returns>
        public static DeletePk ByPk<T1, T2, T3>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3>> c, T1 v1, T2 v2, T3 v3)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3);
        }
     

        /// <summary>
        /// Deletes entity by primary key
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>          
        /// <param name="v1">Primary key field value</param>  
        /// <param name="v2">Primary key field value</param>  
        /// <param name="v3">Primary key field value</param>  
        /// <param name="v4">Primary key field value</param> 
        /// <param name="c">Delete primary key operation</param>
        /// <returns>DeletePk command instance</returns>
        public static DeletePk ByPk<T1, T2, T3, T4>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3, T4>> c, T1 v1, T2 v2, T3 v3, T4 v4)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3, v4);
        }
     

        /// <summary>
        /// Deletes entity by primary key
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>          
        /// <param name="v1">Primary key field value</param>  
        /// <param name="v2">Primary key field value</param>  
        /// <param name="v3">Primary key field value</param>  
        /// <param name="v4">Primary key field value</param>  
        /// <param name="v5">Primary key field value</param> 
        /// <param name="c">Delete primary key operation</param>
        /// <returns>DeletePk command instance</returns>
        public static DeletePk ByPk<T1, T2, T3, T4, T5>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3, T4, T5>> c, T1 v1, T2 v2, T3 v3, T4 v4, T5 v5)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3, v4, v5);
        }
     

        /// <summary>
        /// Deletes entity by primary key
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>          
        /// <param name="v1">Primary key field value</param>  
        /// <param name="v2">Primary key field value</param>  
        /// <param name="v3">Primary key field value</param>  
        /// <param name="v4">Primary key field value</param>  
        /// <param name="v5">Primary key field value</param>  
        /// <param name="v6">Primary key field value</param> 
        /// <param name="c">Delete primary key operation</param>
        /// <returns>DeletePk command instance</returns>
        public static DeletePk ByPk<T1, T2, T3, T4, T5, T6>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3, T4, T5, T6>> c, T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3, v4, v5, v6);
        }
     

        /// <summary>
        /// Deletes entity by primary key
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>          
        /// <param name="v1">Primary key field value</param>  
        /// <param name="v2">Primary key field value</param>  
        /// <param name="v3">Primary key field value</param>  
        /// <param name="v4">Primary key field value</param>  
        /// <param name="v5">Primary key field value</param>  
        /// <param name="v6">Primary key field value</param>  
        /// <param name="v7">Primary key field value</param> 
        /// <param name="c">Delete primary key operation</param>
        /// <returns>DeletePk command instance</returns>
        public static DeletePk ByPk<T1, T2, T3, T4, T5, T6, T7>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>> c, T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3, v4, v5, v6, v7);
        }
        }
}

