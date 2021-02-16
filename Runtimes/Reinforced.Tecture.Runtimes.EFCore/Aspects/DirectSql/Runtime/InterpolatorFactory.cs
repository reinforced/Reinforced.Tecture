using Reinforced.Tecture.Aspects.DirectSql.Infrastructure;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Runtime
{
    public class InterpolatorFactory
    {
        public virtual Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate.LanguageInterpolator CreateLanguageInterpolator()
        {
            return new Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate.LanguageInterpolator();
        }

        public virtual SchemaInterpolator CreateSchemaInterpolator(IMapper mapper)
        {
            return new SchemaInterpolator(mapper);
        }
    }
}
