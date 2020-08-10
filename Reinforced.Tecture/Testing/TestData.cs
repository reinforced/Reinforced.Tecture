using System;

namespace Reinforced.Tecture.Testing
{
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
