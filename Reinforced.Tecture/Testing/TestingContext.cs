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
        /// Gets whether saving in saver is required
        /// </summary>
        public bool IsSavingNeeded => _container._testDataProvider.Instance == null;

        /// <summary>
        /// Gets whether it is needed to actually run the command
        /// </summary>
        public bool IsCommandRunNeeded => _container._testDataProvider.Instance == null;

        /// <summary>
        /// Gets whether it is actually needed to evaluate query to channel and obtain its value (no test data)
        /// </summary>
        public bool IsEvaluationNeeded => _container._testDataProvider.Instance == null;

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
                return new Demands<T>(_container.TraceCollector, _channelType);
            }
            return new Consistency<T>();
        }
    }
}
