using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Tracing.Promises;
using static Reinforced.Tecture.Aspects.DirectSql.DirectSql;

// ReSharper disable PossibleMultipleEnumeration

namespace Reinforced.Tecture.Aspects.DirectSql.Queries
{
    /// <summary>
    /// Raw SQL query
    /// </summary>
    public class RawQuery
    {
        internal string _description = string.Empty;

        private readonly TestingContext _a;
        private readonly Read _read;

        internal RawQuery(Sql sql, Query runtime, Read read)
        {
            Sql = sql;
            _runtime = runtime;
            _read = read;
            _a = runtime.Context;
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
            var p = _a.Promise<IEnumerable<T>>(_read);

            if (p is Containing<IEnumerable<T>> c)
                return c.Get(Sql.Hash(), _description);

            IEnumerable<T> result;
            var cq = _runtime.Tooling.Compile(Sql);
            try
            {
                using (var t = _a.GetQueryTransaction())
                {
                    result = _runtime.DoQuery<T>(cq.Query, cq.Parameters);
                    if (p is NotifyCompleted<IEnumerable<T>> nc) nc.Fulfill($"{_description}. {cq.Query}");
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                if (p is Catching<IEnumerable<T>> de)
                    de.Fulfill(ex, $"{_description}. {cq.Query}");
                throw;
            }

            if (p is Demanding<IEnumerable<T>> d)
                d.Fulfill(result, Sql.Hash(), _description);

            return result;
        }

        /// <summary>
        /// Serialize query results as collection of elements of type <typeparamref name="T"/> (async)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Query result</returns>
        public Task<IEnumerable<T>> AsAsync<T>(CancellationToken token=default) where T : class
        {
            var p = _a.Promise<IEnumerable<T>>(_read);

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
                if (v.Exception != null)
                {
                    if (p is Catching<IEnumerable<T>> de)
                        de.Fulfill(v.Exception,$"{_description}. {cq.Query}");
                    return v.Result;
                }
                else
                {
                    var res = v.Result;
                    if (p is NotifyCompleted<IEnumerable<T>> nc)
                        nc.Fulfill($"{_description}. {cq.Query}");
                    
                    if (p is Demanding<IEnumerable<T>> d) d.Fulfill(res, Sql.Hash(), _description);
                    return v.Result;
                }
            }, token);
        }
    }
}
