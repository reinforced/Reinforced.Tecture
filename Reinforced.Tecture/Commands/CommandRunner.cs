using System.Threading;
using System.Threading.Tasks;

namespace Reinforced.Tecture.Commands
{

    /// <summary>
    /// Marker interface for command runners
    /// </summary>
    public abstract class CommandRunner
    {
        internal abstract void RunInternal(CommandBase command);

        internal abstract Task RunInternalAsync(CommandBase command,CancellationToken token = default);
    }

    /// <summary>
    /// Interface of particular command runner
    /// </summary>
    /// <typeparam name="TCommand">Command type</typeparam>
    public abstract class CommandRunner<TCommand> : CommandRunner
        where TCommand : CommandBase
    {
        internal override void RunInternal(CommandBase command)
        {
            if (!(command is TCommand))
            {
                throw new TectureException($"Something wen completely wrong: runner {this.GetType().FullName} cannot run command {command.GetType().FullName}");
            }
            Run((TCommand) command);
        }

        internal override Task RunInternalAsync(CommandBase command,CancellationToken token = default)
        {
            if (!(command is TCommand))
            {
                throw new TectureException($"Something wen completely wrong: runner {this.GetType().FullName} cannot run command {command.GetType().FullName}");
            }

            return RunAsync((TCommand)command, token);
        }

        /// <summary>
        /// Runs command
        /// </summary>
        /// <param name="cmd">Command</param>
        protected abstract void Run(TCommand cmd);

        /// <summary>
        /// Runs command asynchronously
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Async</returns>
        protected abstract Task RunAsync(TCommand cmd,CancellationToken token = default);
    }
}
