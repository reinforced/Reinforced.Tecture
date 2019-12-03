using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Sql
{
    public class SqlExactParametersAssertion : SideEffectAssertion<DirectSqlSideEffect>
    {
        private readonly object[] _parameters;

        public SqlExactParametersAssertion(object[] parameters)
        {
            _parameters = parameters;
        }

        public override string GetMessage(DirectSqlSideEffect effect)
        {
            if (effect == null)
                return $"expected direct SQL with particular parameters, but story unexpectedly ends";

            string name = "direct SQL";
            if (!string.IsNullOrEmpty(effect.Annotation))
                name = $"SQL for '{effect.Annotation}'";
            if (effect.Parameters.Length != _parameters.Length)
                return $"{name} has {effect.Parameters.Length} parameters, but must have {_parameters.Length} ones";

            for (int i = 0; i < effect.Parameters.Length; i++)
            {
                if (!effect.Parameters[i].Equals(_parameters[i]))
                    return $"{name} has parameter @p{i} = '{effect.Parameters[i]}', but it should be '{_parameters[i]}'"; ;
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

        public override bool IsValid(DirectSqlSideEffect effect)
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
