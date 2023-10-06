using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.AsyncExecutionAdapter
{
    class QueryableWithAsyncExecutor : IOrderedQueryable
    {
        private readonly IQueryable _original;

        public IQueryable Original => _original;

        public QueryableWithAsyncExecutor(IAsyncQueryExecutor executor, IQueryable original)
        {
            Executor = executor;
            _original = original;
            if (original.Provider is AsyncExecutorQueryProvider aeq)
                Provider = aeq;
            else Provider = new AsyncExecutorQueryProvider(original.Provider, Executor);
        }

        internal IAsyncQueryExecutor Executor { get; }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_original).GetEnumerator();
        public Type ElementType => _original.ElementType;
        public Expression Expression => _original.Expression;
        public IQueryProvider Provider { get; }
    }

    class QueryableWithAsyncExecutor<T> : QueryableWithAsyncExecutor, IOrderedQueryable<T>
    {
        private readonly IQueryable<T> _original;

        public IQueryable<T> GenericOriginal => _original;

        public QueryableWithAsyncExecutor(IAsyncQueryExecutor executor, IQueryable<T> original) : base(executor, original)
        {
            _original = original;
        }

        public IEnumerator<T> GetEnumerator() => _original.GetEnumerator();
    }
}