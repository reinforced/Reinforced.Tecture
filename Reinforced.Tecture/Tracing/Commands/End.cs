using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands
{
    /// <summary>
    /// Synthetic command that means end of the commands queue
    /// </summary>
    public sealed class End : CommandBase
    {
        public override bool IsExecutable => false;
        public override string Code => " ! ";
        
        internal End()
        {
            Channel = typeof(NoChannel);
        }

        protected override string ToStringActually() => "End";

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new End();
        }
    }
}
