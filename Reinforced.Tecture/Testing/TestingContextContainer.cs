using System;
using System.Collections.Generic;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// Auxiliary tools for queries testing and tracing
    /// </summary>
    internal class TestingContextContainer
    {
        internal readonly TestDataProvider _testDataProvider;
        internal TraceCollector TraceCollector { get; set; }
        private readonly TransactionManager _transactionManager;

        internal TestingContextContainer(TestDataProvider testDataProvider, TransactionManager transactionManager)
        {
            _testDataProvider = testDataProvider;
            _transactionManager = transactionManager;
        }

        private readonly Dictionary<Type, TestingContext> _cache = new Dictionary<Type, TestingContext>();

        /// <summary>
        /// Obtains testing context for particular channel
        /// </summary>
        /// <param name="channelType">Type of channel</param>
        /// <returns>Testing context for channel</returns>
        internal TestingContext ForChannel(Type channelType)
        {
            if (!_cache.ContainsKey(channelType))
            {
                _cache[channelType] = new TestingContext(this, channelType, _transactionManager);
            }
            return _cache[channelType];
        }
    }
}
