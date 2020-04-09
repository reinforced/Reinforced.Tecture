using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Methodics.SqlStroke
{
    class SqlStrokeException : TectureException
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        public SqlStrokeException(string message) : base($"SQL Strokes Exception: {message}")
        {
        }
    }
}
