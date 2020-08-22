using System;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// Test data source supplying test data for each request
    /// </summary>
    public interface ITestDataSource
    {
        T Get<T>(string hash);
    }

    public class TestDataTypeMismatchException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
        public TestDataTypeMismatchException() : base("Test data type mismatch")
        {
        }
    }
}
