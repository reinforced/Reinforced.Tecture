using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Query;

// ReSharper disable PossibleMultipleEnumeration

namespace Reinforced.Tecture.Aspects.DirectSql.Queries
{
    /// <summary>
    /// Raw SQL query
    /// </summary>
    public class RawQuery
    {
        internal string _description = string.Empty;

        private readonly Auxilary _a;

        internal RawQuery(Sql sql, Query runtime)
        {
            Sql = sql;
            _runtime = runtime;
            _a = runtime.Aux;
        }

        /// <summary>
        /// SQL command to be executed in order to obtain query results
        /// </summary>
        public Sql Sql { get; }

        private readonly Query _runtime;

        /// <summary>
        /// Serialize query results as collection of elements of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Query result</returns>
        public IEnumerable<T> As<T>() where T : class
        {
            IEnumerable<T> result;
            if (_a.IsEvaluationNeeded)
            {
                var cq = _runtime.Tooling.Compile(Sql);
                using (var t = _a.GetQueryTransaction())
                {
                    result = _runtime.DoQuery<T>(cq.Query, cq.Parameters);
                    t.Commit();
                }
            }
            else
            {
                result = _a.Get<IEnumerable<T>>(Sql.Hash(), _description);
            }

            if (_a.IsTracingNeeded)
            {
                _a.Query(Sql.Hash(), result, _description);
            }

            return result;
        }

        /// <summary>
        /// Serialize query results as collection of elements of type <typeparamref name="T"/> (async)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Query result</returns>
        public async Task<IEnumerable<T>> AsAsync<T>() where T : class
        {

            IEnumerable<T> result;
            if (_a.IsEvaluationNeeded)
            {
                var cq = _runtime.Tooling.Compile(Sql);
                using (var t = _a.GetQueryTransaction())
                {
                    result = await _runtime.DoQueryAsync<T>(cq.Query, cq.Parameters);
                    t.Commit();
                }
            }
            else
            {
                result = _a.Get<IEnumerable<T>>(Sql.Hash(), _description);
            }

            if (_a.IsTracingNeeded)
            {
                _a.Query(Sql.Hash(), result, _description);
            }

            return result;
        }
    }
}
