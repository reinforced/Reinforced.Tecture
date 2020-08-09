using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Features.Orm.Queries.Fake
{
    class HookQueryable<T> : IOrderedQueryable<T>
    {
        private readonly IQueryable<T> _baseQueryable;
        private readonly IQueryProvider _provider;
        private readonly TestData _qs;
        internal DescriptionHolder _description;

        public HookQueryable(IQueryable<T> baseQueryable, TestData qs, DescriptionHolder descrHolder)
        {
            _baseQueryable = baseQueryable;
            _qs = qs;
            _description = descrHolder ?? new DescriptionHolder();
            _provider = new HookQueryProvider(baseQueryable.Provider, _qs, _description);
        }


        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var hash = Expression.CalculateHash();
            if (_qs is Collecting data)
                return new HookEnumerator<T>(hash, _baseQueryable.GetEnumerator(), data, _description);

            if (_qs is Providing testData)
            {
                var result = testData.Get<T[]>(hash);
                return new ArrayEnumerator<T>(result);
            }
            throw new TestDataTypeMismatchException();
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            var hash = Expression.CalculateHash();
            if (_qs is Collecting data)
                return new HookEnumerator(hash, ((IEnumerable)_baseQueryable).GetEnumerator(), data, _description);

            if (_qs is Providing testData)
            {
                var result = testData.Get<Array>(hash);
                return result.GetEnumerator();
            }

            throw new TestDataTypeMismatchException();
        }

        /// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.</summary>
        /// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        public Type ElementType
        {
            get
            {
                return _baseQueryable.ElementType;
            }
        }

        /// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.</summary>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
        public Expression Expression
        {
            get
            {
                return _baseQueryable.Expression;
            }
        }

        /// <summary>Gets the query provider that is associated with this data source.</summary>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider" /> that is associated with this data source.</returns>
        public IQueryProvider Provider
        {
            get
            {
                return _provider;
            }
        }
    }
}
