using System.Collections.Generic;
using Reinforced.Storage.Adapters;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.QueryBuilders
{
    class SetManager
    {
        private readonly ISetAccess _sets;
        private readonly StorageStats _stats;

        public SetManager(ISetAccess sets, StorageStats stats)
        {
            _sets = sets;
            _stats = stats;
        }

        public IQueryFor<T> Get<T>() where T : class
        {
            return new OnlineQueryBuilder<T>(_sets, _stats);
        }

        public IEnumerable<T> RawQuery<T>(DirectSqlSideEffect sql) where T : class
        {
            return _sets.RawQuery<T>(sql);
        }


    }
}
