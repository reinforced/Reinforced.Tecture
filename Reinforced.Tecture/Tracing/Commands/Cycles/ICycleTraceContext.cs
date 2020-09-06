using System;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    /// <summary>
    /// Cycle description context
    /// </summary>
    public interface ICycleTraceContext : IDisposable
    {
        /// <summary>
        /// Enqueue cycle iteration delimiter
        /// </summary>
        /// <param name="annotation">Iteration annotation</param>
        void Iteration(string annotation = null);
    }
}