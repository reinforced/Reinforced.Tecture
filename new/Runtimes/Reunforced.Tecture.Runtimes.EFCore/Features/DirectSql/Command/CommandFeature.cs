using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;

namespace Reunforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class CommandFeature : Reinforced.Tecture.Features.SqlStroke.Command
    {
        public CommandFeature(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
