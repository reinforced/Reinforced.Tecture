using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Transactions
{
    public interface ITransactionManager
    {
        IOuterTransaction BeginDbTransaction(OuterTransactionIsolationLevel isolationLevel);
        IOuterTransaction BeginTransactionScopeTransaction(OuterTransactionIsolationLevel isolationLevel);
    }
}
