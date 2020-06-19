using System;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Assumptions
{
    
    /// <summary>
    /// Assumption that is being used as wildcard command runner
    /// It allows to override runners for particular command while testing
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class AssumptionBase<TCommand> : CommandRunner<TCommand>, IAssumption where TCommand : CommandBase
    {
        protected CommandRunner<TCommand> Runner
        {
            get { return (CommandRunner<TCommand>) OriginalRunner; }
            set { OriginalRunner = value; }
        }

        /// <summary>
        /// Determines whether assumption must be applied for command instead of
        /// honest command run
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns>True when this assumption should be applied for command, false otherwise</returns>
        protected abstract bool ShouldActually(TCommand cmd);

        /// <summary>
        /// Performs assumed actions. Use Runtimes to locate your runtime and proceed
        /// </summary>
        /// <param name="cmd">Command</param>
        protected abstract void AssumeActually(TCommand cmd);

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(TCommand cmd)
        {
            AssumeActually(cmd);
            OriginalRunner = null;
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(TCommand cmd)
        {
            AssumeActually(cmd);
            OriginalRunner = null;
            return Task.FromResult(0);
        }

        public CommandRunner OriginalRunner { get; set; }

        public Type CommandType
        {
            get { return typeof(TCommand); }
        }

        public bool Should(CommandBase cmd)
        {
            if (cmd is TCommand cm) return ShouldActually(cm);
            return false;
        }

        public void Assume(CommandBase cmd)
        {
            if (cmd is TCommand cm) AssumeActually(cm);
        }
    }
}
