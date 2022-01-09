using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    /// <summary>
    /// Iteration mark command
    /// </summary>
    [CommandCode(" ^ ")]
    public class Iteration : CommandBase, ITracingOnly
    {
        internal Iteration()
        {
            Channel = typeof(NoChannel);
        }

        /// <inheritdoc />
        public override void Describe(TextWriter tw)
        {
            if (string.IsNullOrEmpty(Annotation))
            {
                tw.Write("--- Cycle iteration ---");
            }
            else
            {
                tw.Write($"--- {Annotation} ---");
            }
        }

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
