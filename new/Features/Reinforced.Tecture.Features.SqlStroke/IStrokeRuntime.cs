using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public interface IStrokeRuntime
    {
        IMapper Mapper { get; }

        IEnumerable<Type> ServingTypes { get; }

        Type Channel { get; }

    }
}
