using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    /// <summary>
    /// Cycle mark command
    /// </summary>
    public class Cycle : CommandBase, ITracingOnly
    {
        public override bool IsExecutable => false;
        public override string Code => " { ";

        internal Cycle()
        {
            Channel = typeof(NoChannel);
        }

        protected override string ToStringActually() => "Loop begins";

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new Cycle();
        }
    }
}
