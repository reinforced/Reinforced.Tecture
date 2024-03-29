﻿using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Aspects.Orm.Queries.Hashing;
using Reinforced.Tecture.Aspects.Orm.Queries.Traced.Enumerators.Nongeneric;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.TraceWrapping
{
    class TracedQueryable : IOrderedQueryable
    {
        public Orm.Query Aspect { get; }

        public IQueryable Original { get; }

        public DescriptionHolder Description { get; }
        
        public Read Read { get; }

        public TracedQueryable(IQueryable original, Orm.Query aspect, DescriptionHolder description, Read read)
        {
            Original = original;
            Aspect = aspect;
            Description = description;
            Read = read;
        }

        public IQueryable CreateNewOriginal(Expression cleanExpression = null) =>
            cleanExpression != null
                ? Original.Provider.CreateQuery(cleanExpression)
                : Original;
        
        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator GetEnumerator()
        {
            var p = Aspect.Context.Promise<IEnumerable>(Read);

            ExpressionHashData hash = null;
            if (p is Containing<IEnumerable> || p is Demanding<IEnumerable>)
                hash = Expression.CalculateHash();
            try
            {
                if (p is Containing<IEnumerable> c)
                {
                    var td = c.Get(hash.Hash, Description.Description);
                    return td.GetEnumerator();
                }

                var tran = Aspect.Context.GetQueryTransaction();
                var originalEnumerator = CreateNewOriginal(hash?.ModifiedExpression).GetEnumerator();
                if (p is NotifyCompleted<IEnumerable> nc) nc.Fulfill(Description.Description);

                if (p is Demanding<IEnumerable> d)
                {
                    var result = new TracedEnumerator(originalEnumerator, tran);
                    result.Demands(d, hash.Hash, Description);
                    return result;
                }

                return originalEnumerator;
            }
            catch (Exception ex)
            {
                if (p is Catching<IEnumerable> d) 
                    d.Fulfill(ex, Description.Description);
                throw;
            }
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
                return Original.Expression;
            }
        }


        private IQueryProvider _provider;

        public IQueryProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new TracedQueryProvider(Original.Provider, Aspect, Description,Read);
                }
                return _provider;
            }
        }
    }
}
