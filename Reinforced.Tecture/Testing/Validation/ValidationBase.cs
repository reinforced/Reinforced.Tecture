using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing.Validation
{
    /// <summary>
    /// Base class for validation the state of the command queue
    /// </summary>
    public abstract class ValidationBase
    {
        /// <summary>
        /// Validates instance of Tecture root. 
        /// Requires started tract on this instance
        /// </summary>
        /// <param name="tecture">Tecture instance</param>
        public void Validate(ITecture tecture)
        {
            var trace = tecture.EndTrace();
            Validate(trace);
        }

        /// <summary>
        /// Validates collected trace
        /// </summary>
        /// <param name="trace">Trace object to validate</param>
        public void Validate(Trace trace)
        {
            var validator = trace.Begins();
            Validate(validator);
        }

        /// <summary>
        /// Performs exact validation calls
        /// </summary>
        /// <param name="flow">Validation flow</param>
        protected abstract void Validate(TraceValidator flow);
    }
}
