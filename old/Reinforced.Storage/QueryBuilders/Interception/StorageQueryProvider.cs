using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.QueryBuilders.Interception
{
    struct StorageQueryProvider : IQueryProvider
    {
        internal readonly Effects _effects;
        private readonly IQueryProvider _originalQueryProvider;

        public StorageQueryProvider(Effects effects, IQueryProvider originalQuery)
        {
            _effects = effects;
            _originalQueryProvider = originalQuery;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var oq = _originalQueryProvider.CreateQuery(expression);
            return new StorageQueryable(oq,this);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var oq = _originalQueryProvider.CreateQuery<TElement>(expression);
            return new StorageQueryable<TElement>(oq, this);
        }

        public object Execute(Expression expression)
        {
            return _originalQueryProvider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _originalQueryProvider.Execute<TResult>(expression);
        }
    }
}
