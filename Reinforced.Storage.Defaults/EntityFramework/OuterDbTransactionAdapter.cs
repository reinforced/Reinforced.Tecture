using System.Data;
using System.Data.Entity;
using Reinforced.Storage.Transactions;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    internal class OuterDbTransactionAdapter : IOuterTransaction
    {
        private readonly IDbTransaction _dbTransaction;

        public OuterDbTransactionAdapter(IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
        }

        public OuterDbTransactionAdapter()
        {
            _dbTransaction = null;
        }

        public void Dispose()
        {
            if (_dbTransaction != null) _dbTransaction.Dispose();
        }

        public void Commit()
        {
            if (_dbTransaction != null) _dbTransaction.Commit();
        }

        public static IOuterTransaction Create(DbContext context, OuterTransactionIsolationLevel isolationLevel)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
            {
                context.Database.Connection.Open();
            }
            if (context.Database.CurrentTransaction != null)
            {
                return new OuterDbTransactionAdapter();
            }

            var transaction = context.Database.Connection.BeginTransaction(ConvertIsolationLevel(isolationLevel));
            context.Database.UseTransaction(transaction);
            return new OuterDbTransactionAdapter(transaction);
        }

        private static System.Data.IsolationLevel ConvertIsolationLevel(OuterTransactionIsolationLevel level)
        {
            switch (level)
            {
                case OuterTransactionIsolationLevel.Chaos: return System.Data.IsolationLevel.Chaos;
                case OuterTransactionIsolationLevel.ReadCommitted: return System.Data.IsolationLevel.ReadCommitted;
                case OuterTransactionIsolationLevel.ReadUncommitted: return System.Data.IsolationLevel.ReadUncommitted;
                case OuterTransactionIsolationLevel.RepeatableRead: return System.Data.IsolationLevel.RepeatableRead;
                case OuterTransactionIsolationLevel.Serializable: return System.Data.IsolationLevel.Serializable;
                case OuterTransactionIsolationLevel.Snapshot: return System.Data.IsolationLevel.Snapshot;
                default: return System.Data.IsolationLevel.Unspecified;
            }
        }
    }
}
