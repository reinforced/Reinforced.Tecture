using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Reinforced.Tecture.CleanPlayground
{
    class FakeQueryable<T> : IOrderedQueryable<T>
    {
        private readonly IQueryable<T> _baseQueryable;
        private readonly IQueryProvider _provider;

        public FakeQueryable(IQueryable<T> baseQueryable)
        {
            _baseQueryable = baseQueryable;
            _provider = new FakeQueryProvider(baseQueryable.Provider);
        }


        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var benum =  _baseQueryable.GetEnumerator();
            var benum2 = _baseQueryable.GetEnumerator();
            if (benum == benum2)
            {
                int i = 0;
            }
            return benum;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _baseQueryable).GetEnumerator();
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