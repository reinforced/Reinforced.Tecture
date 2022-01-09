using Reinforced.Tecture.Aspects.DirectSql.Infrastructure;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Runtime;

namespace Reinforced.Samples.ToyFactory.Tests.Infrastructure.Maria
{
    class MariaSchemaInterpolator : SchemaInterpolator
    {
        public MariaSchemaInterpolator(IMapper mapper) : base(mapper)
        {
        }

        protected override string TableMakeAlias(TableReference tr)
        {
            if (string.IsNullOrEmpty(tr.Alias)) return $"`{Mapper.GetTableName(tr.EntityType)}`";
            return $"`{Mapper.GetTableName(tr.EntityType)}` `{tr.Alias}`";
        }

        protected override string TableAlias(TableReference tr)
        {
            if (string.IsNullOrEmpty(tr.Alias)) return $"`{Mapper.GetTableName(tr.EntityType)}`";
            return $"`{tr.Alias}`";
        }

        protected override string VisitColumnReference(TableReference tr, string colName)
        {
            return $"{TableAlias(tr)}.`{colName}`";
        }
    }
    
    public class MariaInterpolation : InterpolatorFactory
    {
        public override SchemaInterpolator CreateSchemaInterpolator(IMapper mapper)
        {
            return new MariaSchemaInterpolator(mapper);
        }
    }
}