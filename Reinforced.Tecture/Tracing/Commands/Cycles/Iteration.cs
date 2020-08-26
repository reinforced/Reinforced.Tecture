using System.IO;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands.Cycles
{
    [CommandCode("ITR")]
    public class Iteration : CommandBase, ITracingOnly
    {
        internal Iteration() { }

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
    }
}
