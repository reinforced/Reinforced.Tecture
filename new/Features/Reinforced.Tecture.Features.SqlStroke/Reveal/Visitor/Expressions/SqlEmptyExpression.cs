using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlEmptyExpression : SqlQueryExpression
    {
        public override string Serialize(List<Expression> sqlParams)
        {
            return string.Empty;
        }
    }
}