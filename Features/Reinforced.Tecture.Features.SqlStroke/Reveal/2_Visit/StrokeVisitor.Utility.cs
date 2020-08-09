using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit.Expressions;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visit
{
    partial class StrokeVisitor
    {
        private SqlBinaryExpression WrapAloneBoolean(SqlColumnReference boolColumn, bool negate)
        {
            return new SqlBinaryExpression()
            {
                Left = boolColumn,
                Right = new SqlQueryLiteralExpression() { Literal = "1" },
                Operator = negate ? SqlOperator.NotEqual : SqlOperator.Equal
            };
        }

        private T ObtainResult<T>(Expression ex)
        {
            var lambda = Expression.Lambda(ex);
            var compiled = lambda.Compile();
            var result = (T)compiled.DynamicInvoke();
            return result;
        }

        private bool IsNullConstant(Expression ex)
        {
            if (ex.NodeType != ExpressionType.Constant) return false;
            return ((ConstantExpression)ex).Value == null;
        }
    }
}
