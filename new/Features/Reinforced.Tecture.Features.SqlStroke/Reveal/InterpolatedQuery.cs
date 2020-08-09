using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal
{
    public abstract class InterpolatedQuery
    {
        public string Query { get; }

        public object[] Parameters { get; }

        public Type[] UsedTypes { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        internal InterpolatedQuery(string query, object[] parameters, Type[] usedTypes)
        {
            Query = query;
            Parameters = parameters;
            UsedTypes = usedTypes;
        }
    }
}
