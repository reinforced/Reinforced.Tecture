using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke;

namespace Reunforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class DirectSqlFeature : Reinforced.Tecture.Features.SqlStroke.Commands.DirectSql
    {
        public DirectSqlFeature(IStrokeRuntime runtime) : base(runtime)
        {
        }
    }
}
