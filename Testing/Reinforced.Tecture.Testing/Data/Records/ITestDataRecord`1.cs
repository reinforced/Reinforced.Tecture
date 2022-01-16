namespace Reinforced.Tecture.Testing.Data
{
    /// <summary>
    /// Saved response result for query (generic)
    /// </summary>
    public interface ITestDataRecord<out T> : ITestDataRecord
    {
        /// <summary>
        /// Query result
        /// </summary>
        T Data { get; }
    }
}