using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Tracing.Promises
{
    /// <summary>
    /// Promised query that already contains test data
    /// </summary>
    /// <typeparam name="T">Type of test data</typeparam>
    public interface Containing<T> : Promised<T>
    {
        T Get(string hash, string description = null);
    }

    internal class Contains<T> : Containing<T>
    {
        private readonly ITestDataSource _source;
        private readonly PromisedQuery<T>? _promised;

        public Contains(ITestDataSource source, TraceCollector traceCollector, Type channelType)
        {
            _source = source;
            if (traceCollector != null)
            {
                _promised = traceCollector.PromiseTestQuery<T>(channelType);
            }
        }

        public T Get(string hash, string description = null)
        {
            var result = _source.Get<T>(hash, description);
            _promised?.Fulfill(result, DeepCloner.DeepClone(result), hash, description);
            return result;
        }
    }
}
