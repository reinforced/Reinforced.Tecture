using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Data.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    partial class StrokeVisitor
    {
        private string GetNodeSymbol(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.And: return "&";
                case ExpressionType.AndAlso: return "AND";
                case ExpressionType.Or: return "|";
                case ExpressionType.OrElse: return "OR";
                case ExpressionType.Not: return "NOT";
                case ExpressionType.Equal: return "=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.Negate: return "-";
                case ExpressionType.UnaryPlus: return "+";
                case ExpressionType.Assign: return "=";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.Coalesce: return "??";
                case ExpressionType.Modulo: return "%";
                default:
                    throw new Exception("Invalid expression type");
            }
        }

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
