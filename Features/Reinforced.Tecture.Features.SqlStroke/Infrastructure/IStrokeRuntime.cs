using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Features.SqlStroke.Infrastructure
{
    public interface IStrokeRuntime
    {
        IMapper Mapper { get; }

        IEnumerable<Type> ServingTypes { get; }

        Type Channel { get; }

    }
}
