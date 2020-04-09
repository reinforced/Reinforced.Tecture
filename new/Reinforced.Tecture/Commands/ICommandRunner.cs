using System.Threading.Tasks;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Marker interface for command runners
    /// </summary>
    public interface ICommandRunner { }

    /// <summary>
    /// Interface of particular command runner
    /// </summary>
    /// <typeparam name="TCommand">Command type</typeparam>
    public interface ICommandRunner<in TCommand> : ICommandRunner
        where TCommand : CommandBase
    {
        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        void Run(TCommand cmd);

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        Task RunAsync(TCommand cmd);
    }
}
