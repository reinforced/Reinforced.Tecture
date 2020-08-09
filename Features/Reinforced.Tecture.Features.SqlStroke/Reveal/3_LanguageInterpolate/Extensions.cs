using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate
{
    internal static class Extensions
    {
        public static LanguageInterpolatedQuery LanguageInterpolateStroke(this VisitedQuery query, LanguageInterpolator languageInterpolator)
        {
            languageInterpolator = languageInterpolator ?? new LanguageInterpolator();

            var r = languageInterpolator.Proceed(query);
            return r;
        }
    }
}
