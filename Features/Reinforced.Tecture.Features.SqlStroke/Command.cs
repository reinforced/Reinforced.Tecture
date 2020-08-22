using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Features.SqlStroke.Reveal;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public abstract class Command : CommandFeature, Produces<Sql>
    {
        internal readonly StrokeToolingWrapper Tooling;
        protected Command(IStrokeRuntime runtime)
        {
            Tooling = new StrokeToolingWrapper(runtime);
        }

        public InterpolatedQuery Compile(Sql command)
        {
            return Tooling.Compile(command);
        }
    }
}
