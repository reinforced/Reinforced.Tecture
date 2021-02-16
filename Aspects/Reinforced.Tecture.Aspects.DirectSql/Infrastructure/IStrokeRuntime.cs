using System;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate;

namespace Reinforced.Tecture.Aspects.DirectSql.Infrastructure
{
    public interface IStrokeRuntime
    {
        IMapper Mapper { get; }

        Type Channel { get; }

        LanguageInterpolator GetLanguageInterpolator();

        SchemaInterpolator GetSchemaInterpolator();

    }
}
