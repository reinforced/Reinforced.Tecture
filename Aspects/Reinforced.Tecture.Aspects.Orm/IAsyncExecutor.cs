using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reinforced.Tecture.Aspects.Orm
{

    /// <summary>
    /// Asynchronous queries executor
    /// </summary>
    public interface IAsyncQueryExecutor
    {
        Task<bool> AnyAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);

        Task<bool> AllAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);

        Task<int> CountAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<int> CountAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<long> LongCountAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<long> LongCountAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TSource> FirstAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TSource> FirstAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TSource> FirstOrDefaultAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TSource> FirstOrDefaultAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TSource> LastAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TSource> LastAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TSource> LastOrDefaultAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TSource> LastOrDefaultAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TSource> SingleAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TSource> SingleAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TSource> SingleOrDefaultAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TSource> SingleOrDefaultAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default);


        Task<TSource> MinAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TResult> MinAsync<TSource, TResult>(IQueryable<TSource> source,
            Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default);

        Task<TSource> MaxAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TResult> MaxAsync<TSource, TResult>(IQueryable<TSource> source,
            Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default);

        #region Sum

        Task<decimal> SumAsync(IQueryable<decimal> source,
            CancellationToken cancellationToken = default);

        Task<decimal?> SumAsync(IQueryable<decimal?> source,
            CancellationToken cancellationToken = default);

        Task<decimal> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, decimal>> selector,
            CancellationToken cancellationToken = default);

        Task<decimal?> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, decimal?>> selector,
            CancellationToken cancellationToken = default);

        Task<int> SumAsync(IQueryable<int> source,
            CancellationToken cancellationToken = default);

        Task<int?> SumAsync(IQueryable<int?> source,
            CancellationToken cancellationToken = default);

        Task<int> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, int>> selector,
            CancellationToken cancellationToken = default);

        Task<int?> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, int?>> selector,
            CancellationToken cancellationToken = default);

        Task<long> SumAsync(IQueryable<long> source,
            CancellationToken cancellationToken = default);

        Task<long?> SumAsync(IQueryable<long?> source,
            CancellationToken cancellationToken = default);

        Task<long> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, long>> selector,
            CancellationToken cancellationToken = default);

        Task<long?> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, long?>> selector,
            CancellationToken cancellationToken = default);

        Task<double> SumAsync(IQueryable<double> source,
            CancellationToken cancellationToken = default);

        Task<double?> SumAsync(IQueryable<double?> source,
            CancellationToken cancellationToken = default);

        Task<double> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, double>> selector,
            CancellationToken cancellationToken = default);

        Task<double?> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, double?>> selector,
            CancellationToken cancellationToken = default);

        Task<float> SumAsync(IQueryable<float> source,
            CancellationToken cancellationToken = default);

        Task<float?> SumAsync(IQueryable<float?> source,
            CancellationToken cancellationToken = default);

        Task<float> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, float>> selector,
            CancellationToken cancellationToken = default);

        Task<float?> SumAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, float?>> selector,
            CancellationToken cancellationToken = default);

        #endregion

        #region Average


        Task<decimal> AverageAsync(IQueryable<decimal> source,
            CancellationToken cancellationToken = default);

        Task<decimal?> AverageAsync(IQueryable<decimal?> source,
            CancellationToken cancellationToken = default);

        Task<decimal> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, decimal>> selector,
            CancellationToken cancellationToken = default);

        Task<decimal?> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, decimal?>> selector,
            CancellationToken cancellationToken = default);

        Task<double> AverageAsync(IQueryable<int> source,
            CancellationToken cancellationToken = default);

        Task<double?> AverageAsync(IQueryable<int?> source,
            CancellationToken cancellationToken = default);

        Task<double> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, int>> selector,
            CancellationToken cancellationToken = default);

        Task<double?> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, int?>> selector,
            CancellationToken cancellationToken = default);

        Task<double> AverageAsync(IQueryable<long> source,
            CancellationToken cancellationToken = default);

        Task<double?> AverageAsync(IQueryable<long?> source,
            CancellationToken cancellationToken = default);

        Task<double> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, long>> selector,
            CancellationToken cancellationToken = default);

        Task<double?> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, long?>> selector,
            CancellationToken cancellationToken = default);

        Task<double> AverageAsync(IQueryable<double> source,
            CancellationToken cancellationToken = default);

        Task<double?> AverageAsync(IQueryable<double?> source,
            CancellationToken cancellationToken = default);

        Task<double> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, double>> selector,
            CancellationToken cancellationToken = default);

        Task<double?> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, double?>> selector,
            CancellationToken cancellationToken = default);

        Task<float> AverageAsync(IQueryable<float> source,
            CancellationToken cancellationToken = default);

        Task<float?> AverageAsync(IQueryable<float?> source,
            CancellationToken cancellationToken = default);

        Task<float> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, float>> selector,
            CancellationToken cancellationToken = default);

        Task<float?> AverageAsync<TSource>(IQueryable<TSource> source,
            Expression<Func<TSource, float?>> selector,
            CancellationToken cancellationToken = default);


        #endregion

        Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);

        Task<TSource[]> ToArrayAsync<TSource>(IQueryable<TSource> source,
            CancellationToken cancellationToken = default);

        Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(IQueryable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer,
            CancellationToken cancellationToken = default);

        Task<bool> ContainsAsync<TSource>(IQueryable<TSource> source,
            TSource item,
            CancellationToken cancellationToken = default);
    }
}
