using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    /// <summary>
    /// Iteration mark command
    /// </summary>
    public class Iteration : CommandBase, ITracingOnly
    {
        public override bool IsExecutable => false;
        
        public override string Code => " + ";
        internal Iteration()
        {
            Channel = typeof(NoChannel);
        }

        protected override string ToStringActually() => "Loop iteration";

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new Iteration();
        }
    }
}
