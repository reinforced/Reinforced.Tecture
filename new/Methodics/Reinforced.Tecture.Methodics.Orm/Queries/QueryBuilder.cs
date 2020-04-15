using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reinforced.Tecture.Methodics.Orm.Queries
{
    internal class QueryBuilder<TEntity> : IQueryFor<TEntity> where TEntity : class
    {
        protected readonly IOrmQueryRuntime _runtime;
        
        public QueryBuilder(IOrmQueryRuntime runtime)
        {
            _runtime = runtime;
            
            if (_runtime != null)
            {
                if (!runtime.Stats.OnlineCollectionUsageStats.ContainsKey(typeof(TEntity)))
                {
                    runtime.Stats.OnlineCollectionUsageStats[typeof(TEntity)] = 0;
                }
                runtime.Stats.OnlineCollectionUsageStats[typeof(TEntity)]++;
            }
        }

        private IQueryable<TEntity> _allCached;

        public IQueryable<TEntity> All
        {
            get { return _allCached ?? (_allCached = ApplyThats(_runtime.Get<TEntity>())); }
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
            var t = _runtime.Get<T>();
            return t;
        }

        public IQueryFor<TEntity> That(Expression<Func<TEntity, bool>> @where)
        {
            _thatExpressions.Add(@where);
            return this;
        }
    }
}
