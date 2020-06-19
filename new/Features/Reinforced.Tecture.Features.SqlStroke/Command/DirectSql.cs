using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Features.SqlStroke.Commands
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
