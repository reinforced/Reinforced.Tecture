using System;
using System.Threading;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Services;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture
{
    /// <summary>
    /// Tecture core facade
    /// </summary>
    public interface ITecture : IDisposable
    {
        /// <summary>
        /// Obtains instance of service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        T Do<T>() where T : TectureServiceBase;

        /// <summary>
        /// Obtains data source to query data from
        /// </summary>
        /// <typeparam name="T">Type of data source</typeparam>
        /// <returns>Data source instance</returns>
        Read<T> From<T>() where T : CanQuery;

        /// <summary>
        /// Begins trace collection
        /// </summary>
        /// <param name="lightMode">
        /// Enable light mode. It will be not possible to generate tests or validation
        /// from this trace, but is still stays informative enough</param>
        /// <param name="profiling">
        /// Enable or disable profiling while collecting trace (stopwatches)
        /// </param>
        void BeginTrace(bool lightMode = false, bool profiling = true);

        /// <summary>
        /// Finishes trace collection
        /// </summary>
        /// <returns></returns>
        Trace EndTrace();

        /// <summary>
        /// Runs commands queue
        /// </summary>
        void Save();

        /// <summary>
        /// Runs async commands queue
        /// </summary>
        /// <returns></returns>
        Task SaveAsync(CancellationToken token = default);
    }
}
