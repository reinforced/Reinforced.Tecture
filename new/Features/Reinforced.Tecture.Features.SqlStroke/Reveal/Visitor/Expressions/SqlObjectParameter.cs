using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlObjectParameter : SqlQueryExpression
    {
        public object Parameter { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            sqlParams.Add(Expression.Convert(Expression.Constant(Parameter), typeof(object)));
            return string.Format("{{{0}}}", sqlParams.Count - 1);
        }
    }
}