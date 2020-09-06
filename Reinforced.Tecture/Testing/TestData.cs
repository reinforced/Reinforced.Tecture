using System;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// Test data source supplying test data for each request
    /// </summary>
    public interface ITestDataSource
    {
        /// <summary>
        /// Retrieves instance of saved query result of particular type
        /// </summary>
        /// <typeparam name="T">Expected query result type</typeparam>
        /// <param name="hash">Query hash</param>
        /// <param name="description">Query description</param>
        /// <returns>Instance of test data as response to query</returns>
        T Get<T>(string hash, string description = null);
    }

    /// <summary>
    /// Exception occuring when expected query result is not matching the persisted one
    /// </summary>
    public class TestDataTypeMismatchException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
        public TestDataTypeMismatchException() : base("Test data type mismatch")
        {
        }
    }
}
