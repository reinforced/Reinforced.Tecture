using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Aspects.Orm.Queries.Hashing;
using Reinforced.Tecture.Aspects.Orm.Queries.Traced.Enumerators.Generic;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.TraceWrapping
{
    class TracedQueryable<T> : IOrderedQueryable<T>, ITracedQueryable<T>
    {
        public Orm.Query Aspect { get; }

        public IQueryable<T> Original { get; }

        public DescriptionHolder Description { get; }

        private Expression _originalExpression;

        public TracedQueryable(IQueryable<T> original, Orm.Query aspect, DescriptionHolder description, bool stopHashingCrutch)
        {
            Original = original;
            _originalExpression = original.Expression;
            if (stopHashingCrutch)
            {
                _originalExpression = StopHashingCrutch.Apply<T>(_originalExpression);
            }
            Aspect = aspect;
            Description = description;
        }

        public IQueryable<T> CreateNewOriginal(Expression cleanExpression = null) =>
            cleanExpression != null
                ? Original.Provider.CreateQuery<T>(cleanExpression)
                : Original;

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var p = Aspect.Context.Promise<IEnumerable<T>>();

            ExpressionHashData hash = null;
            if (p is Containing<IEnumerable<T>> || p is Demanding<IEnumerable<T>>)
                hash = Expression.CalculateHash();
            try
            {
                if (p is Containing<IEnumerable<T>> c)
                {
                    var td = c.Get(hash.Hash, Description.Description);
                    return td.GetEnumerator();
                }

                var tran = Aspect.Context.GetQueryTransaction();
                var originalEnumerator = CreateNewOriginal(hash?.ModifiedExpression).GetEnumerator();
                if (p is NotifyCompleted<IEnumerable<T>> nc) nc.Fulfill(Description.Description);
               

                if (p is Demanding<IEnumerable<T>> d)
                {
                    var result = new WrappedEnumerator<T>(originalEnumerator, tran);
                    result.Demands(d, hash.Hash, Description);
                    return result;
                }

                return originalEnumerator;
            }
            catch (Exception ex)
            {
                if (p is Catching<IEnumerable<T>> d) 
                    d.Fulfill(ex, Description.Description);
                throw;
            }
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.</summary>
        /// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        public Type ElementType
        {
            get
            {
                return Original.ElementType;
            }
        }

        /// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.</summary>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
        public Expression Expression
        {
            get
            {
                return _originalExpression;
            }
        }

        private IQueryProvider _provider;
        public IQueryProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new TracedQueryProvider(Original.Provider, Aspect, Description);
                }
                return _provider;
            }
        }
    }
}
