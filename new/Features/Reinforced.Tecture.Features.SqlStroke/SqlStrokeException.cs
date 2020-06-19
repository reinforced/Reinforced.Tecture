namespace Reinforced.Tecture.Features.SqlStroke
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
