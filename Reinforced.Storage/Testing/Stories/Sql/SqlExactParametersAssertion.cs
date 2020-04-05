using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Sql
{
    public class SqlExactParametersAssertion : CommandCheck<DirectSqlSideEffect>
    {
        private readonly object[] _parameters;

        public SqlExactParametersAssertion(object[] parameters)
        {
            _parameters = parameters;
        }

        public override string GetMessage(DirectSqlSideEffect command)
        {
            if (command == null)
                return $"expected direct SQL with particular parameters, but story unexpectedly ends";

            string name = "direct SQL";
            if (!string.IsNullOrEmpty(command.Annotation))
                name = $"SQL for '{command.Annotation}'";
            if (command.Parameters.Length != _parameters.Length)
                return $"{name} has {command.Parameters.Length} parameters, but must have {_parameters.Length} ones";

            for (int i = 0; i < command.Parameters.Length; i++)
            {
                if (!command.Parameters[i].Equals(_parameters[i]))
                    return $"{name} has parameter @p{i} = '{command.Parameters[i]}', but it should be '{_parameters[i]}'"; ;
            }

            return string.Empty; //never
        }

        private bool Compare(object value1, object value2)
        {
            if (value2 == null && value1 == null) return true;
            if (value1 == null && value2 != null) return false;
            if (value2 == null && value1 != null) return false;
            return value1.Equals(value2);
        }

        public override bool IsActuallyValid(DirectSqlSideEffect effect)
        {
            if (effect == null) return false;
            if (effect.Parameters.Length != _parameters.Length) return false;
            for (int i = 0; i < effect.Parameters.Length; i++)
            {
                if (!Compare(effect.Parameters[i],_parameters[i])) return false;
            }

            return true;
        }
    }
}
