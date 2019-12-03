using System;
using System.Linq.Expressions;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Strokes
{
    public static partial class Stroke
    {
        #region Stroke

        public static DirectSqlSideEffect SqlStroke<T>(this IStorage s, Expression<Func<T, string>> stroke)
        {
            return RawStroke(s, stroke);
        }

        public static DirectSqlSideEffect SqlStroke<T1, T2>(this IStorage s, Expression<Func<T1, T2, string>> stroke)
        {
            return RawStroke(s, stroke);
        }

        public static DirectSqlSideEffect SqlStroke<T1, T2, T3>(this IStorage s, Expression<Func<T1, T2, T3, string>> stroke)
        {
            return RawStroke(s, stroke);
        }

        public static DirectSqlSideEffect SqlStroke<T1, T2, T3, T4>(this IStorage s, Expression<Func<T1, T2, T3, T4, string>> stroke)
        {
            return RawStroke(s, stroke);
        }

        public static DirectSqlSideEffect SqlStroke<T1, T2, T3, T4, T5>(this IStorage s, Expression<Func<T1, T2, T3, T4, T5, string>> stroke)
        {
            return RawStroke(s, stroke);
        }

        public static DirectSqlSideEffect SqlStroke<T1, T2, T3, T4, T5, T6>(this IStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke)
        {
            return RawStroke(s, stroke);
        }

        public static DirectSqlSideEffect SqlStroke<T1, T2, T3, T4, T5, T6, T7>(this IStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke)
        {
            return RawStroke(s, stroke);
        }

        public static DirectSqlSideEffect SqlStroke<T1, T2, T3, T4, T5, T6, T7, T8>(this IStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke)
        {
            return RawStroke(s, stroke);
        }

        private static DirectSqlSideEffect RawStroke(this IStorage s, LambdaExpression expr)
        {
            var p = s.RevealQuery(expr);
            return new DirectSqlSideEffect(p.CommandText,p.CommandParameters);
        }

        #endregion
    }
}
