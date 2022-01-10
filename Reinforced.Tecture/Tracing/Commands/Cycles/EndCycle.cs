using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    /// <summary>
    /// Synthetic command that means end of logical cycle 
    /// </summary>
    public class EndCycle : CommandBase, ITracingOnly
    {
        public override bool IsExecutable => false;

        public override string Code => " } ";

        internal EndCycle()
        {
            Channel = typeof(NoChannel);
        }

        /// <summary>
        /// Total number of commands that was produced within cycle
        /// </summary>
        public int TotalCommands { get; internal set; }

        /// <summary>
        /// Total number of iterations happened
        /// </summary>
        public int IterationsCount { get; internal set; }

        protected override string ToStringActually()
            => $"Loop ends in {IterationsCount} iterations producing {TotalCommands} commands";

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new EndCycle() { TotalCommands = TotalCommands, IterationsCount = IterationsCount };
        }
    }
}
