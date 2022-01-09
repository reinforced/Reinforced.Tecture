using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Reinforced.Tecture.Aspects.Orm.Queries.Hashing;
using Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.AsyncExecutionAdapter;
using Reinforced.Tecture.Aspects.Orm.Queries.Traced.Queryables.TraceWrapping;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Orm.Queries
{
    public static class AsyncExtensions
    {
        private static Task<U> Call<T, U>(Func<IAsyncQueryExecutor, IQueryable<T>, CancellationToken, Task<U>> method,
            IQueryable<T> q, CancellationToken ct)
        {
            if (q is ITracedQueryable<T> wq)
            {
                var aux = wq.Aspect.Aux;
                var p = aux.Promise<U>();
                ExpressionHashData hash = null;
                if (p is Containing<U> || p is Demanding<U>)
                    hash = wq.Original.Expression.CalculateHash();

                if (p is Containing<U> c)
                {
                    return Task.FromResult(c.Get(hash.Hash, wq.Description.Description));
                }

                var tran = wq.Aspect.Aux.GetQueryTransaction();
                var result = method(wq.Aspect.AsyncExecutorActually, wq.CreateNewOriginal(hash?.ModifiedExpression), ct)
                    .ContinueWith(x =>
                    {
                        if (!x.IsFaulted) tran.Commit();
                        tran.Dispose();
                        if (p is Demanding<U> d)
                        {
                            d.Fullfill(x.Result, hash.Hash, wq.Description.Description);
                        }

                        return x.Result;
                    }, ct);

                return result;
            }

            if (q is QueryableWithAsyncExecutor<T> ae)
                return method(ae.Executor, ae.GenericOriginal, ct);

            throw new TectureOrmAspectException($"{q} does not appear to be valid ORM aspect expression");
        }

        private static Task<U> Call<T, U>(
            Func<IAsyncQueryExecutor, IQueryable<T>, Expression<Func<T, bool>>, CancellationToken, Task<U>> method,
            IQueryable<T> q, Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            if (q is ITracedQueryable<T> wq)
            {
                var aux = wq.Aspect.Aux;
                var p = aux.Promise<U>();

                ExpressionHashData hash = null;
                if (p is Containing<U> || p is Demanding<U>)
                    hash = wq.Original.Expression.CalculateHash(predicate);

                if (p is Containing<U> c)
                {
                    return Task.FromResult(c.Get(hash.Hash, wq.Description.Description));
                }

                var tran = wq.Aspect.Aux.GetQueryTransaction();
                var result = method(wq.Aspect.AsyncExecutorActually, wq.CreateNewOriginal(hash?.ModifiedExpression),
                    predicate, ct).ContinueWith(x =>
                {
                    if (!x.IsFaulted) tran.Commit();
                    tran.Dispose();
                    if (p is Demanding<U> d)
                    {
                        d.Fullfill(x.Result, hash.Hash, wq.Description.Description);
                    }

                    return x.Result;
                }, ct);

                return result;
            }

            if (q is QueryableWithAsyncExecutor<T> ae)
                return method(ae.Executor, ae.GenericOriginal, predicate, ct);

            throw new TectureOrmAspectException($"{q} does not appear to be valid ORM aspect expression");
        }

        private static Task<U> Call<T, U>(
            Func<IAsyncQueryExecutor, IQueryable<T>, Expression<Func<T, U>>, CancellationToken, Task<U>> method,
            IQueryable<T> q, Expression<Func<T, U>> selector, CancellationToken ct)
        {
            if (q is ITracedQueryable<T> wq)
            {
                var aux = wq.Aspect.Aux;
                var p = aux.Promise<U>();
                ExpressionHashData hash = null;
                if (p is Containing<U> || p is Demanding<U>)
                    hash = wq.Original.Expression.CalculateHash(selector);

                if (p is Containing<U> c)
                {
                    return Task.FromResult(c.Get(hash.Hash, wq.Description.Description));
                }

                var tran = wq.Aspect.Aux.GetQueryTransaction();
                var result = method(wq.Aspect.AsyncExecutorActually, wq.CreateNewOriginal(hash?.ModifiedExpression),
                    selector, ct).ContinueWith(x =>
                {
                    if (!x.IsFaulted) tran.Commit();
                    tran.Dispose();
                    if (p is Demanding<U> d)
                    {
                        d.Fullfill(x.Result, hash.Hash, wq.Description.Description);
                    }

                    return x.Result;
                }, ct);

                return result;
            }

            if (q is QueryableWithAsyncExecutor<T> ae)
                method(ae.Executor, ae.GenericOriginal, selector, ct);

            throw new TectureOrmAspectException($"{q} does not appear to be valid ORM aspect expression");
        }

        private static Task<V> Call2<T, U, V>(
            Func<IAsyncQueryExecutor, IQueryable<T>, Expression<Func<T, U>>, CancellationToken, Task<V>> method,
            IQueryable<T> q, Expression<Func<T, U>> selector, CancellationToken ct)
        {
            if (q is ITracedQueryable<T> wq)
            {
                var aux = wq.Aspect.Aux;
                var p = aux.Promise<V>();

                ExpressionHashData hash = null;
                if (p is Containing<V> || p is Demanding<V>)
                    hash = wq.Original.Expression.CalculateHash(selector);

                if (p is Containing<V> c)
                {
                    return Task.FromResult(c.Get(hash.Hash, wq.Description.Description));
                }

                var tran = wq.Aspect.Aux.GetQueryTransaction();
                var result = method(wq.Aspect.AsyncExecutorActually, wq.CreateNewOriginal(hash?.ModifiedExpression),
                    selector, ct).ContinueWith(x =>
                {
                    if (!x.IsFaulted) tran.Commit();
                    tran.Dispose();
                    if (p is Demanding<V> d)
                    {
                        d.Fullfill(x.Result, hash.Hash, wq.Description.Description);
                    }

                    return x.Result;
                }, ct);

                return result;
            }

            if (q is QueryableWithAsyncExecutor<T> ae)
                method(ae.Executor, ae.GenericOriginal, selector, ct);

            throw new TectureOrmAspectException($"{q} does not appear to be valid ORM aspect expression");
        }

        #region Any/All

        /// <summary>
        ///     Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable" /> to check for being empty.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="true" /> if the source sequence contains any elements; otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<bool> AnyAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default) =>
            Call((a, q, ct) => a.AnyAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> whose elements to test for a condition.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="true" /> if any elements in the source sequence pass the test in the specified
        ///     predicate; otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static Task<bool> AnyAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.AnyAsync(q, p, ct), source, predicate, cancellationToken);


        /// <summary>
        ///     Asynchronously determines whether all the elements of a sequence satisfy a condition.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> whose elements to test for a condition.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="true" /> if every element of the source sequence passes the test in the specified
        ///     predicate; otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static Task<bool> AllAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default) => Call((a, q, p, ct) => a.AnyAsync(q, p, ct), source,
            predicate, cancellationToken);

        #endregion

        #region Count/LongCount

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> that contains the elements to be counted.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<int> CountAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.CountAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence that satisfy a condition.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> that contains the elements to be counted.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the sequence that satisfy the condition in the predicate
        ///     function.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static Task<int> CountAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.CountAsync(q, p, ct), source, predicate, cancellationToken);

        /// <summary>
        ///     Asynchronously returns an <see cref="long" /> that represents the total number of elements in a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> that contains the elements to be counted.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<long> LongCountAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.LongCountAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously returns an <see cref="long" /> that represents the number of elements in a sequence
        ///     that satisfy a condition.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> that contains the elements to be counted.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the sequence that satisfy the condition in the predicate
        ///     function.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static Task<long> LongCountAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.LongCountAsync(q, p, ct), source, predicate, cancellationToken);

        #endregion

        #region First/FirstOrDefault

        /// <summary>
        ///     Asynchronously returns the first element of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the first element of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the first element in <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<TSource> FirstAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.FirstAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the first element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the first element of.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the first element in <paramref name="source" /> that passes the test in
        ///     <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <para>
        ///         No element satisfies the condition in <paramref name="predicate" />
        ///     </para>
        ///     <para>
        ///         -or -
        ///     </para>
        ///     <para>
        ///         <paramref name="source" /> contains no elements.
        ///     </para>
        /// </exception>
        public static Task<TSource> FirstAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.FirstAsync(q, p, ct), source, predicate, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the first element of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="default" /> ( <typeparamref name="TSource" /> ) if
        ///     <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.FirstOrDefaultAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the first element of a sequence that satisfies a specified condition
        ///     or a default value if no such element is found.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the first element of.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="default" /> ( <typeparamref name="TSource" /> ) if <paramref name="source" />
        ///     is empty or if no element passes the test specified by <paramref name="predicate" /> ; otherwise, the first
        ///     element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.FirstOrDefaultAsync(q, p, ct), source, predicate, cancellationToken);

        #endregion

        #region Last/LastOrDefault

        /// <summary>
        ///     Asynchronously returns the last element of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the last element of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element in <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<TSource> LastAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.LastAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the last element of.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element in <paramref name="source" /> that passes the test in
        ///     <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <para>
        ///         No element satisfies the condition in <paramref name="predicate" />.
        ///     </para>
        ///     <para>
        ///         -or-
        ///     </para>
        ///     <para>
        ///         <paramref name="source" /> contains no elements.
        ///     </para>
        /// </exception>
        public static Task<TSource> LastAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.LastAsync(q, p, ct), source, predicate, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the last element of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="default" /> ( <typeparamref name="TSource" /> ) if
        ///     <paramref name="source" /> is empty; otherwise, the last element in <paramref name="source" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource> LastOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.LastOrDefaultAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition
        ///     or a default value if no such element is found.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the last element of.
        /// </param>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="default" /> ( <typeparamref name="TSource" /> ) if <paramref name="source" />
        ///     is empty or if no element passes the test specified by <paramref name="predicate" /> ; otherwise, the last
        ///     element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource> LastOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.LastOrDefaultAsync(q, p, ct), source, predicate, cancellationToken);

        #endregion

        #region Single/SingleOrDefault

        /// <summary>
        ///     Asynchronously returns the only element of a sequence, and throws an exception
        ///     if there is not exactly one element in the sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the single element of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <para>
        ///         <paramref name="source" /> contains more than one elements.
        ///     </para>
        ///     <para>
        ///         -or-
        ///     </para>
        ///     <para>
        ///         <paramref name="source" /> contains no elements.
        ///     </para>
        /// </exception>
        public static Task<TSource> SingleAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SingleAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the single element of.
        /// </param>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <para>
        ///         No element satisfies the condition in <paramref name="predicate" />.
        ///     </para>
        ///     <para>
        ///         -or-
        ///     </para>
        ///     <para>
        ///         More than one element satisfies the condition in <paramref name="predicate" />.
        ///     </para>
        ///     <para>
        ///         -or-
        ///     </para>
        ///     <para>
        ///         <paramref name="source" /> contains no elements.
        ///     </para>
        /// </exception>
        public static Task<TSource> SingleAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SingleAsync(q, p, ct), source, predicate, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the only element of a sequence, or a default value if the sequence is empty;
        ///     this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the single element of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence, or <see langword="default" /> (
        ///     <typeparamref name="TSource" />)
        ///     if the sequence contains no elements.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains more than one element.
        /// </exception>
        public static Task<TSource> SingleOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SingleOrDefaultAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to return the single element of.
        /// </param>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />, or <see langword="default" /> ( <typeparamref name="TSource" /> ) if no such element is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     More than one element satisfies the condition in <paramref name="predicate" />.
        /// </exception>
        public static Task<TSource> SingleOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SingleOrDefaultAsync(q, p, ct), source, predicate, cancellationToken);

        #endregion

        #region Min

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> that contains the elements to determine the minimum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource> MinAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.MinAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the minimum resulting value.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the value returned by the function represented by <paramref name="selector" /> .
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> that contains the elements to determine the minimum of.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<TResult> MinAsync<TSource, TResult>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, TResult>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.MinAsync(q, p, ct), source, selector, cancellationToken);

        #endregion

        #region Max

        /// <summary>
        ///     Asynchronously returns the maximum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> that contains the elements to determine the maximum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource> MaxAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.MaxAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the value returned by the function represented by <paramref name="selector" /> .
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> that contains the elements to determine the maximum of.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<TResult> MaxAsync<TSource, TResult>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, TResult>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.MaxAsync(q, p, ct), source, selector, cancellationToken);

        #endregion

        #region Sum

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<decimal> SumAsync(
            this IQueryable<decimal> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<decimal?> SumAsync(
            this IQueryable<decimal?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<decimal> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, decimal>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<decimal?> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, decimal?>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<int> SumAsync(
            this IQueryable<int> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<int?> SumAsync(
            this IQueryable<int?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<int> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, int>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<int?> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, int?>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<long> SumAsync(
            this IQueryable<long> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<long?> SumAsync(
            this IQueryable<long?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<long> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, long>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<long?> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, long?>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<double> SumAsync(
            this IQueryable<double> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<double?> SumAsync(
            this IQueryable<double?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<double> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, double>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<double?> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, double?>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<float> SumAsync(
            this IQueryable<float> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the sum of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the values in the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<float?> SumAsync(
            this IQueryable<float?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.SumAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<float> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, float>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values of type <typeparamref name="TSource" />.
        /// </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<float?> SumAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, float?>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.SumAsync(q, p, ct), source, selector, cancellationToken);

        #endregion

        #region Average

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<decimal> AverageAsync(
            this IQueryable<decimal> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<decimal?> AverageAsync(
            this IQueryable<decimal?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<decimal> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, decimal>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);


        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<decimal?> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, decimal?>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<double> AverageAsync(
            this IQueryable<int> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<double?> AverageAsync(
            this IQueryable<int?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<double> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, int>> selector,
            CancellationToken cancellationToken = default)
            => Call2((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<double?> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, int?>> selector,
            CancellationToken cancellationToken = default)
            => Call2((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<double> AverageAsync(
            this IQueryable<long> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<double?> AverageAsync(
            this IQueryable<long?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<double> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, long>> selector,
            CancellationToken cancellationToken = default)
            => Call2((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<double?> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, long?>> selector,
            CancellationToken cancellationToken = default)
            => Call2((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<double> AverageAsync(
            this IQueryable<double> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<double?> AverageAsync(
            this IQueryable<double?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<double> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, double>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<double?> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, double?>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<float> AverageAsync(
            this IQueryable<float> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="source">
        ///     A sequence of values to calculate the average of.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<float?> AverageAsync(
            this IQueryable<float?> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.AverageAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="source" /> contains no elements.
        /// </exception>
        public static Task<float> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, float>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of values that is obtained
        ///     by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" /> .
        /// </typeparam>
        /// <param name="source"> A sequence of values of type <typeparamref name="TSource" />. </param>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the average of the projected values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.
        /// </exception>
        public static Task<float?> AverageAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, float?>> selector,
            CancellationToken cancellationToken = default)
            => Call((a, q, p, ct) => a.AverageAsync(q, p, ct), source, selector, cancellationToken);

        #endregion

        #region Contains

        /// <summary>
        ///     Asynchronously determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="q" />.
        /// </typeparam>
        /// <param name="q">
        ///     An <see cref="IQueryable{T}" /> to return the single element of.
        /// </param>
        /// <param name="item"> The object to locate in the sequence. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <see langword="true" /> if the input sequence contains the specified value; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="q" /> is <see langword="null" />.
        /// </exception>
        public static Task<bool> ContainsAsync<TSource>(
            this IQueryable<TSource> q,
            TSource item,
            CancellationToken cancellationToken = default)
        {
            if (q is ITracedQueryable<TSource> wq)
            {
                var aux = wq.Aspect.Aux;
                var p = aux.Promise<bool>();

                ExpressionHashData hash = null;
                if (p is Containing<bool> || p is Demanding<bool>)
                    hash = wq.Original.Expression.CalculateHash();
                
                if (p is Containing<bool> c)
                {
                    return Task.FromResult(
                        c.Get(hash.Hash, wq.Description.Description));
                }

                var tran = wq.Aspect.Aux.GetQueryTransaction();
                var result = wq.Aspect.AsyncExecutorActually.ContainsAsync(wq.CreateNewOriginal(hash?.ModifiedExpression), item, cancellationToken)
                    .ContinueWith(x =>
                    {
                        if (!x.IsFaulted) tran.Commit();
                        tran.Dispose();
                        if (p is Demanding<bool> d)
                        {
                            d.Fullfill(x.Result, hash.Hash,
                                wq.Description.Description);
                        }

                        return x.Result;
                    }, cancellationToken);

                return result;
            }

            if (q is QueryableWithAsyncExecutor<TSource> ae)
                return ae.Executor.ContainsAsync(ae.GenericOriginal, item, cancellationToken);

            throw new TectureOrmAspectException($"{q} does not appear to be valid ORM aspect expression");
        }

        #endregion

        #region ToList/Array

        /// <summary>
        ///     Asynchronously creates a <see cref="List{T}" /> from an <see cref="IQueryable{T}" /> by enumerating it
        ///     asynchronously.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to create a list from.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<List<TSource>> ToListAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.ToListAsync(q, ct), source, cancellationToken);

        /// <summary>
        ///     Asynchronously creates an array from an <see cref="IQueryable{T}" /> by enumerating it asynchronously.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to create an array from.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains an array that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> is <see langword="null" />.
        /// </exception>
        public static Task<TSource[]> ToArrayAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default)
            => Call((a, q, ct) => a.ToArrayAsync(q, ct), source, cancellationToken);

        #endregion

        #region ToDictionary

        /// <summary>
        ///     Creates a <see cref="Dictionary{TKey, TValue}" /> from an <see cref="IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to create a <see cref="Dictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="Dictionary{TKey, TSource}" /> that contains selected keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="keySelector" /> is <see langword="null" />.
        /// </exception>
        public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
            this IQueryable<TSource> source,
            Func<TSource, TKey> keySelector,
            CancellationToken cancellationToken = default)
            => ToDictionaryAsync(source, keySelector, e => e, comparer: null, cancellationToken);

        /// <summary>
        ///     Creates a <see cref="Dictionary{TKey, TValue}" /> from an <see cref="IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function and a comparer.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to create a <see cref="Dictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="comparer">
        ///     An <see cref="IEqualityComparer{TKey}" /> to compare keys.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="Dictionary{TKey, TSource}" /> that contains selected keys and values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="keySelector" /> is <see langword="null" />.
        /// </exception>
        public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
            this IQueryable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken = default)
            => ToDictionaryAsync(source, keySelector, e => e, comparer, cancellationToken);

        /// <summary>
        ///     Creates a <see cref="Dictionary{TKey, TValue}" /> from an <see cref="IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector and an element selector function.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <typeparam name="TElement">
        ///     The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to create a <see cref="Dictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="elementSelector"> A transform function to produce a result element value from each element. </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="Dictionary{TKey, TElement}" /> that contains values of type
        ///     <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is <see langword="null" />.
        /// </exception>
        public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(
            this IQueryable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            CancellationToken cancellationToken = default)
            => ToDictionaryAsync(source, keySelector, elementSelector, comparer: null, cancellationToken);

        /// <summary>
        ///     Creates a <see cref="Dictionary{TKey, TValue}" /> from an <see cref="IQueryable{T}" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function, a comparer, and an element selector function.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source" />.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <typeparam name="TElement">
        ///     The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to create a <see cref="Dictionary{TKey, TValue}" /> from.
        /// </param>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="elementSelector"> A transform function to produce a result element value from each element. </param>
        /// <param name="comparer">
        ///     An <see cref="IEqualityComparer{TKey}" /> to compare keys.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="Dictionary{TKey, TElement}" /> that contains values of type
        ///     <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is <see langword="null" />.
        /// </exception>
        public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(
            this IQueryable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken = default)
        {
            if (source is ITracedQueryable<TSource> wq)
            {
                var aux = wq.Aspect.Aux;
                var p = aux.Promise<Dictionary<TKey, TElement>>();
                ExpressionHashData hash = null;
                if (p is Containing<Dictionary<TKey, TElement>>
                    || p is Demanding<Dictionary<TKey, TElement>>)
                    hash = wq.Original.Expression.CalculateHash();
                
                if (p is Containing<Dictionary<TKey, TElement>> c)
                {
                    return Task.FromResult(
                        c.Get(hash.Hash, wq.Description.Description));
                }

                var tran = wq.Aspect.Aux.GetQueryTransaction();
                var result = wq.Aspect.AsyncExecutorActually
                    .ToDictionaryAsync(wq.CreateNewOriginal(hash?.ModifiedExpression), keySelector, elementSelector, comparer, cancellationToken)
                    .ContinueWith(x =>
                    {
                        if (!x.IsFaulted) tran.Commit();
                        tran.Dispose();
                        if (p is Demanding<Dictionary<TKey, TElement>> d)
                        {
                            d.Fullfill(x.Result, hash.Hash,
                                wq.Description.Description);
                        }

                        return x.Result;
                    }, cancellationToken);

                return result;
            }

            if (source is QueryableWithAsyncExecutor<TSource> ae)
                return ae.Executor.ToDictionaryAsync(ae.GenericOriginal, keySelector, elementSelector, comparer,
                    cancellationToken);

            throw new TectureOrmAspectException($"{source} does not appear to be valid ORM aspect expression");
        }

        #endregion
    }
}