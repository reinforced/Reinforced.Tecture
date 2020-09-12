using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Transactional
{
    class TransactionalQueryProvider : IQueryProvider
    {
        private readonly Auxilary _auxilary;
        private readonly IQueryProvider _original;
        public TransactionalQueryProvider(Auxilary auxilary, IQueryProvider original)
        {
            _auxilary = auxilary;
            _original = original;
        }


        /// <summary>Constructs an <see cref="T:System.Linq.IQueryable"></see> object that can evaluate the query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>An <see cref="T:System.Linq.IQueryable"></see> that can evaluate the query represented by the specified expression tree.</returns>
        public IQueryable CreateQuery(Expression expression)
        {
            return new TransactionalQueryable(_auxilary,_original.CreateQuery(expression));
        }

        /// <summary>Constructs an <see cref="T:System.Linq.IQueryable`1"></see> object that can evaluate the query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <typeparam name="TElement">The type of the elements of the <see cref="T:System.Linq.IQueryable`1"></see> that is returned.</typeparam>
        /// <returns>An <see cref="T:System.Linq.IQueryable`1"></see> that can evaluate the query represented by the specified expression tree.</returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TransactionalQueryable<TElement>(_auxilary,_original.CreateQuery<TElement>(expression));
        }

        /// <summary>Executes the query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <returns>The value that results from executing the specified query.</returns>
        public object Execute(Expression expression)
        {
            using (var t = _auxilary.GetQueryTransaction())
            {
                var r = _original.Execute(expression);
                t.Commit();
                return r;
            }
        }

        /// <summary>Executes the strongly-typed query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <typeparam name="TResult">The type of the value that results from executing the query.</typeparam>
        /// <returns>The value that results from executing the specified query.</returns>
        public TResult Execute<TResult>(Expression expression)
        {
            using (var t = _auxilary.GetQueryTransaction())
            {
                var r = _original.Execute<TResult>(expression);
                t.Commit();
                return r;
            }
        }
    }
}
