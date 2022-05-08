using System;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Aspects.Orm.Queries.Hashing;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.TraceWrapping
{
    class TracedQueryProvider : IQueryProvider
    {
        public IQueryProvider Original { get; }

        public Orm.Query Aspect { get; }

        public DescriptionHolder Description { get; }

        public Read Read { get; }

        public TracedQueryProvider(IQueryProvider original, Orm.Query aspect, DescriptionHolder description, Read read)
        {
            Original = original;
            Aspect = aspect;
            Description = description;
            Read = read;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var bs = Original.CreateQuery(expression);
            return new TracedQueryable(bs, Aspect, Description, Read);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var bs = Original.CreateQuery<TElement>(expression);
            return new TracedQueryable<TElement>(bs, Aspect, Description, false, Read);
        }

        public object Execute(Expression expression)
        {
            var p = Aspect.Context.Promise<object>(Read);
            ExpressionHashData hash = null;
            if (p is Containing<object> || p is Demanding<object>)
                hash = expression.CalculateHash();

            if (p is Containing<object> c)
                return c.Get(hash.Hash, Description.Description);

            try
            {
                var result = Original.Execute(hash == null ? expression : hash.ModifiedExpression);
                if (p is NotifyCompleted<object> nc) nc.Fulfill(Description.Description);

                if (p is Demanding<object> d)
                    d.Fulfill(result, hash.Hash, Description.Description);
                return result;
            }
            catch (Exception ex)
            {
                if (p is Catching<object> d) d.Fulfill(ex, Description.Description);
                throw;
            }
        }

        /// <summary>Executes the strongly-typed query represented by a specified expression tree.</summary>
        /// <param name="expression">An expression tree that represents a LINQ query.</param>
        /// <typeparam name="TResult">The type of the value that results from executing the query.</typeparam>
        /// <returns>The value that results from executing the specified query.</returns>
        public TResult Execute<TResult>(Expression expression)
        {
            var p = Aspect.Context.Promise<TResult>(Read);

            ExpressionHashData hash = null;
            if (p is Containing<TResult> || p is Demanding<TResult>)
                hash = expression.CalculateHash();

            try
            {
                if (p is Containing<TResult> c)
                    return c.Get(hash.Hash, Description.Description);

                var result = Original.Execute<TResult>(hash == null ? expression : hash.ModifiedExpression);
                if (p is NotifyCompleted<TResult> nc) nc.Fulfill(Description.Description);

                if (p is Demanding<TResult> d)
                    d.Fulfill(result, hash.Hash, Description.Description);

                return result;
            }
            catch (Exception ex)
            {
                if (p is Catching<TResult> d) d.Fulfill(ex, Description.Description);
                throw;
            }
        }
    }
}