

using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Query;
namespace Reinforced.Tecture.Features.Orm.Queries
{
    public static partial class Extensions
    {
         
        /// <summary>
        /// Retrieves primary key of just added entity
        /// </summary>
        /// <typeparam name="T1">Type of key component #1</typeparam>
        /// <typeparam name="T2">Type of key component #2</typeparam>
        /// <param name="qr">Channel</param>
        /// <param name="keyedAddition">Performed addition</param>
        /// <returns>Primary key</returns>
        public static (T1, T2) Key<T1, T2>(this Read<QueryChannel<Query>> qr, IAddition<IPrimaryKey<T1, T2>> keyedAddition)
        {
            var pr = qr.Feature();
            return pr.Key(keyedAddition);
        }
         
        /// <summary>
        /// Retrieves primary key of just added entity
        /// </summary>
        /// <typeparam name="T1">Type of key component #1</typeparam>
        /// <typeparam name="T2">Type of key component #2</typeparam>
        /// <typeparam name="T3">Type of key component #3</typeparam>
        /// <param name="qr">Channel</param>
        /// <param name="keyedAddition">Performed addition</param>
        /// <returns>Primary key</returns>
        public static (T1, T2, T3) Key<T1, T2, T3>(this Read<QueryChannel<Query>> qr, IAddition<IPrimaryKey<T1, T2, T3>> keyedAddition)
        {
            var pr = qr.Feature();
            return pr.Key(keyedAddition);
        }
         
        /// <summary>
        /// Retrieves primary key of just added entity
        /// </summary>
        /// <typeparam name="T1">Type of key component #1</typeparam>
        /// <typeparam name="T2">Type of key component #2</typeparam>
        /// <typeparam name="T3">Type of key component #3</typeparam>
        /// <typeparam name="T4">Type of key component #4</typeparam>
        /// <param name="qr">Channel</param>
        /// <param name="keyedAddition">Performed addition</param>
        /// <returns>Primary key</returns>
        public static (T1, T2, T3, T4) Key<T1, T2, T3, T4>(this Read<QueryChannel<Query>> qr, IAddition<IPrimaryKey<T1, T2, T3, T4>> keyedAddition)
        {
            var pr = qr.Feature();
            return pr.Key(keyedAddition);
        }
         
        /// <summary>
        /// Retrieves primary key of just added entity
        /// </summary>
        /// <typeparam name="T1">Type of key component #1</typeparam>
        /// <typeparam name="T2">Type of key component #2</typeparam>
        /// <typeparam name="T3">Type of key component #3</typeparam>
        /// <typeparam name="T4">Type of key component #4</typeparam>
        /// <typeparam name="T5">Type of key component #5</typeparam>
        /// <param name="qr">Channel</param>
        /// <param name="keyedAddition">Performed addition</param>
        /// <returns>Primary key</returns>
        public static (T1, T2, T3, T4, T5) Key<T1, T2, T3, T4, T5>(this Read<QueryChannel<Query>> qr, IAddition<IPrimaryKey<T1, T2, T3, T4, T5>> keyedAddition)
        {
            var pr = qr.Feature();
            return pr.Key(keyedAddition);
        }
         
        /// <summary>
        /// Retrieves primary key of just added entity
        /// </summary>
        /// <typeparam name="T1">Type of key component #1</typeparam>
        /// <typeparam name="T2">Type of key component #2</typeparam>
        /// <typeparam name="T3">Type of key component #3</typeparam>
        /// <typeparam name="T4">Type of key component #4</typeparam>
        /// <typeparam name="T5">Type of key component #5</typeparam>
        /// <typeparam name="T6">Type of key component #6</typeparam>
        /// <param name="qr">Channel</param>
        /// <param name="keyedAddition">Performed addition</param>
        /// <returns>Primary key</returns>
        public static (T1, T2, T3, T4, T5, T6) Key<T1, T2, T3, T4, T5, T6>(this Read<QueryChannel<Query>> qr, IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6>> keyedAddition)
        {
            var pr = qr.Feature();
            return pr.Key(keyedAddition);
        }
         
        /// <summary>
        /// Retrieves primary key of just added entity
        /// </summary>
        /// <typeparam name="T1">Type of key component #1</typeparam>
        /// <typeparam name="T2">Type of key component #2</typeparam>
        /// <typeparam name="T3">Type of key component #3</typeparam>
        /// <typeparam name="T4">Type of key component #4</typeparam>
        /// <typeparam name="T5">Type of key component #5</typeparam>
        /// <typeparam name="T6">Type of key component #6</typeparam>
        /// <typeparam name="T7">Type of key component #7</typeparam>
        /// <param name="qr">Channel</param>
        /// <param name="keyedAddition">Performed addition</param>
        /// <returns>Primary key</returns>
        public static (T1, T2, T3, T4, T5, T6, T7) Key<T1, T2, T3, T4, T5, T6, T7>(this Read<QueryChannel<Query>> qr, IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>> keyedAddition)
        {
            var pr = qr.Feature();
            return pr.Key(keyedAddition);
        }
         
        /// <summary>
        /// Retrieves primary key of just added entity
        /// </summary>
        /// <typeparam name="T1">Type of key component #1</typeparam>
        /// <typeparam name="T2">Type of key component #2</typeparam>
        /// <typeparam name="T3">Type of key component #3</typeparam>
        /// <typeparam name="T4">Type of key component #4</typeparam>
        /// <typeparam name="T5">Type of key component #5</typeparam>
        /// <typeparam name="T6">Type of key component #6</typeparam>
        /// <typeparam name="T7">Type of key component #7</typeparam>
        /// <typeparam name="T8">Type of key component #8</typeparam>
        /// <param name="qr">Channel</param>
        /// <param name="keyedAddition">Performed addition</param>
        /// <returns>Primary key</returns>
        public static (T1, T2, T3, T4, T5, T6, T7, T8) Key<T1, T2, T3, T4, T5, T6, T7, T8>(this Read<QueryChannel<Query>> qr, IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>> keyedAddition)
        {
            var pr = qr.Feature();
            return pr.Key(keyedAddition);
        }
        
    }
}

