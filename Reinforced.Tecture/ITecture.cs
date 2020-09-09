using System;
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
    public interface ITectureNoSave : IDisposable
    {
        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
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
        void BeginTrace();

        /// <summary>
        /// Finishes trace collection
        /// </summary>
        /// <returns></returns>
        Trace EndTrace();
    }

    /// <summary>
    /// Tecture core facade with saving capabilities
    /// </summary>
    public interface ITecture : ITectureNoSave
    {
        /// <summary>
        /// Runs commands queue
        /// </summary>
        void Save(OuterTransactionMode transaction = OuterTransactionMode.None,
            OuterTransactionIsolationLevel level = OuterTransactionIsolationLevel.Chaos);

        /// <summary>
        /// Runs async commands queue
        /// </summary>
        /// <returns></returns>
        Task SaveAsync(OuterTransactionMode transaction = OuterTransactionMode.None,
            OuterTransactionIsolationLevel level = OuterTransactionIsolationLevel.Chaos);
    }

}
