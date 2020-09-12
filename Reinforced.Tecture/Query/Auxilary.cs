using System;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Query
{
    /// <summary>
    /// Auxilary tooling for commands and queries processing
    /// </summary>
    public class Auxilary
    {
        private readonly AuxilaryContainer _container;
        private readonly Type _channelType;
        private readonly TransactionManager _transactionManager;
        internal Auxilary(AuxilaryContainer container, Type channelType, TransactionManager transactionManager)
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
        /// Gets whether it is needed to compute query hash
        /// </summary>
        public bool IsHashRequired
        {
            get { return IsTracingNeeded || !IsEvaluationNeeded; }
        }

        /// <summary>
        /// Gets whether it is needed to trace the query
        /// </summary>
        public bool IsTracingNeeded
        {
            get
            {
                return _container.TraceCollector != null;
            }
        }

        /// <summary>
        /// Traces performed query and clones its results
        /// </summary>
        /// <typeparam name="T">Type of query result</typeparam>
        /// <param name="hash">Query hash</param>
        /// <param name="result">Query result</param>
        /// <param name="description">Query description</param>
        public void Query<T>(string hash, T result, string description)
        {
            var type = typeof(T);
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                type = result.GetType();
            }
            if (_container.TraceCollector != null)
            {
                if (_container._testDataHolder.Instance != null)
                {
                    _container.TraceCollector.TestQuery(_channelType, type, hash, result.DeepClone(), description);
                }
                else
                {
                    _container.TraceCollector.Query(_channelType, type, hash, result.DeepClone(), description);
                }
                return;
            }
            throw new TectureException("Test data is not presumed to be collected");
        }

        /// <summary>
        /// Traces performed query without cloning its results
        /// </summary>
        /// <typeparam name="T">Type of query result</typeparam>
        /// <param name="hash">Query hash</param>
        /// <param name="result">Query result</param>
        /// <param name="description">Query description</param>
        public void QueryManuallyClone<T>(string hash, T result, string description)
        {
            var type = typeof(T);
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                type = result.GetType();
            }
            if (_container.TraceCollector != null)
            {
                if (_container._testDataHolder.Instance != null)
                {
                    _container.TraceCollector.TestQuery(_channelType, type, hash, result, description);
                }
                else
                {
                    _container.TraceCollector.Query(_channelType, type, hash, result, description);
                }
                return;
            }
            throw new TectureException("Test data is not presumed to be collected");
        }

        /// <summary>
        /// Retrieves test data for query
        /// </summary>
        /// <typeparam name="T">Type of query result</typeparam>
        /// <param name="hash">Query hash</param>
        /// <param name="description">Query description</param>
        /// <returns>Query result</returns>
        public T Get<T>(string hash, string description = null)
        {
            if (_container._testDataHolder.Instance != null)
            {
                return _container._testDataHolder.Instance.Get<T>(hash, description);
            }
            throw new TectureException("Test data is not provided");
        }

    }
}
