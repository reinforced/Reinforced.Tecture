using System.Threading.Tasks;

namespace Reinforced.Storage.SideEffects
{
    /// <summary>
    /// Interface of particular side effect runner
    /// </summary>
    /// <typeparam name="TSideEffect">Side effect type</typeparam>
    public interface ISideEffectRunner<in TSideEffect> where TSideEffect : SideEffectBase
    {
        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        void Run(TSideEffect effect);

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        Task RunAsync(TSideEffect effect);
    }
}
