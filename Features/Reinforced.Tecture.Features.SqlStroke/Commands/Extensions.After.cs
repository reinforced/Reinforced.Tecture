using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.SqlStroke.Commands
{
    /// <summary>
    /// Sql Stroke extensions
    /// </summary>
    public static partial class Extensions
    {
        #region After

        /// <summary>
        /// Does SQL stroke to be executed after saving changes
        /// </summary>
        /// <typeparam name="T">Type of DB-mapped entity</typeparam>
        /// <param name="s">Write end of channel supporting SQL stroke</param>
        /// <param name="stroke">SQL Stroke expression</param>
        /// <returns>Sql stroke command</returns>
        public static Sql SqlStrokeAfter<T>(this Write<CommandChannel<Command>> s, Expression<Func<T, string>> stroke)
        {
            return After(s, stroke, typeof(T));
        }

        /// <summary>
        /// Does SQL stroke to be executed after saving changes
        /// </summary>
        /// <typeparam name="T1">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T2">Type of DB-mapped entity</typeparam>
        /// <param name="s">Write end of channel supporting SQL stroke</param>
        /// <param name="stroke">SQL Stroke expression</param>
        /// <returns>Sql stroke command</returns>
        public static Sql SqlStrokeAfter<T1, T2>(this Write<CommandChannel<Command>> s, Expression<Func<T1, T2, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2));
        }

        /// <summary>
        /// Does SQL stroke to be executed after saving changes
        /// </summary>
        /// <typeparam name="T1">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T2">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T3">Type of DB-mapped entity</typeparam>
        /// <param name="s">Write end of channel supporting SQL stroke</param>
        /// <param name="stroke">SQL Stroke expression</param>
        /// <returns>Sql stroke command</returns>
        public static Sql SqlStrokeAfter<T1, T2, T3>(this Write<CommandChannel<Command>> s, Expression<Func<T1, T2, T3, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        /// Does SQL stroke to be executed after saving changes
        /// </summary>
        /// <typeparam name="T1">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T2">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T3">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T4">Type of DB-mapped entity</typeparam>
        /// <param name="s">Write end of channel supporting SQL stroke</param>
        /// <param name="stroke">SQL Stroke expression</param>
        /// <returns>Sql stroke command</returns>

        public static Sql SqlStrokeAfter<T1, T2, T3, T4>(this Write<CommandChannel<Command>> s, Expression<Func<T1, T2, T3, T4, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        /// Does SQL stroke to be executed after saving changes
        /// </summary>
        /// <typeparam name="T1">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T2">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T3">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T4">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T5">Type of DB-mapped entity</typeparam>
        /// <param name="s">Write end of channel supporting SQL stroke</param>
        /// <param name="stroke">SQL Stroke expression</param>
        /// <returns>Sql stroke command</returns>
        public static Sql SqlStrokeAfter<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>> s, Expression<Func<T1, T2, T3, T4, T5, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        /// <summary>
        /// Does SQL stroke to be executed after saving changes
        /// </summary>
        /// <typeparam name="T1">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T2">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T3">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T4">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T5">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T6">Type of DB-mapped entity</typeparam>
        /// <param name="s">Write end of channel supporting SQL stroke</param>
        /// <param name="stroke">SQL Stroke expression</param>
        /// <returns>Sql stroke command</returns>
        public static Sql SqlStrokeAfter<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>> s, Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        /// <summary>
        /// Does SQL stroke to be executed after saving changes
        /// </summary>
        /// <typeparam name="T1">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T2">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T3">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T4">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T5">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T6">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T7">Type of DB-mapped entity</typeparam>
        /// <param name="s">Write end of channel supporting SQL stroke</param>
        /// <param name="stroke">SQL Stroke expression</param>
        /// <returns>Sql stroke command</returns>
        public static Sql SqlStrokeAfter<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>> s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6),typeof(T7));
        }

        /// <summary>
        /// Does SQL stroke to be executed after saving changes
        /// </summary>
        /// <typeparam name="T1">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T2">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T3">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T4">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T5">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T6">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T7">Type of DB-mapped entity</typeparam>
        /// <typeparam name="T8">Type of DB-mapped entity</typeparam>
        /// <param name="s">Write end of channel supporting SQL stroke</param>
        /// <param name="stroke">SQL Stroke expression</param>
        /// <returns>Sql stroke command</returns>
        public static Sql SqlStrokeAfter<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>> s, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke)
        {
            return After(s, stroke, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7),typeof(T8));
        }
        
        #endregion
    }
}
