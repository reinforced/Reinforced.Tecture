using System.Linq;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.AsyncExecutionAdapter
{
    class AsyncExecutorQueryProvider : IQueryProvider
    {
        private readonly IQueryProvider _original;

        public AsyncExecutorQueryProvider(IQueryProvider original, IAsyncQueryExecutor executor)
        {
            _original = original;
            Executor = executor;
        }

        internal IAsyncQueryExecutor Executor { get; }
        
        public IQueryable CreateQuery(Expression expression)
        {
            return new QueryableWithAsyncExecutor(Executor, _original.CreateQuery(expression));
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new QueryableWithAsyncExecutor<TElement>(Executor, _original.CreateQuery<TElement>(expression));
        }

        public object Execute(Expression expression) => _original.Execute(expression);

        public TResult Execute<TResult>(Expression expression) => _original.Execute<TResult>(expression);
    }
}