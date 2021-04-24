using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands
{
    /// <summary>
    /// Command that does nothing except explainig things
    /// </summary>
    [CommandCode("COMMENT")]
    public sealed class Comment : CommandBase, ITracingOnly
    {
        internal Comment()
        {
            Channel = typeof(Channelless);
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new Comment();
        }
    }
}
