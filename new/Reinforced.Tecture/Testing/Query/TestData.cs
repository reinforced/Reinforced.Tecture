using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Testing.Query
{
    public interface TestData
    {
        
    }

    public interface Collecting : TestData
    {
        void Put<T>(string hash, T result,string description = null);

        void Finish();
    }

    public interface Providing : TestData
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
