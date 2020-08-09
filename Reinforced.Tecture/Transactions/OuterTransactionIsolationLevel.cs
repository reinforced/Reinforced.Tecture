using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Transactions
{
    public enum OuterTransactionIsolationLevel
    {
        Chaos,
        ReadCommitted,
        ReadUncommitted,
        RepeatableRead,
        Serializable,
        Snapshot,
        Unspecified
    }
}
