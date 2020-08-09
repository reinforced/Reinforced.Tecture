using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Storage.Adapters;

namespace Reinforced.Storage.QueryBuilders
{

    internal class OnlineQueryBuilder<TEntity> : IQueryFor<TEntity> where TEntity : class
    {
        protected readonly ISetAccess _setAccess;
        protected readonly StorageStats _stats;

        private bool _nonTracking = false;
        private readonly Dictionary<Type, List<Expression>> _additionalIncludes = new Dictionary<Type, List<Expression>>();

        public OnlineQueryBuilder(ISetAccess setAccess, StorageStats stats)
        {
            _setAccess = setAccess;
            _stats = stats;
            if (_setAccess != null)
            {
                if (!stats.OnlineCollectionUsageStats.ContainsKey(typeof(TEntity)))
                {
                    stats.OnlineCollectionUsageStats[typeof(TEntity)] = 0;
                }
                stats.OnlineCollectionUsageStats[typeof(TEntity)]++;
            }
        }

        private IQueryable<TEntity> _allCached;

        public IQueryable<TEntity> All
        {
            get { return _allCached ?? (_allCached = ApplyIncludes(_setAccess.Set<TEntity>())); }
        }

        private readonly List<Expression> _includeExpressions = new List<Expression>();
        private readonly List<Expression<Func<TEntity, bool>>> _thatExpressions = new List<Expression<Func<TEntity, bool>>>(2);

        public IQueryFor<TEntity> Include<TProp>(Expression<Func<TEntity, TProp>> include)
        {
            _includeExpressions.Add(include);
            return this;
        }

        public IQueryFor<TEntity> AlsoInclude<T>(Expression<Func<T, object>> include)
        {
            var list = _additionalIncludes.GetOrCreate(typeof(T));
            list.Add(include);
            return this;
        }

        public IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> source)
        {
            source = ApplyThats(source);

            source = ApplyIncludes(source, _includeExpressions);
            if (_nonTracking)
            {
                source = _setAccess.NoTracking(source);
            }
            return source;
        }

        private IQueryable<TEntity> ApplyThats(IQueryable<TEntity> source)
        {
            foreach (var thatExpression in _thatExpressions)
            {
                var te = thatExpression;
                source = source.Where(te);
            }
            return source;
        }

        public IQueryable<T> ApplyIncludes<T>(IQueryable<T> source, IEnumerable<Expression> includeExpressions) where T : class
        {
            foreach (var includeExpression in includeExpressions)
            {
                source = _setAccess.Include<T>(source,includeExpression);
            }

            return source;
        }

        public IQueryable<T> Joined<T>() where T : class
        {
            var t = _setAccess.Set<T>();
            if (!_additionalIncludes.ContainsKey(typeof(T))) return t;
            return ApplyIncludes(t, _additionalIncludes[typeof(T)]);
        }

        public IQueryFor<TEntity> That(Expression<Func<TEntity, bool>> @where)
        {
            _thatExpressions.Add(@where);
            return this;
        }

        public IQueryFor<TEntity> NoTracking()
        {
            _nonTracking = true;
            return this;
        }
    }
}
