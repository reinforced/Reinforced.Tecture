using System;

namespace Reinforced.Tecture.Tracing.Promises
{
    /// <summary>
    /// Promised query that demands data for testing purposes
    /// </summary>
    /// <typeparam name="T">Test data</typeparam>
    public interface Catching<T> : Promised<T>
    {
        /// <summary>
        /// Fulfills query demand with exceptional result.
        /// No hash nor clone needed 
        /// </summary>
        /// <param name="error">Exception</param>
        /// <param name="description">Query description</param>
        void Fulfill(Exception error, string description = null);
    }
}