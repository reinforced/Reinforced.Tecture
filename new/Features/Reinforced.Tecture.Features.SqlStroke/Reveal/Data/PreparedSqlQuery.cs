using System;
using System.Collections.Generic;
using System.Linq;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Data.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Data
{
    class PreparedSqlQuery
    {
        public string QueryStructure { get; private set; }

        public SqlQueryExpression[] Arguments { get; private set; }

        public Type[] UsedTypes { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public PreparedSqlQuery(string queryStructure, SqlQueryExpression[] arguments, IEnumerable<Type> usedTypes)
        {
            QueryStructure = queryStructure;
            Arguments = arguments;
            UsedTypes = usedTypes.ToArray();
        }
    }
}