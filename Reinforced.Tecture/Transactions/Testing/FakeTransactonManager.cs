using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Transactions.Testing
{
    class FakeTransactonManager : ITransactionManager
    {
        public virtual IOuterTransaction BeginDbTransaction(OuterTransactionIsolationLevel isolationLevel)
        {
            return new FakeOuterTransaction(isolationLevel);
        }

        public virtual IOuterTransaction BeginTransactionScopeTransaction(OuterTransactionIsolationLevel isolationLevel)
        {
            return new FakeOuterTransaction(isolationLevel);
        }
    }
}
