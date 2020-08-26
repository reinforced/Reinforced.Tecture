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
    }
}
