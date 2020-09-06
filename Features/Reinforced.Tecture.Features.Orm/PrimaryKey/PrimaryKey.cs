

using System;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.Orm.PrimaryKey
{

 

    /// <summary>
    /// Entity defining compound primary key of 2 fields
    /// </summary> 
    /// <typeparam name="T1">Primary key component #1</typeparam>  
    /// <typeparam name="T2">Primary key component #2</typeparam>     
	public interface IPrimaryKey<T1, T2>
	{
        /// <summary>
        /// Gets tuple of property expressions participating compound primary key
        /// </summary>
		(Expression<Func<T1>>, Expression<Func<T2>>) PrimaryKey { get; }
	}
 

    /// <summary>
    /// Entity defining compound primary key of 3 fields
    /// </summary> 
    /// <typeparam name="T1">Primary key component #1</typeparam>  
    /// <typeparam name="T2">Primary key component #2</typeparam>  
    /// <typeparam name="T3">Primary key component #3</typeparam>     
	public interface IPrimaryKey<T1, T2, T3>
	{
        /// <summary>
        /// Gets tuple of property expressions participating compound primary key
        /// </summary>
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>) PrimaryKey { get; }
	}
 

    /// <summary>
    /// Entity defining compound primary key of 4 fields
    /// </summary> 
    /// <typeparam name="T1">Primary key component #1</typeparam>  
    /// <typeparam name="T2">Primary key component #2</typeparam>  
    /// <typeparam name="T3">Primary key component #3</typeparam>  
    /// <typeparam name="T4">Primary key component #4</typeparam>     
	public interface IPrimaryKey<T1, T2, T3, T4>
	{
        /// <summary>
        /// Gets tuple of property expressions participating compound primary key
        /// </summary>
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>) PrimaryKey { get; }
	}
 

    /// <summary>
    /// Entity defining compound primary key of 5 fields
    /// </summary> 
    /// <typeparam name="T1">Primary key component #1</typeparam>  
    /// <typeparam name="T2">Primary key component #2</typeparam>  
    /// <typeparam name="T3">Primary key component #3</typeparam>  
    /// <typeparam name="T4">Primary key component #4</typeparam>  
    /// <typeparam name="T5">Primary key component #5</typeparam>     
	public interface IPrimaryKey<T1, T2, T3, T4, T5>
	{
        /// <summary>
        /// Gets tuple of property expressions participating compound primary key
        /// </summary>
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>) PrimaryKey { get; }
	}
 

    /// <summary>
    /// Entity defining compound primary key of 6 fields
    /// </summary> 
    /// <typeparam name="T1">Primary key component #1</typeparam>  
    /// <typeparam name="T2">Primary key component #2</typeparam>  
    /// <typeparam name="T3">Primary key component #3</typeparam>  
    /// <typeparam name="T4">Primary key component #4</typeparam>  
    /// <typeparam name="T5">Primary key component #5</typeparam>  
    /// <typeparam name="T6">Primary key component #6</typeparam>     
	public interface IPrimaryKey<T1, T2, T3, T4, T5, T6>
	{
        /// <summary>
        /// Gets tuple of property expressions participating compound primary key
        /// </summary>
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>) PrimaryKey { get; }
	}
 

    /// <summary>
    /// Entity defining compound primary key of 7 fields
    /// </summary> 
    /// <typeparam name="T1">Primary key component #1</typeparam>  
    /// <typeparam name="T2">Primary key component #2</typeparam>  
    /// <typeparam name="T3">Primary key component #3</typeparam>  
    /// <typeparam name="T4">Primary key component #4</typeparam>  
    /// <typeparam name="T5">Primary key component #5</typeparam>  
    /// <typeparam name="T6">Primary key component #6</typeparam>  
    /// <typeparam name="T7">Primary key component #7</typeparam>     
	public interface IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>
	{
        /// <summary>
        /// Gets tuple of property expressions participating compound primary key
        /// </summary>
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>, Expression<Func<T7>>) PrimaryKey { get; }
	}
 

    /// <summary>
    /// Entity defining compound primary key of 8 fields
    /// </summary> 
    /// <typeparam name="T1">Primary key component #1</typeparam>  
    /// <typeparam name="T2">Primary key component #2</typeparam>  
    /// <typeparam name="T3">Primary key component #3</typeparam>  
    /// <typeparam name="T4">Primary key component #4</typeparam>  
    /// <typeparam name="T5">Primary key component #5</typeparam>  
    /// <typeparam name="T6">Primary key component #6</typeparam>  
    /// <typeparam name="T7">Primary key component #7</typeparam>  
    /// <typeparam name="T8">Primary key component #8</typeparam>     
	public interface IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>
	{
        /// <summary>
        /// Gets tuple of property expressions participating compound primary key
        /// </summary>
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>, Expression<Func<T7>>, Expression<Func<T8>>) PrimaryKey { get; }
	}

    /// <summary>
    /// Extensions for entities with compound primary key
    /// </summary>
	public static partial class Extensions
	{
        
 
		      
        /// <summary>
        /// Gets tuple of compound primary key values consisting of 2 values
        /// </summary> 
        /// <typeparam name="T1">Primary key component #1</typeparam>  
        /// <typeparam name="T2">Primary key component #2</typeparam>     
        /// <param name="k">Entity with compound primary key</param>
        /// <returns>Compound primary key tuple</returns>
        public static (T1, T2) Key<T1, T2>(this IPrimaryKey<T1, T2> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2));
        }
 
		      
        /// <summary>
        /// Gets tuple of compound primary key values consisting of 3 values
        /// </summary> 
        /// <typeparam name="T1">Primary key component #1</typeparam>  
        /// <typeparam name="T2">Primary key component #2</typeparam>  
        /// <typeparam name="T3">Primary key component #3</typeparam>     
        /// <param name="k">Entity with compound primary key</param>
        /// <returns>Compound primary key tuple</returns>
        public static (T1, T2, T3) Key<T1, T2, T3>(this IPrimaryKey<T1, T2, T3> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3));
        }
 
		      
        /// <summary>
        /// Gets tuple of compound primary key values consisting of 4 values
        /// </summary> 
        /// <typeparam name="T1">Primary key component #1</typeparam>  
        /// <typeparam name="T2">Primary key component #2</typeparam>  
        /// <typeparam name="T3">Primary key component #3</typeparam>  
        /// <typeparam name="T4">Primary key component #4</typeparam>     
        /// <param name="k">Entity with compound primary key</param>
        /// <returns>Compound primary key tuple</returns>
        public static (T1, T2, T3, T4) Key<T1, T2, T3, T4>(this IPrimaryKey<T1, T2, T3, T4> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4));
        }
 
		      
        /// <summary>
        /// Gets tuple of compound primary key values consisting of 5 values
        /// </summary> 
        /// <typeparam name="T1">Primary key component #1</typeparam>  
        /// <typeparam name="T2">Primary key component #2</typeparam>  
        /// <typeparam name="T3">Primary key component #3</typeparam>  
        /// <typeparam name="T4">Primary key component #4</typeparam>  
        /// <typeparam name="T5">Primary key component #5</typeparam>     
        /// <param name="k">Entity with compound primary key</param>
        /// <returns>Compound primary key tuple</returns>
        public static (T1, T2, T3, T4, T5) Key<T1, T2, T3, T4, T5>(this IPrimaryKey<T1, T2, T3, T4, T5> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4),Value<T5>(k, ks.Item5));
        }
 
		      
        /// <summary>
        /// Gets tuple of compound primary key values consisting of 6 values
        /// </summary> 
        /// <typeparam name="T1">Primary key component #1</typeparam>  
        /// <typeparam name="T2">Primary key component #2</typeparam>  
        /// <typeparam name="T3">Primary key component #3</typeparam>  
        /// <typeparam name="T4">Primary key component #4</typeparam>  
        /// <typeparam name="T5">Primary key component #5</typeparam>  
        /// <typeparam name="T6">Primary key component #6</typeparam>     
        /// <param name="k">Entity with compound primary key</param>
        /// <returns>Compound primary key tuple</returns>
        public static (T1, T2, T3, T4, T5, T6) Key<T1, T2, T3, T4, T5, T6>(this IPrimaryKey<T1, T2, T3, T4, T5, T6> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4),Value<T5>(k, ks.Item5),Value<T6>(k, ks.Item6));
        }
 
		      
        /// <summary>
        /// Gets tuple of compound primary key values consisting of 7 values
        /// </summary> 
        /// <typeparam name="T1">Primary key component #1</typeparam>  
        /// <typeparam name="T2">Primary key component #2</typeparam>  
        /// <typeparam name="T3">Primary key component #3</typeparam>  
        /// <typeparam name="T4">Primary key component #4</typeparam>  
        /// <typeparam name="T5">Primary key component #5</typeparam>  
        /// <typeparam name="T6">Primary key component #6</typeparam>  
        /// <typeparam name="T7">Primary key component #7</typeparam>     
        /// <param name="k">Entity with compound primary key</param>
        /// <returns>Compound primary key tuple</returns>
        public static (T1, T2, T3, T4, T5, T6, T7) Key<T1, T2, T3, T4, T5, T6, T7>(this IPrimaryKey<T1, T2, T3, T4, T5, T6, T7> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4),Value<T5>(k, ks.Item5),Value<T6>(k, ks.Item6),Value<T7>(k, ks.Item7));
        }
 
		      
        /// <summary>
        /// Gets tuple of compound primary key values consisting of 8 values
        /// </summary> 
        /// <typeparam name="T1">Primary key component #1</typeparam>  
        /// <typeparam name="T2">Primary key component #2</typeparam>  
        /// <typeparam name="T3">Primary key component #3</typeparam>  
        /// <typeparam name="T4">Primary key component #4</typeparam>  
        /// <typeparam name="T5">Primary key component #5</typeparam>  
        /// <typeparam name="T6">Primary key component #6</typeparam>  
        /// <typeparam name="T7">Primary key component #7</typeparam>  
        /// <typeparam name="T8">Primary key component #8</typeparam>     
        /// <param name="k">Entity with compound primary key</param>
        /// <returns>Compound primary key tuple</returns>
        public static (T1, T2, T3, T4, T5, T6, T7, T8) Key<T1, T2, T3, T4, T5, T6, T7, T8>(this IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4),Value<T5>(k, ks.Item5),Value<T6>(k, ks.Item6),Value<T7>(k, ks.Item7),Value<T8>(k, ks.Item8));
        }
	}
}

