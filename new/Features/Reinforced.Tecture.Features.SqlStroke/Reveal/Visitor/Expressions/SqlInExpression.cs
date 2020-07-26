using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
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

        public override string Serialize(List<Expression> sqlParams)
        {
            sqlParams.Add(System.Linq.Expressions.Expression.Constant(Range));
            string oper = Not ? "NOT IN" : "IN";
            return string.Format("{0} {2} ({{{1}}})", Expression.Serialize(sqlParams), sqlParams.Count - 1, oper);
        }
    }
}