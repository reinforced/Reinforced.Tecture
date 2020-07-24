using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public class Command : StrokeFeatureBase, Produces<Sql>
    {

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {

        }

        public Command(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
