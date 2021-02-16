using System.IO;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    /// <summary>
    /// Synthetic command that means end of logical cycle 
    /// </summary>
    [CommandCode(" . ")]
    public class EndCycle : CommandBase, ITracingOnly
    {
        internal EndCycle() { }

        /// <summary>
        /// Total number of commands that was produced within cycle
        /// </summary>
        public int TotalCommands { get; internal set; }

        /// <summary>
        /// Total number of iterations happened
        /// </summary>
        public int IterationsCount { get; internal set; }

        /// <inheritdoc />
        public override void Describe(TextWriter tw)
        {
            if (string.IsNullOrEmpty(Annotation))
            {
                tw.Write("Cycle ends in ");
                tw.Write(IterationsCount);
                tw.Write("iterations");
            }
            else
            {
                tw.Write(Annotation);
                tw.Write(" ends in ");
                tw.Write(IterationsCount);
                tw.Write(" iterations and ");
                tw.Write(TotalCommands);
                tw.Write(" commands");
            }
        }

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
