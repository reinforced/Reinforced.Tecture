using System;
using System.Linq.Expressions;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Testing.Stories;

namespace Reinforced.Storage.Services
{
    /// <summary>
    /// Base class for storage service
    /// </summary>
    public partial class StorageService
    {
        #region Before

        [Unexplainable]
        protected DirectSqlSideEffect Sql(Expression<Func<string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        protected DirectSqlSideEffect Sql<T>(Expression<Func<T, string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        protected DirectSqlSideEffect Sql<T1, T2>(Expression<Func<T1, T2, string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        protected DirectSqlSideEffect Sql<T1, T2, T3>(Expression<Func<T1, T2, T3, string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        protected DirectSqlSideEffect Sql<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        protected DirectSqlSideEffect Sql<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        protected DirectSqlSideEffect Sql<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        protected DirectSqlSideEffect Sql<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        protected DirectSqlSideEffect Sql<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, string>> stroke)
        {
            return QueueSql(stroke);
        }
        [Unexplainable]
        private DirectSqlSideEffect QueueSql(LambdaExpression expr)
        {
            object[] pars = null;
            var p = ControlledReveal(expr, out pars);
            return Sql(p, pars);
        }



        #endregion
    }
}
