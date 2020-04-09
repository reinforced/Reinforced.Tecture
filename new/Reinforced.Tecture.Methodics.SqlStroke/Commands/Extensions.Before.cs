using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Methodics.SqlStroke.Commands
{
    public static partial class Extensions
    {
        #region Before

        public static SqlCommand SqlStrokeBefore<T>(this ServicePipeline s, Expression<Func<T, string>> stroke)
        {
            return Before(s, stroke);
        }
        public static SqlCommand SqlStrokeBefore<T1, T2>(this ServicePipeline s, Expression<Func<T1, T2, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static SqlCommand SqlStrokeBefore<T1, T2, T3>(this ServicePipeline s, Expression<Func<T1, T2, T3, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static SqlCommand SqlStrokeBefore<T1, T2, T3, T4>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static SqlCommand SqlStrokeBefore<T1, T2, T3, T4, T5>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, T5, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static SqlCommand SqlStrokeBefore<T1, T2, T3, T4, T5, T6>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static SqlCommand SqlStrokeBefore<T1, T2, T3, T4, T5, T6, T7>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke)
        {
            return Before(s, stroke);
        }

        public static SqlCommand SqlStrokeBefore<T1, T2, T3, T4, T5, T6, T7, T8>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke)
        {
            return Before(s, stroke);
        }
        private static SqlCommand Before(this ServicePipeline s, LambdaExpression expr)
        {
            var p = s.RevealQuery(expr);
            var cmd = new SqlCommand(p.CommandText, p.CommandParameters);
            return s.Enqueue(cmd);
        }
        #endregion
    }
}
