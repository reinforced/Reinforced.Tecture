using System;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Aspects.Orm.Testing.Checks.Add
{
    /// <summary>
    /// Added object check
    /// </summary>
    /// <typeparam name="T">Typed entity</typeparam>
    public class AddPredicateCheck<T> : CommandCheck<Commands.Add.Add>
    {
        private readonly Func<T, bool> _predicate;
        private readonly string _explanation;

        internal AddPredicateCheck(Func<T, bool> predicate, string explanation)
        {
            _predicate = predicate;
            _explanation = explanation;
        }

        /// <inheritdoc />
        protected override string GetMessage(Commands.Add.Add command)
        {
            if (command == null) return $"expected added entity {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected added entity of type {typeof(T).Name} and {_explanation}, but got one of {command.EntityType.Name}";
            }

            if (string.IsNullOrEmpty(_explanation)) return $"added {typeof(T).Name} does not satisfy conditions";
            return $"addition '{_explanation}' does not satisfy condition";
        }

        /// <inheritdoc />
        protected override bool IsActuallyValid(Commands.Add.Add effect)
        {
            if (effect == null) return false;
            if (effect.EntityType != typeof(T)) return false;
            return _predicate((T) effect.Entity);
        }
    }
}
