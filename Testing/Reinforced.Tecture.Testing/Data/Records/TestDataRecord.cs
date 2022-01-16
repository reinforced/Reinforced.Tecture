using System;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Tecture.Testing.Data
{
    /// <summary>
    /// Saved response result for query
    /// </summary>
    public class TestDataRecord<T> : ITestDataRecord<T>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public TestDataRecord(T data)
        {
            Data = data;
        }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public string Hash { get; set; }

        /// <inheritdoc />
        public Type RecordType => typeof(T);

        /// <inheritdoc />
        public object Payload => Data;

        /// <inheritdoc />
        public T Data { get; set; }


    }
}
