using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.SchemaInterpolate
{
    internal static class Extensions
    {
        public static SchemaInterpolatedQuery SchemaInterpolateStroke(this LanguageInterpolatedQuery query, SchemaInterpolator interpolator)
        {
            var r = interpolator.Proceed(query);
            return r;
        }
    }
}
