using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal
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

        public static string GetNestedTableAlias(this ParameterExpression expr, Type derivedTypeName)
        {
            return string.Format("{0}_as_{1}", expr.Name, derivedTypeName.Name);
        }
        public static string GetNestedTableAlias(this MemberExpression expr, bool takeDerived)
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

        public static bool IsColumnAlias(this string format, int argNumber)
        {
            const string AS = "AS";
            var searchString = "{" + argNumber + "}";
            var idx = format.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase);
            if (idx <= 0) return false;

            return format.Precends(AS, idx);
        }

        public static bool IsSet(this string format, int argNumber)
        {
            const string SET = "SET";
            var searchString = "{" + argNumber + "}";
            var idx = format.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase);
            if (idx <= 0) return false;

            return format.Precends(SET, idx);
        }

        public static bool IsClause(this string format, int argNumber)
        {
            const string WHERE = "WHERE";
            const string OR = "OR";
            const string AND = "AND";
            const string NOT = "NOT";
            const string ON = "ON";
            var searchString = "{" + argNumber + "}";
            var idx = format.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase);
            if (idx <= 0) return false;

            return format.Precends(WHERE, idx) || format.Precends(OR, idx) || format.Precends(AND, idx) || format.Precends(NOT, idx) || format.Precends(ON, idx);
        }

        public static bool NeedsAlias(this string format, int argNumber)
        {
            const string FROM = "FROM";
            const string DELETE = "DELETE";
            const string JOIN = "JOIN";

            var searchString = "{" + argNumber + "}";
            var idx = format.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase);
            if (idx <= 0) return false;

            var isFrom = format.Precends(FROM, idx);

            if (isFrom)
            {
                var frmidx = idx - FROM.Length - 1;
                if (frmidx < 0) return false;
                if (format.Precends(DELETE, frmidx)) return false;
                return true;
            }

            var isJoin = format.Precends(JOIN, idx);
            return isJoin;
        }
    }
}