namespace Reinforced.Tecture.Aspects.DirectSql
{
    /// <summary>
    /// Join types
    /// </summary>
    public enum Join
    {
        /// <summary>
        /// INNER
        /// </summary>
        Inner,
        /// <summary>
        /// LEFT
        /// </summary>
        Left,
        /// <summary>
        /// RIGHT
        /// </summary>
        Right,
        /// <summary>
        /// OUTER
        /// </summary>
        Outer,
        /// <summary>
        /// CROSS
        /// </summary>
        Cross,
        /// <summary>
        /// DEFAULT (absence of join)
        /// </summary>
        Default
    }
}