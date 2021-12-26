using System;
using System.Collections.Generic;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Aspects.DirectSql.Infrastructure;
using Reinforced.Tecture.Aspects.DirectSql.Reveal;

namespace Reinforced.Tecture.Aspects.DirectSql
{
    public sealed partial class DirectSql
    {
        /// <summary>
        /// Command tooling for DirectSql aspect
        /// </summary>
        public abstract class Command : CommandAspect<Sql>
        {
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

            /// <summary>
            /// Gets set of valid types to be used within SQL query
            /// </summary>
            protected abstract HashSet<Type> ServingTypes { get; }

            /// <inheritdoc />
            protected Command(IStrokeRuntime runtime)
            {
                _runtime = runtime;
            }

            /// <summary>
            /// Compiles SQL command interpolating all the entities
            /// </summary>
            /// <param name="command">Sql command to compile</param>
            /// <returns>Interpolated query with extracted parameters</returns>
            public InterpolatedQuery Compile(Sql command)
            {
                return Tooling.Compile(command);
            }
        }
    }
}
