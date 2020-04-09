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

        public static Sql SqlStroke<T>(this ServicePipeline s, Expression<Func<T, string>> stroke)
        {
            return Before(s, stroke, typeof(T));
        }
        public static Sql SqlStroke<T1, T2>(this ServicePipeline s, Expression<Func<T1, T2, string>> stroke)
        {
            return Before(s, stroke, typeof(T1), typeof(T2));
        }

        public static Sql SqlStroke<T1, T2, T3>(this ServicePipeline s, Expression<Func<T1, T2, T3, string>> stroke)
        {
            return Before(s, stroke, typeof(T1), typeof(T2), typeof(T3));
        }

        public static Sql SqlStroke<T1, T2, T3, T4>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, string>> stroke)
        {
            return Before(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static Sql SqlStroke<T1, T2, T3, T4, T5>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, T5, string>> stroke)
        {
            return Before(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        public static Sql SqlStroke<T1, T2, T3, T4, T5, T6>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke)
        {
            return Before(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        public static Sql SqlStroke<T1, T2, T3, T4, T5, T6, T7>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke)
        {
            return Before(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        }

        public static Sql SqlStroke<T1, T2, T3, T4, T5, T6, T7, T8>(this ServicePipeline s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke)
        {
            return Before(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
        }
        
        #endregion
    }
}
