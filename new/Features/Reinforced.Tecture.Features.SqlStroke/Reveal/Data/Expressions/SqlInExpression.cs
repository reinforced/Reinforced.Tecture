using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Data.Expressions
{
    /// <summary>
    /// In/contains expression
    /// </summary>
    public class SqlInExpression : SqlQueryExpression
    {
        internal SqlInExpression() { }

        public SqlQueryExpression Expression { get; internal set; }

        public object[] Range { get; internal set; }

        public bool Not { get; internal set; }
        
    }
}