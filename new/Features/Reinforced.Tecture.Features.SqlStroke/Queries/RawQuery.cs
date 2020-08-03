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
        private readonly IQueryStore _qs;

        internal RawQuery(Sql sql, Query runtime, IQueryStore qs)
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
                if (_qs.State == QueryMemorizeState.Put)
                {
                    var r = _runtime.DoQuery<T>(Sql.Command, Sql.Parameters);
                    _qs.Put(Sql.Hash(),r);
                    return r;
                }
                else
                {
                    return _qs.Get<IEnumerable<T>>(Sql.Hash());
                }
            }
            return _runtime.DoQuery<T>(Sql.Command, Sql.Parameters);
        }

        public async Task<IEnumerable<T>> AsAsync<T>()
        {
            if (_qs != null)
            {
                if (_qs.State == QueryMemorizeState.Put)
                {
                    var r = await _runtime.DoQueryAsync<T>(Sql.Command, Sql.Parameters);
                    _qs.Put(Sql.Hash(), r);
                    return r;
                }
                else
                {
                    return _qs.Get<IEnumerable<T>>(Sql.Hash());
                }
            }
            return await _runtime.DoQueryAsync<T>(Sql.Command, Sql.Parameters).ConfigureAwait(false);
        }
    }
}
