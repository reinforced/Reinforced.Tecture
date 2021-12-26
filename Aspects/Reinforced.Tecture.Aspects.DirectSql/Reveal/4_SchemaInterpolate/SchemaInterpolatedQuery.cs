using System;
using System.Linq;
using Reinforced.Tecture.Cloning;

// ReSharper disable CheckNamespace

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate
{
    class SchemaInterpolatedQuery : InterpolatedQuery
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public SchemaInterpolatedQuery(string query, object[] parameters, Type[] usedTypes) : base(query, parameters, usedTypes)
        {
        }

        internal override InterpolatedQuery Clone()
        {
            return new SchemaInterpolatedQuery(Query, Parameters.Select(x => DeepCloner.DeepClone(x)).ToArray(), UsedTypes);
        }
    }
}
