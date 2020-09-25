using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Tracing.Promises;

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
            var p = _a.Promise<IEnumerable<T>>();

            if (p is Containing<IEnumerable<T>> c)
                return c.Get(Sql.Hash(), _description);

            IEnumerable<T> result;
            var cq = _runtime.Tooling.Compile(Sql);
            using (var t = _a.GetQueryTransaction())
            {
                result = _runtime.DoQuery<T>(cq.Query, cq.Parameters);
                t.Commit();
            }

            if (p is Demanding<IEnumerable<T>> d)
                d.Fullfill(result, Sql.Hash(), _description);

            return result;
        }

        /// <summary>
        /// Serialize query results as collection of elements of type <typeparamref name="T"/> (async)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Query result</returns>
        public Task<IEnumerable<T>> AsAsync<T>() where T : class
        {
            var p = _a.Promise<IEnumerable<T>>();

            if (p is Containing<IEnumerable<T>> c)
            {
                return Task.FromResult(c.Get(Sql.Hash(), _description));
            }

            var cq = _runtime.Tooling.Compile(Sql);
            var t = _a.GetQueryTransaction();
            return _runtime.DoQueryAsync<T>(cq.Query, cq.Parameters).ContinueWith(v =>
            {
                t.Commit();
                t.Dispose();
                var res = v.Result;
                if (p is Demanding<IEnumerable<T>> d) d.Fullfill(res, Sql.Hash(), _description);
                return res;
            });
        }
    }
}
