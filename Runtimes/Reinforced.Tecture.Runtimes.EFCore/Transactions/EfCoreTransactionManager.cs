using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Transactions;

namespace Reinforced.Tecture.Runtimes.EFCore.Transactions
{
    /// <summary>
    /// Inherit this class and override particular methods in order to return correct EfCoreTransaction instance
    /// </summary>
    public class EfCoreTransactionManager : ChannelTransactionsManager
    {
        private LazyDisposable<DbContext> _context;

        public EfCoreTransactionManager(Type channel, LazyDisposable<DbContext> context) : base(channel)
        {
            _context = context;
        }
    }
}
