using System;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Tecture.Testing.Data
{
    public interface ITestDataRecord
    {
        string Description { get; set; }
        string Hash { get; set; }
        Type RecordType { get; }
        object Payload { get; }
    }

    public interface ITestDataRecord<out T> : ITestDataRecord
    {
        T Data { get; }
    }

    public class TestDataRecord<T> : ITestDataRecord<T>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public TestDataRecord(T data)
        {
            Data = data;
        }

        public string Description { get; set; }
        public string Hash { get; set; }
        public Type RecordType
        {
            get { return typeof(T); }
        }

        public object Payload
        {
            get { return Data; }
        }

        public T Data { get; set; }


    }
}
