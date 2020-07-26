using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    /// <summary>
    /// Object parameter reference
    /// </summary>
    public class SqlObjectParameter : SqlQueryExpression
    {
        internal SqlObjectParameter() { }

        /// <summary>
        /// Parameter value
        /// </summary>
        public object Parameter { get; set; }
        
    }
}