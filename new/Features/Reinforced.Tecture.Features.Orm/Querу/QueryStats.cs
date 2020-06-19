using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Features.Orm.Querу
{
    public class QueryStats
    {
        internal readonly Dictionary<Type, int> _onlineCollectionUsageStats = new Dictionary<Type, int>();
        internal readonly Dictionary<Type, int> _offlineCollectionUsageStats = new Dictionary<Type, int>();
        internal readonly Dictionary<Type, int> OnlineCollectionUsageStats = new Dictionary<Type, int>();
        internal readonly Dictionary<Type, int> OfflineCollectionUsageStats = new Dictionary<Type, int>();

        public string Stats()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Online (DB queries ) collection requests count (by types):");
            foreach (var collectionUsageStat in _onlineCollectionUsageStats)
            {
                sb.AppendLine(String.Format("{0}: {1} times", collectionUsageStat.Key.Name, collectionUsageStat.Value));
            }

            sb.AppendLine("Offline (cached queries) collection requests count (by types):");
            foreach (var collectionUsageStat in _offlineCollectionUsageStats)
            {
                sb.AppendLine(String.Format("{0}: {1} times", collectionUsageStat.Key.Name, collectionUsageStat.Value));
            }

            return sb.ToString();
        }
    }
}
