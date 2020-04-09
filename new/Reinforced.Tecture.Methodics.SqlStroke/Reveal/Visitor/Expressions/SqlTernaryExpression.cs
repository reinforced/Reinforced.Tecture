using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlTernaryExpression : SqlQueryExpression
    {
        public SqlQueryExpression Condition { get; set; }
        public SqlQueryExpression IfTrue { get; set; }
        public SqlQueryExpression IfFalse { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            return string.Format("(IIF({0},{1},{2}))", Condition.Serialize(sqlParams), IfTrue.Serialize(sqlParams),
                IfFalse.Serialize(sqlParams));
        }
    }
}