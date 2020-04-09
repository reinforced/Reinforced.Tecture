using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Testing
{
    public class TestRunException : TectureException
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public TestRunException(Exception innerException) : base($"Exception while test executing: {innerException.Message}", innerException)
        {
        }
    }
}
