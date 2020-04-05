using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Testing.Validation
{
    public class AssertionException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        public AssertionException(string message) : base("Storage assertion failed: " + message)
        {
        }
    }
}
