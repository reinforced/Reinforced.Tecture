using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    class FakeCycleTraceContext : ICycleTraceContext
    {
        public void Dispose() { }

        public void Iteration(string annotation = null) { }
    }
}
