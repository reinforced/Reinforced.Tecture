using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Methodics.SqlStroke
{
    public interface ISqlSource : ISource
    {
        SqlStrokeRuntimeBase GetStrokeRuntime(Type[] usedTypes);
    }
}
