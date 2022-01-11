using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Commands
{
    /// <summary>
    /// Exception that occured during save
    /// </summary>
    public class TectureSaveException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        internal TectureSaveException(Exception innerException) : base($"An error occured during save. See inner exception for details", innerException)
        {
        
        }

        
    }
}
