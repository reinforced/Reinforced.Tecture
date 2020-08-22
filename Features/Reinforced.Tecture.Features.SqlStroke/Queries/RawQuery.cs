using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.SqlStroke.Queries
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

        public Sql Sql { get; }

        private readonly Query _runtime;

        public IEnumerable<T> As<T>() where T : class
        {
            IEnumerable<T> result;
            if (_a.IsEvaluationNeeded)
            {
                var cq = _runtime.Tooling.Compile(Sql);
                result = _runtime.DoQuery<T>(cq.Query, cq.Parameters);
            }
            else
            {
                result = _a.Get<IEnumerable<T>>(Sql.Hash());
            }

            if (_a.IsTracingNeeded)
            {
                _a.Query(Sql.Hash(), result, _description);
            }

            return result;
        }

        public async Task<IEnumerable<T>> AsAsync<T>() where T : class
        {

            IEnumerable<T> result;
            if (_a.IsEvaluationNeeded)
            {
                var cq = _runtime.Tooling.Compile(Sql);
                result = await _runtime.DoQueryAsync<T>(cq.Query, cq.Parameters);
            }
            else
            {
                result = _a.Get<IEnumerable<T>>(Sql.Hash());
            }

            if (_a.IsTracingNeeded)
            {
                _a.Query(Sql.Hash(), result, _description);
            }

            return result;
        }
    }
}
