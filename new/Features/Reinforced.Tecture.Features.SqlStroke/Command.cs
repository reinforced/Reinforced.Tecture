using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public class Command : StrokeFeatureBase, Produces<Sql>
    {
        public virtual void Dispose() { }

        protected Command(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
