

using System;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Features.Orm.Commands.Add;

namespace Reinforced.Tecture.Features.Orm.PrimaryKey
{

 

	public interface IPrimaryKey<T1, T2>
	{
		(Expression<Func<T1>>, Expression<Func<T2>>) PrimaryKey { get; }
	}
 

	public interface IPrimaryKey<T1, T2, T3>
	{
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>) PrimaryKey { get; }
	}
 

	public interface IPrimaryKey<T1, T2, T3, T4>
	{
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>) PrimaryKey { get; }
	}
 

	public interface IPrimaryKey<T1, T2, T3, T4, T5>
	{
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>) PrimaryKey { get; }
	}
 

	public interface IPrimaryKey<T1, T2, T3, T4, T5, T6>
	{
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>) PrimaryKey { get; }
	}
 

	public interface IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>
	{
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>, Expression<Func<T7>>) PrimaryKey { get; }
	}
 

	public interface IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>
	{
		(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>, Expression<Func<T7>>, Expression<Func<T8>>) PrimaryKey { get; }
	}

	public static partial class Extensions
	{
        
 
		       
        public static (T1, T2) Key<T1, T2>(this IPrimaryKey<T1, T2> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2));
        }
 
		       
        public static (T1, T2, T3) Key<T1, T2, T3>(this IPrimaryKey<T1, T2, T3> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3));
        }
 
		       
        public static (T1, T2, T3, T4) Key<T1, T2, T3, T4>(this IPrimaryKey<T1, T2, T3, T4> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4));
        }
 
		       
        public static (T1, T2, T3, T4, T5) Key<T1, T2, T3, T4, T5>(this IPrimaryKey<T1, T2, T3, T4, T5> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4),Value<T5>(k, ks.Item5));
        }
 
		       
        public static (T1, T2, T3, T4, T5, T6) Key<T1, T2, T3, T4, T5, T6>(this IPrimaryKey<T1, T2, T3, T4, T5, T6> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4),Value<T5>(k, ks.Item5),Value<T6>(k, ks.Item6));
        }
 
		       
        public static (T1, T2, T3, T4, T5, T6, T7) Key<T1, T2, T3, T4, T5, T6, T7>(this IPrimaryKey<T1, T2, T3, T4, T5, T6, T7> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4),Value<T5>(k, ks.Item5),Value<T6>(k, ks.Item6),Value<T7>(k, ks.Item7));
        }
 
		       
        public static (T1, T2, T3, T4, T5, T6, T7, T8) Key<T1, T2, T3, T4, T5, T6, T7, T8>(this IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8> k)
        {
            var ks = k.PrimaryKey;
            
            return (Value<T1>(k, ks.Item1),Value<T2>(k, ks.Item2),Value<T3>(k, ks.Item3),Value<T4>(k, ks.Item4),Value<T5>(k, ks.Item5),Value<T6>(k, ks.Item6),Value<T7>(k, ks.Item7),Value<T8>(k, ks.Item8));
        }
	}
}

