using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Features.Orm.Queries.Fake
{
    class HookQueryProvider : IQueryProvider
    {
        private readonly IQueryProvider _baseQueryProvider;
        private readonly IQueryStore _queryStore;
        public HookQueryProvider(IQueryProvider baseQueryProvider, IQueryStore queryStore)
        {
            _baseQueryProvider = baseQueryProvider;
            _queryStore = queryStore;
        }

        /// <summary>Constructs an <see cref="T:System.Linq.IQueryable" /> object that can evaluate the query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>An <see cref="T:System.Linq.IQueryable" /> that can evaluate the query represented by the specified expression tree.</returns>
        public IQueryable CreateQuery(Expression expression)
        {
            return _baseQueryProvider.CreateQuery(expression);
        }

        /// <summary>Constructs an <see cref="T:System.Linq.IQueryable`1" /> object that can evaluate the query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <typeparam name="TElement">The type of the elements of the <see cref="T:System.Linq.IQueryable`1" /> that is returned.</typeparam>
        /// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that can evaluate the query represented by the specified expression tree.</returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var bs = _baseQueryProvider.CreateQuery<TElement>(expression);
            return new HookQueryable<TElement>(bs, _queryStore);
        }

        /// <summary>Executes the query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>The value that results from executing the specified query.</returns>
        public object Execute(Expression expression)
        {
            return _baseQueryProvider.Execute(expression);
        }

        /// <summary>Executes the strongly-typed query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <typeparam name="TResult">The type of the value that results from executing the query.</typeparam>
        /// <returns>The value that results from executing the specified query.</returns>
        public TResult Execute<TResult>(Expression expression)
        {
            var hash = expression.CalculateHash();
            if (_queryStore.State == QueryMemorizeState.Put)
            {
                var value = _baseQueryProvider.Execute<TResult>(expression);
                _queryStore.Put(hash,value);
                return value;
            }

            var val = _queryStore.Get<TResult>(hash);
            return val;
        }
    }
}
