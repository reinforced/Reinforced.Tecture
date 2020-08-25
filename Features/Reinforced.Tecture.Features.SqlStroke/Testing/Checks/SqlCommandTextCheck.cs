using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.SqlStroke.Testing.Checks
{
    public class SqlCommandTextCheck : CommandCheck<Sql>
    {
        private readonly string _commandText;
        internal SqlCommandTextCheck(string commandText)
        {
            _commandText = commandText;
        }
        /// <summary>
        /// Gets error message (called only if command is not valid)
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>Error message</returns>
        protected override string GetMessage(Sql command)
        {
            if (command == null) return $"expected SQL command, but story unexpectedly ends";
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets whether particular command instance is valid or not
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>True if command is valid, false otherwise</returns>
        protected override bool IsActuallyValid(Sql command)
        {
            var p = command.Preview;
            return p.Query == _commandText;
        }
    }
}
