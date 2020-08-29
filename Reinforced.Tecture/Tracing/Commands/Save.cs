using System.IO;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands
{
    [CommandCode("SAVE")]
    public sealed class Save : CommandBase, ITracingOnly
    {
        internal Save() { }

        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            tw.Write("====== Saved =====");
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new Save();
        }
    }
}
