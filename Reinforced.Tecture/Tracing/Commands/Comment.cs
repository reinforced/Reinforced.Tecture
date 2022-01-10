using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands
{
    /// <summary>
    /// Command that does nothing except explaining things
    /// </summary>
    public sealed class Comment : CommandBase, ITracingOnly
    {
        public override bool IsExecutable => false;
        
        public override string Code => "// ";
        
        internal Comment()
        {
            Channel = typeof(NoChannel);
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
