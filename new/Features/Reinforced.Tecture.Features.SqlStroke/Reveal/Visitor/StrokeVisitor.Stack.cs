using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    partial class StrokeVisitor
    {
        private readonly Stack<SqlQueryExpression> _resultsStack = new Stack<SqlQueryExpression>();
        private void Return(SqlQueryExpression expr)
        {
            _resultsStack.Push(expr);
        }

        public SqlQueryExpression Retrieve()
        {
            return _resultsStack.Pop();
        }

        private readonly HashSet<Type> _usedTypes = new HashSet<Type>();

        public IEnumerable<Type> UsedTypes
        {
            get { return _usedTypes; }
        }

    }
}
