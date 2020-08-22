using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing.Validation
{
    public abstract class ValidationBase
    {
        public void Validate(ITecture tecture)
        {
            var trace = tecture.EndTrace();
            Validate(trace);
        }

        public void Validate(Trace trace)
        {
            var validator = trace.Begins();
            Validate(validator);
        }

        protected abstract void Validate(TraceValidator flow);
    }
}
