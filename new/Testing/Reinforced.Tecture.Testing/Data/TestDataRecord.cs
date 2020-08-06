using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Testing.Data
{
    public interface ITestDataRecord
    {
        string Description { get; set; }
        string Hash { get; set; }
    }

    public interface ITestDataRecord<out T> : ITestDataRecord
    {
        T Data { get; }
    }

    public class TestDataRecord<T> : ITestDataRecord<T>
    {
        public string Description { get; set; }
        public string Hash { get; set; }
        public T Data { get; set; }
    }
}
