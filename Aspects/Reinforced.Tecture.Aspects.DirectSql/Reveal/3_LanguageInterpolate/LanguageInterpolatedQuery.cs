using System;
using System.Linq;
using Reinforced.Tecture.Cloning;
// ReSharper disable CheckNamespace

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate
{
    

    class LanguageInterpolatedQuery : InterpolatedQuery
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public LanguageInterpolatedQuery(string query, object[] parameters, Type[] usedTypes) : base(query, parameters, usedTypes)
        {
        }

        internal override InterpolatedQuery Clone()
        {
            return new LanguageInterpolatedQuery(Query, Parameters.Select(x => DeepCloner.DeepClone(x)).ToArray(), UsedTypes);
        }
    }

    
}
