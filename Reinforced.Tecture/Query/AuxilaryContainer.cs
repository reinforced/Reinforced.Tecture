using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Query
{
    /// <summary>
    /// Auxilary tools for queries testing and tracing
    /// </summary>
    internal class AuxilaryContainer
    {
        internal readonly TestDataHolder _testDataHolder;
        internal TraceCollector TraceCollector { get; set; }
        private readonly TransactionManager _transactionManager;
        internal AuxilaryContainer(TestDataHolder testDataHolder, TransactionManager transactionManager)
        {
            _testDataHolder = testDataHolder;
            _transactionManager = transactionManager;
        }

        private readonly Dictionary<Type, Auxilary> _cache = new Dictionary<Type, Auxilary>();

        internal Auxilary ForChannel(Type channelType)
        {
            if (!_cache.ContainsKey(channelType))
            {
                _cache[channelType] = new Auxilary(this, channelType, _transactionManager);
            }
            return _cache[channelType];
        }

    }
}
