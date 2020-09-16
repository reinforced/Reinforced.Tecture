using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Tracing
{
    /// <summary>
    /// Query record with result promised in near future
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
    public class PromisedResult<T>
    {
        private readonly QueryRecord _record;

        internal PromisedResult(QueryRecord record)
        {
            _record = record;
        }

        /// <summary>
        /// Provide promised result
        /// </summary>
        /// <param name="result">Query result</param>
        /// <param name="clone">Result clone</param>
        public void Fulfill(T result, T clone)
        {
            _record.SetResult<T>(result, clone);
        }
    }
}
