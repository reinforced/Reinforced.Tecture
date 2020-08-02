using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Preparation
{
    class PreparedQuery
    {
        public string QueryStructure { get; private set; }

        public PositionedExpression[] Arguments { get; private set; }

        public Dictionary<string, TableReference> InitialTableReferences { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public PreparedQuery(string queryStructure, PositionedExpression[] arguments, Dictionary<string, TableReference> initialTableReferences)
        {
            QueryStructure = queryStructure;
            Arguments = arguments;
            InitialTableReferences = initialTableReferences;
        }

        
    }

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