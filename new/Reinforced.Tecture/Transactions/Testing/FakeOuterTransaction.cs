using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Transactions.Testing
{
    class FakeOuterTransaction : IOuterTransaction
    {
        public FakeOuterTransaction(OuterTransactionIsolationLevel level)
        {
            IsolationLevel = level;
        }

        public void Dispose() { }

        public void Commit() { }

        public OuterTransactionIsolationLevel IsolationLevel { get; private set; }
    }
}
