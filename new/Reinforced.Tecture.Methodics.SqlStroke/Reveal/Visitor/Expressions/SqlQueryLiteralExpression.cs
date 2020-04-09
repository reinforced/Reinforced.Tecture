using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlQueryLiteralExpression : SqlQueryExpression
    {
        public string Literal { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            return Literal;
        }
    }
}