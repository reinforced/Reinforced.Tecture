using System.Text;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Aspects.DirectSql.Testing.Checks
{
    /// <summary>
    /// SQL command parameters check
    /// </summary>
    public class SqlCommandParametersCheck : CommandCheck<Sql>
    {
        private readonly object[] _expectedParameters;

        internal SqlCommandParametersCheck(object[] expectedParameters)
        {
            _expectedParameters = expectedParameters;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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
