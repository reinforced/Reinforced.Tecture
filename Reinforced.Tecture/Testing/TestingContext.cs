using System;
using Reinforced.Tecture.Tracing.Promises;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// Auxiliary tooling for commands and queries processing
    /// </summary>
    public class TestingContext
    {
        private readonly TestingContextContainer _container;
        private readonly Type _channelType;
        private readonly TransactionManager _transactionManager;
        internal TestingContext(TestingContextContainer container, Type channelType, TransactionManager transactionManager)
        {
            _container = container;
            _channelType = channelType;
            _transactionManager = transactionManager;
        }

        /// <summary>
        /// Obtains query transaction for channel
        /// </summary>
        /// <returns>Transaction</returns>
        public ChannelTransaction GetQueryTransaction() => _transactionManager.GetQueryTransaction(_channelType);

        /// <summary>
        /// Gets whether test data is provided and query to real sources is not needed
        /// </summary>
        public bool ProvidesTestData => _container._testDataProvider.Instance != null;

        /// <summary>
        /// Gets whether channel queries and commands will be captured
        /// </summary>
        public bool CollectsTestData => _container.TraceCollector != null;
        
        /// <summary>
        /// Gets whether trace collects data in light mode
        /// </summary>
        public bool? LightMode => _container?.TraceCollector.LightMode;
        

        /// <summary>
        /// Traces query that will be fulfilled later
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <returns>Promised</returns>
        public Promised<T> Promise<T>()
        {
            if (_container._testDataProvider.Instance != null)
            {
                return new Contains<T>(_container._testDataProvider.Instance, _container.TraceCollector, _channelType);
            }

            if (_container.TraceCollector != null)
            {
                if (_container.TraceCollector.LightMode)
                {
                    return new LightDemands<T>(_container.TraceCollector, _channelType);
                }
                return new Demands<T>(_container.TraceCollector, _channelType);
            }
            return new Consistency<T>();
        }
    }
}
