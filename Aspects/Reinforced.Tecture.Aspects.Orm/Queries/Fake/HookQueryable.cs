using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Fake
{
    class HookQueryable : IOrderedQueryable
    {
        private readonly IQueryable _baseQueryable;
        private readonly IQueryProvider _provider;
        private readonly Auxiliary _aux;
        internal readonly DescriptionHolder _description;

        public HookQueryable(IQueryable baseQueryable, Auxiliary aux, DescriptionHolder descrHolder)
        {
            _baseQueryable = baseQueryable;
            _aux = aux;
            _description = descrHolder ?? new DescriptionHolder();
            _provider = new HookQueryProvider(baseQueryable.Provider, _aux, _description);
        }


        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator GetEnumerator()
        {
            var hash = _aux.IsHashRequired ? Expression.CalculateHash() : string.Empty;
            IEnumerator result;
            IEnumerable data = null;
            if (_aux.IsEvaluationNeeded)
            {
                result = _baseQueryable.GetEnumerator();
            }
            else
            {
                data = _aux.Get<IEnumerable>(hash, _description.Description);
                result = data.GetEnumerator();
            }

            if (_aux.IsTracingNeeded)
            {
                if (_aux.IsEvaluationNeeded)
                {
                    result = new HookEnumerator(hash, result, _aux, _description);
                }
                else
                {
                    _aux.Query(hash, data, _description.Description);
                }
            }

            return result;
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
