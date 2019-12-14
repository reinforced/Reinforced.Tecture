using System.Threading.Tasks;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Interface of particular command runner
    /// </summary>
    /// <typeparam name="TCommand">Command type</typeparam>
    public interface ICommandRunner<in TCommand> where TCommand : CommandBase
    {
        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        void Run(TCommand effect);

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        Task RunAsync(TCommand effect);
    }
}
