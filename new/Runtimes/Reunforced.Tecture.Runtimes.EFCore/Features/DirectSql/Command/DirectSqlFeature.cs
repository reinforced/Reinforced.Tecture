using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke;

namespace Reunforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class DirectSqlFeature : Reinforced.Tecture.Features.SqlStroke.Command.DirectSql
    {
        public DirectSqlFeature(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
