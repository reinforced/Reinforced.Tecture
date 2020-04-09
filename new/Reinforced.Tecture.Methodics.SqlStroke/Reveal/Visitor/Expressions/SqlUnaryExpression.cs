using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlUnaryExpression : SqlQueryExpression
    {
        public SqlQueryExpression Operand { get; set; }

        public string Symbol { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            if (Symbol == "NOT")
            {
                var op = Operand as SqlInExpression;
                if (op != null)
                {
                    op.Not = true;
                    return string.Format("({0})", op.Serialize(sqlParams));
                }
            }
            return string.Format("({0} {1})", Symbol, Operand.Serialize(sqlParams));
        }
    }
}