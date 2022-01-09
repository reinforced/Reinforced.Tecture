using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    /// <summary>
    /// Cycle mark command
    /// </summary>
    [CommandCode(" * ")]
    public class Cycle : CommandBase, ITracingOnly
    {
        internal Cycle()
        {
            Channel = typeof(NoChannel);
        }

        /// <inheritdoc />
        public override void Describe(TextWriter tw)
        {
            if (string.IsNullOrEmpty(Annotation)) tw.Write("Do following in cycle:");
            else
            {
                tw.Write("In cycle ");
                tw.Write(Annotation);
                tw.Write(":");
            }
        }

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
