using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.SqlStroke.Testing.Checks
{
    public class SqlCommandParametersCheck : CommandCheck<Sql>
    {
        private readonly object[] _expectedParameters;

        internal SqlCommandParametersCheck(object[] expectedParameters)
        {
            _expectedParameters = expectedParameters;
        }

        /// <summary>
        /// Gets error message (called only if command is not valid)
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>Error message</returns>
        protected override string GetMessage(Sql command)
        {
            if (command == null) return $"expected SQL command, but story unexpectedly ends";
            var p = command.Preview;
            if (_expectedParameters.Length != p.Parameters.Length) return $"expected command with {_expectedParameters.Length} but got one with {p.Parameters.Length} parameters";
            StringBuilder sb = new StringBuilder("following parameters of the command are invalid: ");
            sb.AppendLine();
            
            for (int i = 0; i < _expectedParameters.Length; i++)
            {
                if (!object.Equals(_expectedParameters[i], p.Parameters[i]))
                {
                    sb.Append("\t#");
                    sb.Append(i);
                    sb.Append(": expected '");
                    sb.Append(_expectedParameters[i]);
                    sb.Append("', got '");
                    sb.Append(p.Parameters[i]);
                    sb.Append("'");
                    sb.AppendLine();
                }
            }

            return sb.ToString();

        }

        /// <summary>
        /// Gets whether particular command instance is valid or not
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>True if command is valid, false otherwise</returns>
        protected override bool IsActuallyValid(Sql command)
        {
            if (command == null) return false;
            var p = command.Preview;
            if (_expectedParameters.Length != p.Parameters.Length) return false;
            for (int i = 0; i < _expectedParameters.Length; i++)
            {
                if (!object.Equals(_expectedParameters[i], p.Parameters[i])) return false;
            }
            return true;
        }
    }
}
