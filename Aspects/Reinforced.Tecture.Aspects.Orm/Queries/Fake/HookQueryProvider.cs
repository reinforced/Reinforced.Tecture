using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Fake
{
    class HookQueryProvider : IQueryProvider
    {
        private readonly IQueryProvider _baseQueryProvider;
        private readonly Auxilary _aux;
        private readonly DescriptionHolder _description;
        public HookQueryProvider(IQueryProvider baseQueryProvider, Auxilary aux, DescriptionHolder description)
        {
            _baseQueryProvider = baseQueryProvider;
            _aux = aux;
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
            return new HookQueryable<TElement>(bs, _aux, _description);
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
            string hash = _aux.IsHashRequired ? expression.CalculateHash() : string.Empty;
            TResult result;
            if (_aux.IsEvaluationNeeded)
            {
                result = _baseQueryProvider.Execute<TResult>(expression);
            }
            else
            {
                result = _aux.Get<TResult>(hash,_description.Description);
            }

            if (_aux.IsTracingNeeded)
            {
                if (_aux.IsEvaluationNeeded)
                {
                    _aux.Query(hash, result, _description.Description ?? $"Obtaining {typeof(TResult)} via O/RM");
                }
                else
                {
                    _aux.Query(hash, "test data", _description.Description ?? $"Obtaining {typeof(TResult)} via O/RM");
                }
            }

            return result;
        }
    }
}
