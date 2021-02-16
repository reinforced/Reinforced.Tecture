using System;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        public Type RecordType
        {
            get { return typeof(T); }
        }

        /// <inheritdoc />
        public object Payload
        {
            get { return Data; }
        }

        /// <inheritdoc />
        public T Data { get; set; }


    }
}
