using System.Data.Entity;
using System.Transactions;
using Reinforced.Storage.Transactions;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    internal class OuterTransactionScopeAdapter : IOuterTransaction
    {
        private readonly TransactionScope _scope;

        public OuterTransactionScopeAdapter(TransactionScope scope)
        {
            _scope = scope;
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public void Commit()
        {
            _scope.Complete();
        }

        public static IOuterTransaction Create(DbContext context, OuterTransactionIsolationLevel isolationLevel)
        {
            var level = ConvertIsolationLevel(isolationLevel);
            var options = new TransactionOptions { IsolationLevel = level, Timeout = TransactionManager.MaximumTimeout };
            if (Transaction.Current != null)
            {
                options.IsolationLevel = Transaction.Current.IsolationLevel;
            }
            var scope = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
            return new OuterTransactionScopeAdapter(scope);
        }

        private static IsolationLevel ConvertIsolationLevel(OuterTransactionIsolationLevel level)
        {
            switch (level)
            {
                case OuterTransactionIsolationLevel.Chaos: return IsolationLevel.Chaos;
                case OuterTransactionIsolationLevel.ReadCommitted: return IsolationLevel.ReadCommitted;
                case OuterTransactionIsolationLevel.ReadUncommitted: return IsolationLevel.ReadUncommitted;
                case OuterTransactionIsolationLevel.RepeatableRead: return IsolationLevel.RepeatableRead;
                case OuterTransactionIsolationLevel.Serializable: return IsolationLevel.Serializable;
                case OuterTransactionIsolationLevel.Snapshot: return IsolationLevel.Snapshot;
                default: return IsolationLevel.Unspecified;
            }
        }
    }
}
