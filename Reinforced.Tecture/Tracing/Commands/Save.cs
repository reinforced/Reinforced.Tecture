using System.IO;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Tracing.Commands
{
    /// <summary>
    /// Synthetic command that means saving happening at the particular point
    /// </summary>
    public sealed class Save : CommandBase
    {
        public override bool IsExecutable => false;
        public override string Code => " <-";
        
        internal Save()
        {
            Channel = typeof(NoChannel);
        }

        protected override string ToStringActually() => "Save";

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
