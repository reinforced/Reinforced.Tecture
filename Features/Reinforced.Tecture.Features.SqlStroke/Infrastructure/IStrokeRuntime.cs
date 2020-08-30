using System;
using System.Collections.Generic;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.SchemaInterpolate;

namespace Reinforced.Tecture.Features.SqlStroke.Infrastructure
{
    public interface IStrokeRuntime
    {
        IMapper Mapper { get; }

        Type Channel { get; }

        LanguageInterpolator GetLanguageInterpolator();

        SchemaInterpolator GetSchemaInterpolator();

    }
}
