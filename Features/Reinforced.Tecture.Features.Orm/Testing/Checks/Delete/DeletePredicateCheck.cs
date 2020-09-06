using System;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Delete
{
    public class DeletePredicateCheck<T> : CommandCheck<Commands.Delete.Delete>
    {
        private readonly Func<T, bool> _predicate;
        private readonly string _explanation;

        internal DeletePredicateCheck(Func<T, bool> predicate, string explanation)
        {
            _predicate = predicate;
            _explanation = explanation;
        }

        /// <inheritdoc />
        protected override string GetMessage(Commands.Delete.Delete command)
        {
            if (command == null) return $"expected removed entity {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected removed entity of type {typeof(T).Name} and {_explanation}, but got one of {command.EntityType.Name}";
            }
            if (string.IsNullOrEmpty(_explanation)) return $"deleted {typeof(T).Name} does not meet conditions";
            return $"removal '{_explanation}' does not satisfy conditions";
        }

        protected override bool IsActuallyValid(Commands.Delete.Delete effect)
        {
            if (effect == null) return false;
            if (effect.EntityType != typeof(T)) return false;
            return _predicate((T) effect.Entity);
        }
    }
}
