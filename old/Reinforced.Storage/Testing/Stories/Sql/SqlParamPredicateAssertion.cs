using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Sql
{

    public class SqlParamPredicateAssertion : CommandCheck<DirectSqlSideEffect>
    {
        private readonly bool _original;
        private readonly SqlParameterValidator[] _validators;

        public SqlParamPredicateAssertion(SqlParameterValidator[] validators, bool original)
        {
            _validators = validators;
            _original = original;
        }

        public override string GetMessage(DirectSqlSideEffect command)
        {
            if (command == null)
                return $"expected direct SQL with particular parameters, but story unexpectedly ends"; ;

            string name = "direct SQL";
            if (!string.IsNullOrEmpty(command.Annotation))
                name = $"SQL for '{command.Annotation}'";

            var pars = _original ? command.OriginalParameters : command.Parameters;

            if (pars.Length != _validators.Length)
                return $"{name} has {pars.Length} parameters, but must have {_validators.Length} ones";

            for (int i = 0; i < pars.Length; i++)
            {
                
                var validator = _validators[i];
                var error =
                    $"parameter @p{i} must be {validator.Description}, but actually is '{pars[i]}'";

                if (validator.Any) continue;
                if (validator.ShouldBeNull)
                {
                    if (pars[i] != null) return error;
                }

                if (validator.Type != null)
                {
                    if (pars[i] == null) return error;
                    if (pars[i].GetType() != validator.Type) return error;
                }

                if (validator.ExactValue != null)
                {
                    if (!Compare(pars[i], validator.ExactValue)) return error;
                }

                if (validator.Validator != null)
                {
                    if (!(bool)(validator.Validator.DynamicInvoke(pars[i]))) return error;
                }
            }

            return null; //never
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
            return GetMessage(effect) != null;
        }
    }
}
