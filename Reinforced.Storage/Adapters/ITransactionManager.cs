using Reinforced.Storage.Transactions;

namespace Reinforced.Storage.Adapters
{
    public interface ITransactionManager
    {
        IOuterTransaction BeginDbTransaction(OuterTransactionIsolationLevel isolationLevel);
        IOuterTransaction BeginTransactionScopeTransaction(OuterTransactionIsolationLevel isolationLevel);
    }
}