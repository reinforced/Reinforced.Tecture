using System.IO;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    [CommandCode("CYC")]
    public class Cycle : CommandBase, ITracingOnly
    {
        internal Cycle() { }

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
    }
}
