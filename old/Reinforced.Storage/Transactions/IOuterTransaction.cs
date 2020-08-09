using System;

namespace Reinforced.Storage.Transactions
{
    /// <summary>
    /// Outer transaction wrapper to integrate DbTransaction and TransactionScope together
    /// </summary>
    public interface IOuterTransaction : IDisposable
    {
        /// <summary>
        /// Commit transaction
        /// </summary>
        void Commit();
    }
}
