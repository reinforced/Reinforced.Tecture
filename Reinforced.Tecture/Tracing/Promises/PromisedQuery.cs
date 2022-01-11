using System;
using System.Diagnostics;
using Reinforced.Tecture.Cloning;

namespace Reinforced.Tecture.Tracing.Promises
{
    /// <summary>
    /// Promised query. Can be cast-checked to Demands or Contains or neither
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Promised<T> { }

    /// <summary>
    /// Query record with result promised in near future
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
    internal struct PromisedQuery<T>
    {
        private readonly QueryRecord _record;
        private readonly Stopwatch _sw;

        internal PromisedQuery(QueryRecord record)
        {
            _record = record;
            _sw = Stopwatch.StartNew();
        }

        /// <summary>
        /// Provide promised result
        /// </summary>
        /// <param name="result">Query result</param>
        /// <param name="clone">Result clone</param>
        /// <param name="hash">Query hash</param>
        /// <param name="description">Query description</param>
        public void Fulfill(T result, T clone, string hash, string description)
        {
            _sw.Stop();
            _record.SetResult(result, clone, hash, description, _sw.Elapsed);
        }
        
        /// <summary>
        /// Provide promised result
        /// </summary>
        /// <param name="result">Query result</param>
        /// <param name="clone">Result clone</param>
        /// <param name="hash">Query hash</param>
        /// <param name="description">Query description</param>
        public void LightFulfill(string description)
        {
            _sw.Stop();
            _record.SetLightResult<T>(description, _sw.Elapsed);
        }
        
        /// <summary>
        /// Fulfills promise with error
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="description"></param>
        public void FulfillError(Exception ex, string description)
        {
            _sw.Stop();
            _record.SetException<T>(ex, description, _sw.Elapsed);
        }
    }
}
