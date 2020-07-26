using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
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

            //if (_format.NeedsAlias(ArgIdx)) todo
            //{
            //    result.IsDeclaration = true;
            //    _tables[node.Name].IsDeclared = true;
            //}

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



        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.BinaryExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            //if (_format.IsSet(ArgIdx) && IsSpecialSetSyntax(node) && !_isParseringSet) todo
            //{
            //    _isParseringSet = true;
            //    VisitSet(node);
            //    _isParseringSet = false;
            //    return node;
            //}

            //if (_format.IsClause(ArgIdx))
            //{
            //    if ((IsNullConstant(node.Left) || IsNullConstant(node.Right)))
            //    {
            //        if (IsNullConstant(node.Left)) Visit(node.Right);
            //        else if (IsNullConstant(node.Right)) Visit(node.Left);

            //        string oper = node.NodeType == ExpressionType.NotEqual ? "IS NOT" : "IS";
            //        var lft = Retrieve();
            //        Return(new SqlBinaryExpression()
            //        {
            //            Left = lft,
            //            Right = new SqlQueryLiteralExpression() { Literal = "NULL" },
            //            Symbol = oper
            //        });
            //        return node;
            //    }
            //}

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
