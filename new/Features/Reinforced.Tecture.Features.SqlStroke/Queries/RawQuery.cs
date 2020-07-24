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

            }
            return _runtime.Query<T>(Sql.Command, Sql.Parameters);
        }

        public Task<IEnumerable<T>> AsAsync<T>()
        {
            return _runtime.QueryAsync<T>(Sql.Command, Sql.Parameters);
        }
    }
}
