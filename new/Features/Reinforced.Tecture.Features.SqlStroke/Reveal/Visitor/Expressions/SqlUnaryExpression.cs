using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    /// <summary>
    /// Unary SQL expression
    /// </summary>
    public class SqlUnaryExpression : SqlQueryExpression
    {
        public SqlQueryExpression Operand { get; internal set; }

        public SqlOperator Operator { get; internal set; }
        
    }
}