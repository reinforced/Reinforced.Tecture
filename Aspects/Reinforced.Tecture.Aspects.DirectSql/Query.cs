using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Aspects.DirectSql.Infrastructure;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Aspects.DirectSql
{
    /// <summary>
    /// Query tooling of DirectSql aspect
    /// </summary>
    public abstract class Query : QueryAspect
    {
        /// <summary>
        /// Performs SQL query
        /// </summary>
        /// <typeparam name="T">Result record type</typeparam>
        /// <param name="command">SQL command</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Set of deserialized records</returns>
        public abstract IEnumerable<T> DoQuery<T>(string command, object[] parameters) where T : class;

        /// <summary>
        /// Performs SQL query (asynchronously)
        /// </summary>
        /// <typeparam name="T">Result record type</typeparam>
        /// <param name="command">SQL command</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Set of deserialized records</returns>
        public abstract Task<IEnumerable<T>> DoQueryAsync<T>(string command, object[] parameters) where T : class;

        private SqlToolingWrapper _tooling;
        internal SqlToolingWrapper Tooling
        {
            get
            {
                if (_tooling == null)
                {
                    _tooling = new SqlToolingWrapper(_runtime, Aux, ServingTypes);
                }

                return _tooling;
            }
        }

        private readonly IStrokeRuntime _runtime;
        internal new Auxiliary Aux
        {
            get { return base.Aux; }
        }

        /// <inheritdoc />
        protected Query(IStrokeRuntime runtime)
        {
            _runtime = runtime;
        }

        /// <summary>
        /// Gets set of valid types to be used within SQL query
        /// </summary>
        protected abstract HashSet<Type> ServingTypes { get; }
    }
}
