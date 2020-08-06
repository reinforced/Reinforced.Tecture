using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Testing.Data
{
    public class TestDataEndsException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class.</summary>
        public TestDataEndsException(int counter) : base($"Test data unexpectidly ends at position {counter}")
        {
        }
    }

    public class TestDataTypeMismatchException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class.</summary>
        public TestDataTypeMismatchException(int counter, string description, Type expected, Type actual) : base(
            string.IsNullOrEmpty(description)
                ? $"Test data type mismatch at {counter} position: '{expected}' expected but '{actual}' got "
                : $"Test data type mismatch at {counter} position within {description} query: '{expected}' expected but '{actual}' got "
                )
        {
        }
    }

    public class TestHashMismatchException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class.</summary>
        public TestHashMismatchException(int counter, string description, string expected, string actual)
            : base(
                string.IsNullOrEmpty(description)
                    ? $"Query hash mismatch in test data at {counter} position: '{expected}' expected but '{actual}' got "
                    : $"Query hash mismatch in test data at {counter} position within {description} query: '{expected}' expected but '{actual}' got "
            )
        {
        }
    }
}
