using Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate;
// ReSharper disable CheckNamespace

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate
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
