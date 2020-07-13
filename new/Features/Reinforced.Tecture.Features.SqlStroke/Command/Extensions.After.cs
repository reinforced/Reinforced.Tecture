﻿using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.SqlStroke.Command
{
    public static partial class Extensions
    {
        #region After

        public static Sql SqlStrokeAfter<T>(this Write<CommandChannel<DirectSql>> s, Expression<Func<T, string>> stroke)
        {
            return After(s, stroke, typeof(T));
        }
        public static Sql SqlStrokeAfter<T1, T2>(this Write<CommandChannel<DirectSql>> s, Expression<Func<T1, T2, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2));
        }

        public static Sql SqlStrokeAfter<T1, T2, T3>(this Write<CommandChannel<DirectSql>> s, Expression<Func<T1, T2, T3, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3));
        }

        public static Sql SqlStrokeAfter<T1, T2, T3, T4>(this Write<CommandChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static Sql SqlStrokeAfter<T1, T2, T3, T4, T5>(this Write<CommandChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, T5, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        public static Sql SqlStrokeAfter<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        public static Sql SqlStrokeAfter<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),typeof(T7));
        }

        public static Sql SqlStrokeAfter<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<DirectSql>> s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7),typeof(T8));
        }
        
        #endregion
    }
}
