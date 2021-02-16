using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate
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
