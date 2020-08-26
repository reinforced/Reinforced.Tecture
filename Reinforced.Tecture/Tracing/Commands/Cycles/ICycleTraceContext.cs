using System;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    public interface ICycleTraceContext : IDisposable
    {
        void Iteration(string annotation = null);
    }
}