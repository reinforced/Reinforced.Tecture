using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Services;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture
{
    /// <summary>
    /// Tecture facade
    /// </summary>
    public interface ITecture : IDisposable
    {
        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        T Do<T>() where T : TectureService, INoContext;

        /// <summary>
        /// Obtains context service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Context service <typeparamref name="T"/></returns>
        LetBuilder<T> Let<T>() where T : TectureService, IWithContext;

        /// <summary>
        /// Obtains data source to query data from
        /// </summary>
        /// <typeparam name="T">Type of data source</typeparam>
        /// <returns>Data source instance</returns>
        T From<T>() where T : class, ISource;

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
