using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Integrate;

namespace Reinforced.Tecture.Testing.Assumptions
{
    public abstract class AssumptionBase : IAssumption
    {
        /// <summary>
        /// Locates runtime
        /// </summary>
        public IRuntimeLocator Locate { get; internal set; }

        /// <summary>
        /// Original command runner (if any)
        /// </summary>
        internal ICommandRunner OriginalRunner;

        public abstract Type CommandType { get; }
        public abstract bool Should(CommandBase cmd);
        public abstract void Assume(CommandBase cmd);
    }

    /// <summary>
    /// Assumption that is being used as wildcard command runner
    /// It allows to override runners for particular command while testing
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class AssumptionBase<TCommand> : AssumptionBase, ICommandRunner<TCommand> where TCommand : CommandBase
    {
        protected ICommandRunner<TCommand> Runner
        {
            get { return (ICommandRunner<TCommand>) base.OriginalRunner; }
            set { base.OriginalRunner = value; }
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
        public void Run(TCommand cmd)
        {
            AssumeActually(cmd);
            OriginalRunner = null;
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(TCommand cmd)
        {
            AssumeActually(cmd);
            OriginalRunner = null;
            return Task.FromResult(0);
        }

        public override Type CommandType
        {
            get { return typeof(TCommand); }
        }

        public override bool Should(CommandBase cmd)
        {
            if (cmd is TCommand cm) return ShouldActually(cm);
            return false;
        }

        public override void Assume(CommandBase cmd)
        {
            if (cmd is TCommand cm) AssumeActually(cm);
        }
    }
}
