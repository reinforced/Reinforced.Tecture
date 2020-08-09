using System;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Features.Orm.Queries.Fake
{
    class HookQueryProvider : IQueryProvider
    {
        private readonly IQueryProvider _baseQueryProvider;
        private readonly TestData _qs;
        private readonly DescriptionHolder _description;
        public HookQueryProvider(IQueryProvider baseQueryProvider, TestData qs, DescriptionHolder description)
        {
            _baseQueryProvider = baseQueryProvider;
            _qs = qs;
            _description = description;
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
            return new HookQueryable<TElement>(bs, _qs, _description);
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
            if (_qs is Collecting data)
            {
                var value = _baseQueryProvider.Execute<TResult>(expression);
                data.Put(hash, value, _description.Description);
                return value;
            }

            if (_qs is Providing testData)
            {
                var val = testData.Get<TResult>(hash);
                return val;
            }

            throw new TestDataTypeMismatchException();
        }
    }
}
