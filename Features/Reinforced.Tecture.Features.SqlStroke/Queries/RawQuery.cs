using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Features.SqlStroke.Queries
{
    /// <summary>
    /// Raw SQL query
    /// </summary>
    public class RawQuery
    {
        internal string _description = string.Empty;

        private readonly TestData _qs;

        internal RawQuery(Sql sql, Query runtime, TestData qs)
        {
            Sql = sql;
            _runtime = runtime;
            _qs = qs;
        }

        public Sql Sql { get; }

        private readonly Query _runtime;

        public IEnumerable<T> As<T>() where T : class
        {

            if (_qs != null)
            {
                if (_qs is Collecting data)
                {
                    var compiled = _runtime.Compile(Sql);
                    var r = _runtime.DoQuery<T>(compiled.Query, compiled.Parameters);
                    data.Put(Sql.Hash(), r, _description);
                    return r;
                }

                if (_qs is Providing testData)
                {
                    return testData.Get<IEnumerable<T>>(Sql.Hash());
                }
            }

            var cq = _runtime.Compile(Sql);
            return _runtime.DoQuery<T>(cq.Query, cq.Parameters);
        }

        public async Task<IEnumerable<T>> AsAsync<T>() where T : class
        {

            if (_qs != null)
            {
                if (_qs is Collecting data)
                {
                    var compiled = _runtime.Compile(Sql);
                    var r = await _runtime.DoQueryAsync<T>(compiled.Query, compiled.Parameters);
                    data.Put(Sql.Hash(), r, _description);
                    return r;
                }

                if (_qs is Providing testData)
                {
                    return testData.Get<IEnumerable<T>>(Sql.Hash());
                }
            }

            var cq = _runtime.Compile(Sql);
            return await _runtime.DoQueryAsync<T>(cq.Query, cq.Parameters).ConfigureAwait(false);

        }
    }
}
