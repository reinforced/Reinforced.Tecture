using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Cloning;

namespace Reinforced.Tecture.Query
{
    public class Auxilary
    {
        private readonly AuxilaryContainer _container;
        private readonly Type _channelType;

        internal Auxilary(AuxilaryContainer container, Type channelType)
        {
            _container = container;
            _channelType = channelType;
        }

        public bool IsSavingNeeded
        {
            get
            {
                return _container._testDataHolder.Instance == null;
            }
        }

        public bool IsCommandRunNeeded
        {
            get
            {
                return _container._testDataHolder.Instance == null;
            }
        }

        public bool IsEvaluationNeeded
        {
            get
            {
                return _container._testDataHolder.Instance == null;
            }
        }

        public bool IsHashRequired
        {
            get { return IsTracingNeeded || !IsEvaluationNeeded; }
        }
        public bool IsTracingNeeded
        {
            get
            {
                return _container.TraceCollector != null;
            }
        }

        public void Query<T>(string hash, T result, string description)
        {
            if (_container.TraceCollector != null)
            {
                if (_container._testDataHolder.Instance != null)
                {
                    _container.TraceCollector.TestQuery(_channelType, typeof(T), hash, result.DeepClone(), description);
                }
                else
                {
                    _container.TraceCollector.Query(_channelType, typeof(T), hash, result.DeepClone(), description);
                }
                return;
            }
            throw new TectureException("Test data is not presumed to be collected");
        }

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
