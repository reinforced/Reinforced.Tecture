namespace Reinforced.Storage.Transactions
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
