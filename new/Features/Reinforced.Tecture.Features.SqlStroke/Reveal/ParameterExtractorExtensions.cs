using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal
{
    static class ParameterExtractorExtensions
    {

        public static bool IsScopedParameterAccess(this Expression expr)
        {
            expr = expr.Unconvert();
            if (expr.NodeType == ExpressionType.Parameter) return true;
            var ex = expr.Unconvert() as MemberExpression;
            if (ex == null) return false;

            var root = GetRootMember(ex);
            if (root == null) return false;
            if (root.NodeType != ExpressionType.Parameter) return false;
            return true;
        }

        public static string MakeNestedTableAlias(this ParameterExpression expr, Type derivedTypeName)
        {
            return string.Format("{0}_as_{1}", expr.Name, derivedTypeName.Name);
        }

        /// <summary>
        /// Obtains alias for nested table
        /// </summary>
        /// <param name="expr">Member expression in form of x.User.Order </param>
        /// <param name="takeDerived">Flag when we address to the column of the "parent" entity (from another table)</param>
        /// <returns></returns>
        public static string MakeNestedTableAlias(this MemberExpression expr, bool takeDerived)
        {
            Stack<string> path = new Stack<string>();
            if (takeDerived) path.Push("_d");
            var accessee = expr;
            var current = (Expression)expr;
            while (accessee != null)
            {

                path.Push(accessee.Member.Name);
                current = accessee.Expression;
                accessee = accessee.Expression as MemberExpression;
            }

            var pax = current as ParameterExpression;
            if (pax != null) path.Push(pax.Name);

            return string.Join("_", path);
        }

       
        
    }
}