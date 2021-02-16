using System;

namespace Reinforced.Tecture.Cloning
{
    /// <summary>
    /// Exception that happens during deep clone
    /// </summary>
    public class TectureCloningException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public TectureCloningException(string message) : base("Exception when cloning object: " + message)
        {
        }
    }
}
