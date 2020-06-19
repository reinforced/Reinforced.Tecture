using System.Threading.Tasks;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Marker interface for command runners
    /// </summary>
    public abstract class CommandRunner
    {
        internal abstract void RunCommand(CommandBase command);

        internal abstract Task RunCommandAsync(CommandBase command);
    }

    /// <summary>
    /// Interface of particular command runner
    /// </summary>
    /// <typeparam name="TCommand">Command type</typeparam>
    public abstract class CommandRunner<TCommand> : CommandRunner
        where TCommand : CommandBase
    {
        internal override void RunCommand(CommandBase command)
        {
            if (!(command is TCommand))
            {
                throw new TectureException($"Something wen completely wrong: runner {this.GetType().FullName} cannot run command {command.GetType().FullName}");
            }
            Run((TCommand) command);
        }

        internal override Task RunCommandAsync(CommandBase command)
        {
            if (!(command is TCommand))
            {
                throw new TectureException($"Something wen completely wrong: runner {this.GetType().FullName} cannot run command {command.GetType().FullName}");
            }
            return RunAsync((TCommand)command);
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected abstract void Run(TCommand cmd);

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected abstract Task RunAsync(TCommand cmd);
    }
}
