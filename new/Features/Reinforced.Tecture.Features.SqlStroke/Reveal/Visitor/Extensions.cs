using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    static class Extensions
    {
        public static bool IsEnumerable(this Type t)
        {
            if (t.IsArray) return true;
            if (typeof(IEnumerable).IsAssignableFrom(t)) return true;
            if (t.IsGenericType)
            {
                var tg = t.GetGenericTypeDefinition();
                if (typeof(IEnumerable<>).IsAssignableFrom(tg)) return true;
            }
            return false;
        }

        public static Expression Unconvert(this Expression ex)
        {
            if (ex.NodeType == ExpressionType.Convert)
            {
                var cex = ex as UnaryExpression;
                if (cex != null) ex = cex.Operand;
            }
            return ex;
        }

        public static Expression GetRootMember(this MemberExpression expr)
        {
            var mex = expr.Expression.Unconvert();
            var accessee = mex as MemberExpression;

            var current = mex;
            while (accessee != null)
            {
                current = accessee.Expression.Unconvert();
                accessee = accessee.Expression as MemberExpression;
            }
            return current;
        }

        internal static void AddIfNotExists<T>(this HashSet<T> hashSet, T val)
        {
            if (hashSet.Contains(val)) return;
            hashSet.Add(val);
        }

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