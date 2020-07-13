using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Features.SqlStroke.Command
{
    public class DirectSql : StrokeFeatureBase, Produces<Sql>
    {

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {

        }

        public DirectSql(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
