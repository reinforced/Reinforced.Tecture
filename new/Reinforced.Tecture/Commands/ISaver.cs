using System.Threading.Tasks;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Side effect saver that runs some actions after side effects queue ran out
    /// </summary>
    public interface ISaver
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