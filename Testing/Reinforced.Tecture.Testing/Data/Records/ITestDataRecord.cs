using System;

namespace Reinforced.Tecture.Testing.Data
{
    /// <summary>
    /// Saved response result for query
    /// </summary>
    public interface ITestDataRecord
    {
        /// <summary>
        /// Query description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Query hash
        /// </summary>
        string Hash { get; set; }

        /// <summary>
        /// Query result type
        /// </summary>
        Type RecordType { get; }

        /// <summary>
        /// Query result
        /// </summary>
        object Payload { get; }
    }
}