

using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Features.Orm.Commands.Add;

namespace Reinforced.Tecture.Features.Orm.PrimaryKey
{

 

		public interface IPrimaryKey<T1, T2>
		{
			(Expression<Func<T1>>, Expression<Func<T2>>) PrimaryKey { get; }
		}		

		public class Expectation<T1, T2>
        {
            private readonly IAddition<IPrimaryKey<T1, T2>> _addition;
            internal Expectation(IAddition<IPrimaryKey<T1, T2>> addition)
            {
                _addition = addition;
            }
            public (T1, T2) Key
            {
                get { return _addition.Key(); }
            }
        }
 

		public interface IPrimaryKey<T1, T2, T3>
		{
			(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>) PrimaryKey { get; }
		}		

		public class Expectation<T1, T2, T3>
        {
            private readonly IAddition<IPrimaryKey<T1, T2, T3>> _addition;
            internal Expectation(IAddition<IPrimaryKey<T1, T2, T3>> addition)
            {
                _addition = addition;
            }
            public (T1, T2, T3) Key
            {
                get { return _addition.Key(); }
            }
        }
 

		public interface IPrimaryKey<T1, T2, T3, T4>
		{
			(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>) PrimaryKey { get; }
		}		

		public class Expectation<T1, T2, T3, T4>
        {
            private readonly IAddition<IPrimaryKey<T1, T2, T3, T4>> _addition;
            internal Expectation(IAddition<IPrimaryKey<T1, T2, T3, T4>> addition)
            {
                _addition = addition;
            }
            public (T1, T2, T3, T4) Key
            {
                get { return _addition.Key(); }
            }
        }
 

		public interface IPrimaryKey<T1, T2, T3, T4, T5>
		{
			(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>) PrimaryKey { get; }
		}		

		public class Expectation<T1, T2, T3, T4, T5>
        {
            private readonly IAddition<IPrimaryKey<T1, T2, T3, T4, T5>> _addition;
            internal Expectation(IAddition<IPrimaryKey<T1, T2, T3, T4, T5>> addition)
            {
                _addition = addition;
            }
            public (T1, T2, T3, T4, T5) Key
            {
                get { return _addition.Key(); }
            }
        }
 

		public interface IPrimaryKey<T1, T2, T3, T4, T5, T6>
		{
			(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>) PrimaryKey { get; }
		}		

		public class Expectation<T1, T2, T3, T4, T5, T6>
        {
            private readonly IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6>> _addition;
            internal Expectation(IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6>> addition)
            {
                _addition = addition;
            }
            public (T1, T2, T3, T4, T5, T6) Key
            {
                get { return _addition.Key(); }
            }
        }
 

		public interface IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>
		{
			(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>, Expression<Func<T7>>) PrimaryKey { get; }
		}		

		public class Expectation<T1, T2, T3, T4, T5, T6, T7>
        {
            private readonly IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>> _addition;
            internal Expectation(IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>> addition)
            {
                _addition = addition;
            }
            public (T1, T2, T3, T4, T5, T6, T7) Key
            {
                get { return _addition.Key(); }
            }
        }
 

		public interface IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>
		{
			(Expression<Func<T1>>, Expression<Func<T2>>, Expression<Func<T3>>, Expression<Func<T4>>, Expression<Func<T5>>, Expression<Func<T6>>, Expression<Func<T7>>, Expression<Func<T8>>) PrimaryKey { get; }
		}		

		public class Expectation<T1, T2, T3, T4, T5, T6, T7, T8>
        {
            private readonly IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>> _addition;
            internal Expectation(IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>> addition)
            {
                _addition = addition;
            }
            public (T1, T2, T3, T4, T5, T6, T7, T8) Key
            {
                get { return _addition.Key(); }
            }
        }

	public static partial class Extensions
	{
 
		public static (T1, T2) Key<T1, T2>(this IAddition<IPrimaryKey<T1, T2>> cmd)
        {
            if (cmd is Add c)
            {
                if (c.PkData == null)
                    throw new TectureOrmFeatureException($"Primary key of just added {c.EntityType} could not be obtained. Addition did not run yet");

                return ((T1, T2))c.PkData;
            }

            throw new TectureOrmFeatureException($".Key call is valid only on add command");
        }

        public static Expectation<T1, T2> Expect<T1, T2>(this IAddition<IPrimaryKey<T1, T2>> cmd)
        {
            return new Expectation<T1, T2>(cmd);
        }
 
