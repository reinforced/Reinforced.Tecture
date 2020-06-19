using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Features.SqlStroke.Commands;

namespace Reinforced.Tecture.Features.SqlStroke.Queries
{
    /// <summary>
    /// Raw SQL query
    /// </summary>
    public class RawQuery
    {
        internal RawQuery(Sql sql, DirectSql runtime)
        {
            Sql = sql;
            _runtime = runtime;
        }

        public Sql Sql { get; }

        private readonly DirectSql _runtime;

        public IEnumerable<T> As<T>()
        {
            return _runtime.Query<T>(Sql.Command, Sql.Parameters);
        }

        public Task<IEnumerable<T>> AsAsync<T>()
        {
            return _runtime.QueryAsync<T>(Sql.Command, Sql.Parameters);
        }
    }
}
