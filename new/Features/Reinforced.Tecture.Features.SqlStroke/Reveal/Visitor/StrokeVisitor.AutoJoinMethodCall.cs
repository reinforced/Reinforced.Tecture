using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Data.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    partial class StrokeVisitor
    {
        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MethodCallExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            // handle stroke text attribute
            if (node.Method.GetCustomAttribute(typeof(StrokeTextAttribute), true) != null)
            {
                var result = ObtainResult<string>(node);
                Return(new SqlQueryLiteralExpression() { Literal = result ?? string.Empty });
                return node;
            }

            // reveal "contains" method for collections to "IN" - expression
            if (node.Method.Name == "Contains" && node.Arguments.Count == 2 && node.Arguments[0].Type.IsEnumerable())
            {
                var result = ObtainResult<IEnumerable>(node.Arguments[0]);
                Visit(node.Arguments[1]);
                var expr = Retrieve();
                Return(new SqlInExpression() { Expression = expr, Range = result == null ? new object[0] : result.Cast<object>().ToArray() });
                return node;
            }

            // reveal call of {x.JoinedAs(Join.Left)}
            if (node.Method == _joinedAsMethod)
            {
                SqlTableReference result;
                bool success = false;
                if (node.Arguments[0].Unconvert() is ParameterExpression jex)
                {
                    VisitParameter(jex);
                    result = Retrieve() as SqlTableReference;
                    if (result != null)
                    {
                        if (node.Arguments[1].Unconvert() is ConstantExpression cex)
                        {
                            if (cex.Value is Join jn)
                            {
                                success = true;
                                result.ChildrenJoinedAs = jn;
                                Return(result);
                            }
                        }
                    }
                }
                if (!success) throw new Exception("Invalid .JoinedAs expression");

                return node;
            }

            // reveal call of {x.JoinedAs(Join.Left)}
            if (node.Method == _aliasMethod)
            {
                SqlTableReference result;
                bool success = false;
                if (node.Arguments[0].Unconvert() is ParameterExpression jex)
                {
                    VisitParameter(jex);
                    result = Retrieve() as SqlTableReference;
                    if (result != null)
                    {
                        success = true;
                        result.AsAlias = true;
                        Return(result);

                    }
                }
                if (!success) throw new Exception("Invalid .JoinedAs expression");

                return node;
            }

            // reveal call of  {Overjoin(Join.Left, x=>x.Orders.Users}
            // it reveals into empty string, just changing join type of nested table
            if (node.Method == _joinOverrideMethod)
            {
                var join = (Join)(node.Arguments[0] as ConstantExpression).Value;
                var mex = node.Arguments[1].Unconvert() as MemberExpression;

                if (mex == null) throw new Exception("Join overrides work only for nested aggregates");
                var tref = ObtainNestedTableReference(mex, false);
                tref.JoinOveride = join;
                Return(new SqlEmptyExpression());
                return node;
            }

            // todo - joins on many-to-many collections

            if (node.Method.IsGenericMethod && node.Method.GetGenericMethodDefinition() == _everyMethod)
            {

            }

            if (node.Method.IsGenericMethod && node.Method.GetGenericMethodDefinition() == _relationMethod)
            {

            }

            if (node.Method.ReturnType == typeof(void))
                throw new Exception("Cannot use void methods in queries");
            else
            {
                var result = ObtainResult<object>(node);
                Return(new SqlObjectParameter() { Parameter = result });
                return node;
            }
        }


        private static readonly MethodInfo _joinedAsMethod;
        private static readonly MethodInfo _aliasMethod;
        private static readonly MethodInfo _joinOverrideMethod;
        private static readonly MethodInfo _everyMethod;
        private static readonly MethodInfo _relationMethod;
        static StrokeVisitor()
        {
            _joinedAsMethod = typeof(StrokeJoins).GetMethod("JoinedAs");
            _aliasMethod = typeof(StrokeJoins).GetMethod("Alias");
            _joinOverrideMethod = typeof(StrokeJoins).GetMethod("Overjoin");

            _everyMethod = typeof(StrokeRelations).GetMethod("Every");
            _relationMethod = typeof(StrokeRelations).GetMethod("Relation");
        }

    }
}
