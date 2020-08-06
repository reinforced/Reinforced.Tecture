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

        public IEnumerable<T> As<T>()
        {
            if (_qs != null)
            {
                if (_qs is Collecting data)
                {
                    var r = _runtime.DoQuery<T>(Sql.Command, Sql.Parameters);
                    data.Put(Sql.Hash(), r, _description);
                    return r;
                }

                if (_qs is Providing testData)
                {
                    return testData.Get<IEnumerable<T>>(Sql.Hash());
                }
            }
            return _runtime.DoQuery<T>(Sql.Command, Sql.Parameters);
        }

        public async Task<IEnumerable<T>> AsAsync<T>()
        {
            if (_qs != null)
            {
                if (_qs is Collecting data)
                {
                    var r = await _runtime.DoQueryAsync<T>(Sql.Command, Sql.Parameters);
                    data.Put(Sql.Hash(), r, _description);
                    return r;
                }

                if (_qs is Providing testData)
                {
                    return testData.Get<IEnumerable<T>>(Sql.Hash());
                }
            }
            return await _runtime.DoQueryAsync<T>(Sql.Command, Sql.Parameters).ConfigureAwait(false);
        }
    }
}
