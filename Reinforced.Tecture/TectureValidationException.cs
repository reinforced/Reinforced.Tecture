using System;

namespace Reinforced.Tecture
{
    public class TectureValidationException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        internal TectureValidationException(string message) : base($"Instance validation failed: {message}")
        {
        }
    }
}
