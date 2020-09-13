using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Runtimes.EFCore.Transactions
{
    /// <summary>
    /// EntityFramework Core transaction
    /// </summary>
    public class EfCoreTransaction : ChannelTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public EfCoreTransaction(Type channel, IDbContextTransaction transaction) : base(channel)
        {
            _transaction = transaction;
        }

        public override void Commit()
        {
            _transaction.Commit();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
