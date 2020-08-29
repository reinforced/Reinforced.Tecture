using System;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Update
{
    public class UpdatePredicateCheck<T> : CommandCheck<Commands.Update.Update>
    {
        private readonly Func<T, bool> _predicate;
        private readonly string _explanation;

        public UpdatePredicateCheck(Func<T, bool> predicate, string explanation)
        {
            _predicate = predicate;
            _explanation = explanation;
        }

        protected override string GetMessage(Commands.Update.Update command)
        {
            if (command == null) return $"expected updated entity {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected updated entity of type {typeof(T).Name} and {_explanation}, but got one of {command.EntityType.Name}";
            }
            if (string.IsNullOrEmpty(_explanation)) return $"updated {typeof(T).Name} does not meet conditions";
            return $"update '{_explanation}' does not satisfy conditions";
        }

        protected override bool IsActuallyValid(Commands.Update.Update effect)
        {
            if (effect == null) return false;
            if (effect.EntityType != typeof(T)) return false;
            return _predicate((T) effect.Entity);
        }
    }
}