		public static (T1, T2, T3) Key<T1, T2, T3>(this IAddition<IPrimaryKey<T1, T2, T3>> cmd)
        {
            if (cmd is Add c)
            {
                if (c.PkData == null)
                    throw new TectureOrmFeatureException($"Primary key of just added {c.EntityType} could not be obtained. Addition did not run yet");

                return ((T1, T2, T3))c.PkData;
            }

            throw new TectureOrmFeatureException($".Key call is valid only on add command");
        }

        public static Expectation<T1, T2, T3> Expect<T1, T2, T3>(this IAddition<IPrimaryKey<T1, T2, T3>> cmd)
        {
            return new Expectation<T1, T2, T3>(cmd);
        }
 
		public static (T1, T2, T3, T4) Key<T1, T2, T3, T4>(this IAddition<IPrimaryKey<T1, T2, T3, T4>> cmd)
        {
            if (cmd is Add c)
            {
                if (c.PkData == null)
                    throw new TectureOrmFeatureException($"Primary key of just added {c.EntityType} could not be obtained. Addition did not run yet");

                return ((T1, T2, T3, T4))c.PkData;
            }

            throw new TectureOrmFeatureException($".Key call is valid only on add command");
        }

        public static Expectation<T1, T2, T3, T4> Expect<T1, T2, T3, T4>(this IAddition<IPrimaryKey<T1, T2, T3, T4>> cmd)
        {
            return new Expectation<T1, T2, T3, T4>(cmd);
        }
 
		public static (T1, T2, T3, T4, T5) Key<T1, T2, T3, T4, T5>(this IAddition<IPrimaryKey<T1, T2, T3, T4, T5>> cmd)
        {
            if (cmd is Add c)
            {
                if (c.PkData == null)
                    throw new TectureOrmFeatureException($"Primary key of just added {c.EntityType} could not be obtained. Addition did not run yet");

                return ((T1, T2, T3, T4, T5))c.PkData;
            }

            throw new TectureOrmFeatureException($".Key call is valid only on add command");
        }

        public static Expectation<T1, T2, T3, T4, T5> Expect<T1, T2, T3, T4, T5>(this IAddition<IPrimaryKey<T1, T2, T3, T4, T5>> cmd)
        {
            return new Expectation<T1, T2, T3, T4, T5>(cmd);
        }
 
		public static (T1, T2, T3, T4, T5, T6) Key<T1, T2, T3, T4, T5, T6>(this IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6>> cmd)
        {
            if (cmd is Add c)
            {
                if (c.PkData == null)
                    throw new TectureOrmFeatureException($"Primary key of just added {c.EntityType} could not be obtained. Addition did not run yet");

                return ((T1, T2, T3, T4, T5, T6))c.PkData;
            }

            throw new TectureOrmFeatureException($".Key call is valid only on add command");
        }

        public static Expectation<T1, T2, T3, T4, T5, T6> Expect<T1, T2, T3, T4, T5, T6>(this IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6>> cmd)
        {
            return new Expectation<T1, T2, T3, T4, T5, T6>(cmd);
        }
 
		public static (T1, T2, T3, T4, T5, T6, T7) Key<T1, T2, T3, T4, T5, T6, T7>(this IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>> cmd)
        {
            if (cmd is Add c)
            {
                if (c.PkData == null)
                    throw new TectureOrmFeatureException($"Primary key of just added {c.EntityType} could not be obtained. Addition did not run yet");

                return ((T1, T2, T3, T4, T5, T6, T7))c.PkData;
            }

            throw new TectureOrmFeatureException($".Key call is valid only on add command");
        }

        public static Expectation<T1, T2, T3, T4, T5, T6, T7> Expect<T1, T2, T3, T4, T5, T6, T7>(this IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>> cmd)
        {
            return new Expectation<T1, T2, T3, T4, T5, T6, T7>(cmd);
        }
 
		public static (T1, T2, T3, T4, T5, T6, T7, T8) Key<T1, T2, T3, T4, T5, T6, T7, T8>(this IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>> cmd)
        {
            if (cmd is Add c)
            {
                if (c.PkData == null)
                    throw new TectureOrmFeatureException($"Primary key of just added {c.EntityType} could not be obtained. Addition did not run yet");

                return ((T1, T2, T3, T4, T5, T6, T7, T8))c.PkData;
            }

            throw new TectureOrmFeatureException($".Key call is valid only on add command");
        }

        public static Expectation<T1, T2, T3, T4, T5, T6, T7, T8> Expect<T1, T2, T3, T4, T5, T6, T7, T8>(this IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>> cmd)
        {
            return new Expectation<T1, T2, T3, T4, T5, T6, T7, T8>(cmd);
        }
	}
}

