using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Strokes
{
    public static partial class Stroke
    {
        #region Stroke

        public static IEnumerable<TResult> QueryStroke<T, TResult>(this IStorage s, Expression<Func<T, string>> stroke, string annotation = null) where TResult : class
        {
            return RawStroke<TResult>(s, stroke, annotation);
        }

        public static IEnumerable<TResult> QueryStroke<T1, T2, TResult>(this IStorage s, Expression<Func<T1, T2, string>> stroke, string annotation = null) where TResult : class
        {
            return RawStroke<TResult>(s, stroke, annotation);
        }

        public static IEnumerable<TResult> QueryStroke<T1, T2, T3, TResult>(this IStorage s, Expression<Func<T1, T2, T3, string>> stroke, string annotation = null) where TResult : class
        {
            return RawStroke<TResult>(s, stroke, annotation);
        }

        public static IEnumerable<TResult> QueryStroke<T1, T2, T3, T4, TResult>(this IStorage s, Expression<Func<T1, T2, T3, T4, string>> stroke, string annotation = null) where TResult : class
        {
            return RawStroke<TResult>(s, stroke, annotation);
        }

        public static IEnumerable<TResult> QueryStroke<T1, T2, T3, T4, T5, TResult>(this IStorage s, Expression<Func<T1, T2, T3, T4, T5, string>> stroke, string annotation = null) where TResult : class
        {
            return RawStroke<TResult>(s, stroke, annotation);
        }

        public static IEnumerable<TResult> QueryStroke<T1, T2, T3, T4, T5, T6, TResult>(this IStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke, string annotation = null) where TResult : class
        {
            return RawStroke<TResult>(s, stroke, annotation);
        }

        public static IEnumerable<TResult> QueryStroke<T1, T2, T3, T4, T5, T6, T7, TResult>(this IStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke, string annotation = null) where TResult : class
        {
            return RawStroke<TResult>(s, stroke, annotation);
        }

        public static IEnumerable<TResult> QueryStroke<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this IStorage s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke, string annotation = null) where TResult : class
        {
            return RawStroke<TResult>(s, stroke, annotation);
        }

        private static IEnumerable<TResult> RawStroke<TResult>(this IStorage s, LambdaExpression expr, string annotation) where TResult : class
        {
            var p = s.RevealQuery(expr);
            return s.Query<TResult>(new DirectSqlSideEffect(p.CommandText, p.CommandParameters).Annotate(annotation));
        }

        #endregion
    }
}
