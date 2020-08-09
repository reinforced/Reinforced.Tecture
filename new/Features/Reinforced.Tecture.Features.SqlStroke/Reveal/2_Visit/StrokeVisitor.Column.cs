using System;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit.Expressions;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visit
{
    partial class StrokeVisitor
    {
        private SqlQueryExpression VisitColumn(MemberExpression node)
        {
            // first we detect closures
            if (!node.IsScopedParameterAccess())
            {
                return new SqlObjectParameter() { Parameter = ObtainResult<object>(node) };
            }

            // Get whether property points to an entity (case like x.User.Order) 
            if (_isEntity(node.Type))
            {
                // if so, we have nested table access
                var nestedResult = new SqlTableReference()
                {
                    Table = ObtainNestedTableReference(node, false)
                };
                
                return nestedResult;
            }

            TableReference tableRef = null;
            PropertyInfo colId = null;
            var root = node.GetRootMember();
            var nex = node.Expression.Unconvert();

            if (nex.NodeType != ExpressionType.Parameter)
            {
                if (nex.NodeType == ExpressionType.MemberAccess && nex is MemberExpression mex) //nested table column
                {
                    var derived = (node.Member.DeclaringType != mex.Type) && _isEntity(node.Member.DeclaringType);

                    tableRef = ObtainNestedTableReference(mex, derived);
                    colId = node.Member as PropertyInfo;
                }
                else throw new Exception(string.Format("Please refer only top-level properties of {0}", root.Type));
            }


            if (tableRef == null && colId == null)
            {
                var parRef = root as ParameterExpression;
                if (parRef == null) throw new Exception("Unknown column reference: " + node.ToString());
                tableRef = _tables[parRef.Name];
                var derived = (node.Member.DeclaringType != root.Type) && _isEntity(node.Member.DeclaringType);
                colId = node.Member as PropertyInfo;
            }

            var result = new SqlColumnReference()
            {
                Column = colId,
                Table = tableRef
            };


            return result;
        }
    }
}
