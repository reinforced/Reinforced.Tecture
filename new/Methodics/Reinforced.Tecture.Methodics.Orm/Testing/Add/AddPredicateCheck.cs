using System;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Add
{
    public class AddPredicateCheck<T> : CommandCheck<AddCommand>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;
        private readonly Func<T, bool> _predicate;
        private readonly string _explanation;

        public AddPredicateCheck(Func<T, bool> predicate, string explanation, Memorize<T> mem = null)
        {
            _predicate = predicate;
            _explanation = explanation;
            _memorizedValue = mem;
        }

        protected override string GetMessage(AddCommand command)
        {
            if (command == null) return $"expected added entity {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected added entity of type {typeof(T).Name} and {_explanation}, but got one of {command.EntityType.Name}";
            }
            return $"expected added entity {_explanation}, but seems that it does not";
        }

        protected override bool IsActuallyValid(AddCommand effect)
        {
            if (effect == null) return false;
            if (effect.EntityType != typeof(T)) return false;
            return _predicate((T) effect.Entity);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((AddCommand)seb).Entity);
        }
    }
}
