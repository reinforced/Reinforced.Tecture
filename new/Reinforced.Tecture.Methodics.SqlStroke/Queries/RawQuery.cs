using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Methodics.SqlStroke.Commands;

namespace Reinforced.Tecture.Methodics.SqlStroke.Queries
{
    /// <summary>
    /// Raw SQL query
    /// </summary>
    public class RawQuery
    {
        internal RawQuery(Sql sql, SqlStrokeRuntimeBase runtime)
        {
            Sql = sql;
            _runtime = runtime;
        }

        public Sql Sql { get; }

        private readonly SqlStrokeRuntimeBase _runtime;

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
