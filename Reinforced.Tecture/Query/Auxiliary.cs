using System;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Tracing.Promises;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Query
{
    /// <summary>
    /// Auxiliary tooling for commands and queries processing
    /// </summary>
    public class Auxiliary
    {
        private readonly AuxiliaryContainer _container;
        private readonly Type _channelType;
        private readonly TransactionManager _transactionManager;
        internal Auxiliary(AuxiliaryContainer container, Type channelType, TransactionManager transactionManager)
        {
            _container = container;
            _channelType = channelType;
            _transactionManager = transactionManager;
        }

        /// <summary>
        /// Obtains query transaction for channel
        /// </summary>
        /// <returns>Transaction</returns>
        public ChannelTransaction GetQueryTransaction()
        {
            return _transactionManager.GetQueryTransaction(_channelType);
        }

        /// <summary>
        /// Gets whether saving in saver is required
        /// </summary>
        public bool IsSavingNeeded
        {
            get
            {
                return _container._testDataHolder.Instance == null;
            }
        }

        /// <summary>
        /// Gets whether it is needed to actually run the command
        /// </summary>
        public bool IsCommandRunNeeded
        {
            get
            {
                return _container._testDataHolder.Instance == null;
            }
        }

        /// <summary>
        /// Gets whether it is actually needed to evaluate query to channel and obtain its value (no test data)
        /// </summary>
        public bool IsEvaluationNeeded
        {
            get
            {
                return _container._testDataHolder.Instance == null;
            }
        }

        /// <summary>
        /// Traces query that will be fulfilled later
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <returns>Promised</returns>
        public Promised<T> Promise<T>()
        {
            if (_container.TraceCollector != null)
            {
                if (_container._testDataHolder.Instance != null)
                {
                    return new Contains<T>(_container._testDataHolder.Instance, _container.TraceCollector, _channelType);
                }
                else
                {
                    return new Demands<T>(_container.TraceCollector,_channelType);
                }
            }
            return new Consistency<T>();
        }
    }
}
