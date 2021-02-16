namespace Reinforced.Tecture.Aspects.DirectSql.Infrastructure
{
    class DirectSqlException : TectureException
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        internal DirectSqlException(string message) : base($"SQL Strokes Exception: {message}")
        {
        }
    }
}
