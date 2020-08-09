using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.SchemaInterpolate;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime
{
    public class InterpolatorFactory
    {
        public virtual LanguageInterpolator CreateLanguageInterpolator()
        {
            return new LanguageInterpolator();
        }

        public virtual SchemaInterpolator CreateSchemaInterpolator(IMapper mapper)
        {
            return new SchemaInterpolator(mapper);
        }
    }
}
