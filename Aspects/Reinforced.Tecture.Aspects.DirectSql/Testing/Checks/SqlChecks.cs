namespace Reinforced.Tecture.Aspects.DirectSql.Testing.Checks
{
    /// <summary>
    /// Checks for SQL commands
    /// </summary>
    public static class SqlChecks
    {
        /// <summary>
        /// Validates SQL command preview text (without schema)
        /// </summary>
        /// <param name="command">SQL query text</param>
        /// <returns>Check instance</returns>
        public static SqlCommandTextCheck SqlCommand(string command) => new SqlCommandTextCheck(command);

        /// <summary>
        /// Validates SQL command parameters
        /// </summary>
        /// <param name="p">Command parameters set</param>
        /// <returns>Check instance</returns>
        public static SqlCommandParametersCheck SqlParameters(params object[] p) => new SqlCommandParametersCheck(p);
    }

    
}
