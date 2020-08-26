using System.IO;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands
{
    /// <summary>
    /// Command that does nothing except explainig things
    /// </summary>
    [CommandCode("COMMENT")]
    public sealed class Comment : CommandBase, ITracingOnly
    {
        internal Comment() { }

        /// <summary>
        /// Describes actions that are being performed within command
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            tw.Write(Annotation);
            if (Debug != null) tw.Write($" ({Debug.Location})");
        }
    }
}
