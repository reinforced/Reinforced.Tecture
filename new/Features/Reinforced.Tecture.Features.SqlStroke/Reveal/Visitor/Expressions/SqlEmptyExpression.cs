using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    /// <summary>
    /// Empty SQL Expression (needed sometimes)
    /// </summary>
    public class SqlEmptyExpression : SqlQueryExpression
    {
        internal SqlEmptyExpression() { }
    }
}