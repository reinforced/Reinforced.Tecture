using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Cloning;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate
{
    

    class LanguageInterpolatedQuery : InterpolatedQuery
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public LanguageInterpolatedQuery(string query, object[] parameters, Type[] usedTypes) : base(query, parameters, usedTypes)
        {
        }

        internal override InterpolatedQuery Clone()
        {
            return new LanguageInterpolatedQuery(Query, Parameters.Select(x => x.DeepClone()).ToArray(), UsedTypes);
        }
    }

    
}
