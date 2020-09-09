using Reinforced.Tecture.Aspects.DirectSql.Infrastructure;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspect.DirectSql.Runtime
{
    public class InterpolatorFactory
    {
        public virtual Aspects.DirectSql.Reveal.LanguageInterpolate.LanguageInterpolator CreateLanguageInterpolator()
        {
            return new Aspects.DirectSql.Reveal.LanguageInterpolate.LanguageInterpolator();
        }

        public virtual SchemaInterpolator CreateSchemaInterpolator(IMapper mapper)
        {
            return new SchemaInterpolator(mapper);
        }
    }
}
