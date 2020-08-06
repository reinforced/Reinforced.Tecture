using System;
using System.Collections.Generic;
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
