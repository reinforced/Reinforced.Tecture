using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.SqlStroke.Query
{
    public static partial class Extensions
    {
        public static RawQuery SqlQuery<T>(this Read<QueryChannel<DirectSql>> s, Expression<Func<T, string>> stroke)
        {
            return QueryCore(s, stroke, typeof(T));
        }
        public static RawQuery SqlQuery<T1, T2>(this Read<QueryChannel<DirectSql>> s, Expression<Func<T1, T2, string>> stroke)
        {
            return QueryCore(s, stroke, typeof(T1), typeof(T2));
        }

        public static RawQuery SqlQuery<T1, T2, T3>(this Read<QueryChannel<DirectSql>> s, Expression<Func<T1, T2, T3, string>> stroke)
        {
            return QueryCore(s, stroke, typeof(T1), typeof(T2), typeof(T3));
        }

        public static RawQuery SqlQuery<T1, T2, T3, T4>(this Read<QueryChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, string>> stroke)
        {
            return QueryCore(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static RawQuery SqlQuery<T1, T2, T3, T4, T5>(this Read<QueryChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, T5, string>> stroke)
        {
            return QueryCore(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        public static RawQuery SqlQuery<T1, T2, T3, T4, T5, T6>(this Read<QueryChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke)
        {
            return QueryCore(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        public static RawQuery SqlQuery<T1, T2, T3, T4, T5, T6, T7>(this Read<QueryChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke)
        {
            return QueryCore(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        }

        public static RawQuery SqlQuery<T1, T2, T3, T4, T5, T6, T7, T8>(this Read<QueryChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke)
        {
            return QueryCore(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
        }

    }
}
