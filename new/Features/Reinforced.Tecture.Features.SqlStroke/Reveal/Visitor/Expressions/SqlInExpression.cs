using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlInExpression : SqlQueryExpression
    {
        public SqlQueryExpression Expression { get; set; }

        public object[] Range { get; set; }

        internal bool Not { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            sqlParams.Add(System.Linq.Expressions.Expression.Constant(Range));
            string oper = Not ? "NOT IN" : "IN";
            return string.Format("{0} {2} ({{{1}}})", Expression.Serialize(sqlParams), sqlParams.Count - 1, oper);
        }
    }
}