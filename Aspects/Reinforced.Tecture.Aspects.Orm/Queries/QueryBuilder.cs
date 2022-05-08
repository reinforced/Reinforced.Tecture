using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Aspects.Orm.Queries
{
    internal class QueryBuilder<TEntity> : IQueryFor<TEntity> where TEntity : class
    {
        private readonly Orm.Query _src;
        private readonly Read _read;
        public QueryBuilder(Orm.Query src, Read read)
        {
            _src = src;
            _read = read;

            if (_src != null)
            {
                if (!src.Stats.OnlineCollectionUsageStats.ContainsKey(typeof(TEntity)))
                {
                    src.Stats.OnlineCollectionUsageStats[typeof(TEntity)] = 0;
                }
                src.Stats.OnlineCollectionUsageStats[typeof(TEntity)]++;
            }
        }

        private IQueryable<TEntity> _allCached;

        public IQueryable<TEntity> All
        {
            get { return _allCached ?? (_allCached = ApplyThats(_src.GetSet<TEntity>(_read))); }
        }

     
        private readonly List<Expression<Func<TEntity, bool>>> _thatExpressions = new List<Expression<Func<TEntity, bool>>>(2);
        
     
        private IQueryable<TEntity> ApplyThats(IQueryable<TEntity> source)
        {
            foreach (var thatExpression in _thatExpressions)
            {
                var te = thatExpression;
                source = source.Where(te);
            }
            return source;
        }

        
        public IQueryable<T> Joined<T>() where T : class
        {
            var t = _src.GetSet<T>(_read);
            return t;
        }

        public IQueryFor<TEntity> That(Expression<Func<TEntity, bool>> @where)
        {
            _thatExpressions.Add(@where);
            return this;
        }
    }
}
