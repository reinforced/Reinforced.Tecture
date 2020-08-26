using System.IO;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands
{
    [CommandCode("END")]
    public sealed class End : CommandBase, ITracingOnly
    {
        internal End() { }


        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            tw.Write("======  END  =====");
        }
    }
}
