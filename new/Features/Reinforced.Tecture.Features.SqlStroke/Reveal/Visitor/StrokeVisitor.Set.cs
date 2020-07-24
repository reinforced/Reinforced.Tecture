using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    partial class StrokeVisitor
    {
        //private bool _isParseringSet = false;

        private bool IsSpecialSetSyntax(BinaryExpression node)
        {
            if (node.NodeType != ExpressionType.Or) return false;
            if (node.Left.NodeType != ExpressionType.Or && node.Left.NodeType != ExpressionType.Equal) return false;
            if (node.Right.NodeType != ExpressionType.Or && node.Right.NodeType != ExpressionType.Equal) return false;
            return true;
        }

        //private void VisitSet(BinaryExpression node)
        //{
        //    var result = new SqlSetExpression();
        //    ProceedPart(node.Left, result);
        //    ProceedPart(node.Right, result);
        //    Return(result);
        //}

        //private void ProceedPart(Expression ex, SqlSetExpression result)
        //{
        //    if (ex.NodeType == ExpressionType.Or)
        //    {
        //        VisitSet((BinaryExpression)ex);
        //        var d = (SqlSetExpression)Retrieve();
        //        foreach (var sx in d.Assignments)
        //        {
        //            result.Assignments.Add(sx);
        //        }
        //        return;
        //    }

        //    if (ex.NodeType == ExpressionType.Equal)
        //    {
        //        var setEx = (BinaryExpression)ex;
        //        var left = setEx.Left.Unconvert();
        //        var right = setEx.Right.Unconvert();
        //        if (left.NodeType == ExpressionType.MemberAccess)
        //        {
        //            var mex = (MemberExpression)left;
        //            var col = VisitColumn(mex) as SqlColumnReference;
        //            if (col != null)
        //            {
        //                Visit(right);
        //                var exp = Retrieve();
        //                var assign = new SqlSetAssignmentExpression()
        //                {
        //                    Column = col,
        //                    Expression = exp
        //                };
        //                result.Assignments.Add(assign);
        //                return;
        //            }
        //        }
        //    }
        //    throw new Exception("Invalid SET expression around " + ex.ToString());
        //}
    }
}
