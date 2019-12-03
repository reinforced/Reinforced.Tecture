using System.Threading.Tasks;

namespace Reinforced.Storage.SideEffects
{
    /// <summary>
    /// Side effect saver that runs some actions after side effects queue ran out
    /// </summary>
    public interface ISideEffectSaver
    {
        /// <summary>
        /// Saves data
        /// </summary>
        void Save();

        /// <summary>
        /// Saves data asynchronously
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}