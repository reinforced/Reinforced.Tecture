using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Tracing;

// ReSharper disable PossibleMultipleEnumeration

namespace Reinforced.Tecture.Aspects.DirectSql.Queries
{
    /// <summary>
    /// Raw SQL query
    /// </summary>
    public class RawQuery
    {
        internal string _description = string.Empty;

        private readonly Auxiliary _a;

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
        public Task<IEnumerable<T>> AsAsync<T>() where T : class
        {
            PromisedResult<IEnumerable<T>> promised = null;
            if (_a.IsEvaluationNeeded)
            {

                if (_a.IsTracingNeeded)
                {
                    promised = _a.PromiseQuery<IEnumerable<T>>(Sql.Hash(), _description);
                }

                var cq = _runtime.Tooling.Compile(Sql);
                var t = _a.GetQueryTransaction();
                return _runtime.DoQueryAsync<T>(cq.Query, cq.Parameters).ContinueWith(v =>
                {
                    t.Commit();
                    t.Dispose();
                    var res = v.Result;
                    promised?.Fulfill(res, res.DeepClone());
                    return res;
                });
            }

            return Task.FromResult(_a.Get<IEnumerable<T>>(Sql.Hash(), _description));
        }
    }
}
