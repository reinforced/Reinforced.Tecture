using System;
using System.Linq.Expressions;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Strokes
{
    public static partial class Stroke
    {
        #region Before

        public static DirectSqlSideEffect SqlStrokeBefore<T>(this IModifyableStorage s, Expression<Func<T, string>> stroke)
        {
            return Before(s, stroke);
        }
        public static DirectSqlSideEffect SqlStrokeBefore<T1, T2>(this IModifyableStorage s, Expression<Func<T1, T2, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static DirectSqlSideEffect SqlStrokeBefore<T1, T2, T3>(this IModifyableStorage s, Expression<Func<T1, T2, T3, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static DirectSqlSideEffect SqlStrokeBefore<T1, T2, T3, T4>(this IModifyableStorage s, Expression<Func<T1, T2, T3, T4, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static DirectSqlSideEffect SqlStrokeBefore<T1, T2, T3, T4, T5>(this IModifyableStorage s, Expression<Func<T1, T2, T3, T4, T5, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static DirectSqlSideEffect SqlStrokeBefore<T1, T2, T3, T4, T5, T6>(this IModifyableStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static DirectSqlSideEffect SqlStrokeBefore<T1, T2, T3, T4, T5, T6, T7>(this IModifyableStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static DirectSqlSideEffect SqlStrokeBefore<T1, T2, T3, T4, T5, T6, T7, T8>(this IModifyableStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke)
        {
            return Before(s, stroke);
        }


        private static DirectSqlSideEffect Before(this IModifyableStorage s, LambdaExpression expr)
        {
            var p = s.RevealQuery(expr);
            return s.DeferDirectSql(p.CommandText, p.CommandParameters);
        }

        

        #endregion
    }
}
