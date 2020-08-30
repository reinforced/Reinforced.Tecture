using System;
using System.Collections.Generic;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Features.SqlStroke.Reveal;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public abstract class Command : CommandFeature, Produces<Sql>
    {
        private StrokeToolingWrapper _tooling;
        internal StrokeToolingWrapper Tooling
        {
            get
            {
                if (_tooling == null)
                {
                    _tooling = new StrokeToolingWrapper(_runtime, Aux, ServingTypes);
                }

                return _tooling;
            }
        }
        private readonly IStrokeRuntime _runtime;

        protected abstract HashSet<Type> ServingTypes { get; }

        protected Command(IStrokeRuntime runtime)
        {
            _runtime = runtime;
        }

        public InterpolatedQuery Compile(Sql command)
        {
            return Tooling.Compile(command);
        }
    }
}
