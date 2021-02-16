using System;
using System.Collections.Generic;
using System.Linq;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit
{
    class VisitedQuery
    {
        public string QueryStructure { get; private set; }

        public SqlQueryExpression[] Arguments { get; private set; }

        public Type[] UsedTypes { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        internal VisitedQuery(string queryStructure, SqlQueryExpression[] arguments, IEnumerable<Type> usedTypes)
        {
            QueryStructure = queryStructure;
            Arguments = arguments;
            UsedTypes = usedTypes.ToArray();
        }
    }
}