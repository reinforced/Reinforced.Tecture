using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit
{

    internal partial class StrokeVisitor : ExpressionVisitor
    {
        private readonly Dictionary<string, TableReference> _tables;
        private readonly Func<Type, bool> _isEntity;

        public StrokeVisitor(Dictionary<string, TableReference> tables, Func<Type, bool> isEntity)
        {
            _tables = tables;
            _isEntity = isEntity;
            foreach (var tpr in tables)
            {
                _usedTypes.AddIfNotExists(tpr.Value.EntityType);
            }
        }

        /// <summary>Dispatches the expression to one of the more specialized visit methods in this class.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.ConditionalExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            Visit(node.Test);
            var test = Retrieve();
            Visit(node.IfTrue);
            var ifTrue = Retrieve();
            Visit(node.IfFalse);
            var ifFalse = Retrieve();
            Return(new SqlTernaryExpression() { Condition = test, IfTrue = ifTrue, IfFalse = ifFalse });

            return node;
        }

        /// <summary>Visits the <see cref="T:System.Linq.Expressions.ParameterExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            var result = new SqlTableReference() { Table = _tables[node.Name] };
            Return(result);
            return node;
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitMember(MemberExpression node)
        {
            Return(VisitColumn(node));
            return node;
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.UnaryExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            var unconverted = node.Unconvert();
            if (unconverted != node) return base.Visit(unconverted);
            SqlQueryExpression operand = null;

            Visit(node.Operand);
            operand = Retrieve();
            if (node.NodeType == ExpressionType.Not && node.Type == typeof(bool) && operand is SqlColumnReference)
            {
                var cr = operand as SqlColumnReference;
                var result = WrapAloneBoolean(cr, true);
                Return(result);
                return node;
            }
            Return(new SqlUnaryExpression() { Operand = operand, Operator = node.NodeType.ToSqlOperator() });
            return node;
        }

        private bool IsSpecialSetSyntax(BinaryExpression node)
        {
            if (node.NodeType != ExpressionType.Or) return false;
            if (node.Left.NodeType != ExpressionType.Or && node.Left.NodeType != ExpressionType.Equal) return false;
            if (node.Right.NodeType != ExpressionType.Or && node.Right.NodeType != ExpressionType.Equal) return false;
            return true;
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.BinaryExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Visit(node.Left);
            var left = Retrieve();
            Visit(node.Right);
            var right = Retrieve();

            if (node.NodeType != ExpressionType.Equal)
            {
                if ((node.Left.Type == typeof(bool) || node.Left.Type == typeof(bool?)) && left is SqlColumnReference)
                {
                    var col = left as SqlColumnReference;
                    left = WrapAloneBoolean(col, false);
                }

                if ((node.Right.Type == typeof(bool) || node.Right.Type == typeof(bool?)) && right is SqlColumnReference)
                {
                    var col = right as SqlColumnReference;
                    right = WrapAloneBoolean(col, false);
                }
            }

            Return(new SqlBinaryExpression() { Left = left, Right = right, Operator = node.NodeType.ToSqlOperator(), IsSetSuspect = IsSpecialSetSyntax(node)});
            return node;
        }

        /// <summary>Visits the <see cref="T:System.Linq.Expressions.ConstantExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            var value = node.Value;
            var type = node.Type;

            if (value == null)
            {
                Return(new SqlNullExpression());
                return node;
            }
            if (type == typeof(bool))
            {
                var b = (bool)value;
                Return(new SqlBooleanExpression(b));
                return node;
            }
            Return(new SqlObjectParameter() { Parameter = value });
            return node;
        }
    }
}
