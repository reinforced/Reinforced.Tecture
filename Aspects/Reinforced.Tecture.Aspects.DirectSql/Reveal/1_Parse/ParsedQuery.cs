using System.Collections.Generic;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Parse
{
    class ParsedQuery
    {
        public string QueryStructure { get; private set; }

        public PositionedExpression[] Arguments { get; private set; }

        public Dictionary<string, TableReference> InitialTableReferences { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public ParsedQuery(string queryStructure, PositionedExpression[] arguments, Dictionary<string, TableReference> initialTableReferences)
        {
            QueryStructure = queryStructure;
            Arguments = arguments;
            InitialTableReferences = initialTableReferences;
        }

        
    }
}