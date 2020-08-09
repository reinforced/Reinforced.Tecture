using System;
using System.Collections.Generic;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit.Expressions;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visit
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
