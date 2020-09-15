using System;
using System.Linq.Expressions;
// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit
{
    public static class SqlOperatorExtensions
    {
        public static SqlOperator ToSqlOperator(this ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Add: return SqlOperator.Add;
                case ExpressionType.Subtract: return SqlOperator.Subtract;
                case ExpressionType.Divide: return SqlOperator.Divide;
                case ExpressionType.Multiply: return SqlOperator.Multiply;
                case ExpressionType.And: return SqlOperator.And;
                case ExpressionType.AndAlso: return SqlOperator.AndAlso;
                case ExpressionType.Or: return SqlOperator.Or;
                case ExpressionType.OrElse: return SqlOperator.OrElse;
                case ExpressionType.Not: return SqlOperator.Not;
                case ExpressionType.Equal: return SqlOperator.Equal;
                case ExpressionType.NotEqual: return SqlOperator.NotEqual;
                case ExpressionType.Negate: return SqlOperator.Negate;
                case ExpressionType.UnaryPlus: return SqlOperator.UnaryPlus;
                case ExpressionType.Assign: return SqlOperator.Assign;
                case ExpressionType.GreaterThan: return SqlOperator.GreaterThan;
                case ExpressionType.LessThan: return SqlOperator.LessThan;
                case ExpressionType.LessThanOrEqual: return SqlOperator.LessThanOrEqual;
                case ExpressionType.GreaterThanOrEqual: return SqlOperator.GreaterThanOrEqual;
                case ExpressionType.Coalesce: return SqlOperator.Coalesce;
                case ExpressionType.Modulo: return SqlOperator.Modulo;
                default:
                    throw new Exception("Invalid expression type");
            }
        }
    }

    public enum SqlOperator
    {
        Add,
        Subtract,
        Divide,
        Multiply,
        And,
        AndAlso,
        Or,
        OrElse,
        Not,
        Equal,
        NotEqual,
        Negate,
        UnaryPlus,
        Assign,
        GreaterThan,
        LessThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        Coalesce,
        Modulo,
    }
}
