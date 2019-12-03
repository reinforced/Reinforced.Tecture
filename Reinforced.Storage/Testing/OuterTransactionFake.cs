using Reinforced.Storage.Transactions;

namespace Reinforced.Storage.Testing
{
    internal class OuterTransactionFake : IOuterTransaction
    {
        public OuterTransactionFake(OuterTransactionIsolationLevel level)
        {
            IsolationLevel = level;
        }

        public void Dispose() { }

        public void Commit() { }

        public OuterTransactionIsolationLevel IsolationLevel { get; private set; }
    }
}