using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Transactional
{
    class TransactionalQueryable : IOrderedQueryable
    {
        private readonly IQueryable _original;
        private readonly Auxilary _auxilary;

        public TransactionalQueryable(Auxilary auxilary, IQueryable original)
        {
            _auxilary = auxilary;
            _original = original;
            Provider = new TransactionalQueryProvider(auxilary, original.Provider);
        }

        public IEnumerator GetEnumerator()
        {
            return new TransactionalEnumerator(_original.GetEnumerator(), _auxilary);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"></see> is executed.</summary>
        /// <returns>A <see cref="T:System.Type"></see> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        public Type ElementType
        {
            get { return _original.ElementType; }
        }

        /// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"></see>.</summary>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression"></see> that is associated with this instance of <see cref="T:System.Linq.IQueryable"></see>.</returns>
        public Expression Expression
        {
            get { return _original.Expression; }
        }

        /// <summary>Gets the query provider that is associated with this data source.</summary>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider"></see> that is associated with this data source.</returns>
        public IQueryProvider Provider { get; }
    }
}
