using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Cloning;

namespace Reinforced.Tecture.Tracing.Promises
{
    /// <summary>
    /// Promised query that demands data for testing purposes
    /// </summary>
    /// <typeparam name="T">Test data</typeparam>
    public interface Demanding<T> : Promised<T>
    {
        /// <summary>
        /// Fulfills query demand with exact result
        /// </summary>
        /// <param name="result">Results instance</param>
        /// <param name="clone">Result instance clone</param>
        /// <param name="hash">Result hash</param>
        /// <param name="description">Result description</param>
        void Fullfill(T result, T clone, string hash, string description);

        /// <summary>
        /// Fulfills query demand with exact result.
        /// Result will be deeply cloned automatically. 
        /// </summary>
        /// <param name="result">Results instance</param>
        /// <param name="hash">Result hash</param>
        /// <param name="description">Result description</param>
        void Fullfill(T result, string hash, string description);
    }

    class Demands<T> : Demanding<T>
    {
        private PromisedQuery<T> _promised;
        public Demands(TraceCollector traceCollector, Type channelType)
        {
            _promised = traceCollector.PromiseQuery<T>(channelType);
        }

        public void Fullfill(T result, T clone, string hash, string description)
        {
            _promised.Fulfill(result, clone, hash, description);
        }

        public void Fullfill(T result, string hash, string description)
        {
            _promised.Fulfill(result, result.DeepClone(), hash, description);
        }
    }
}
