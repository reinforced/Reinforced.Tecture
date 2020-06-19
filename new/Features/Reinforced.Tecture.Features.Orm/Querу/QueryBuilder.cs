using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reinforced.Tecture.Methodics.Orm.Queries;

namespace Reinforced.Tecture.Features.Orm.Queries
{
    internal class QueryBuilder<TEntity> : IQueryFor<TEntity> where TEntity : class
    {
        protected readonly Features.Orm.Queries.Orm Src;
        
        public QueryBuilder(Features.Orm.Queries.Orm src)
        {
            Src = src;
            
            if (Src != null)
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
            get { return _allCached ?? (_allCached = ApplyThats(Src.GetSet<TEntity>())); }
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
            var t = Src.GetSet<T>();
            return t;
        }

        public IQueryFor<TEntity> That(Expression<Func<TEntity, bool>> @where)
        {
            _thatExpressions.Add(@where);
            return this;
        }
    }
}
