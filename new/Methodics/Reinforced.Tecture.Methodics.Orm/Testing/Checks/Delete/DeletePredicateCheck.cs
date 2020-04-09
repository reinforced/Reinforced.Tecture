using System;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Checks.Delete
{
    public class DeletePredicateCheck<T> : CommandCheck<DeleteCommand>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;
        private readonly Func<T, bool> _predicate;
        private readonly string _explanation;

        public DeletePredicateCheck(Func<T, bool> predicate, string explanation, Memorize<T> mem = null)
        {
            _predicate = predicate;
            _explanation = explanation;
            _memorizedValue = mem;
        }

        protected override string GetMessage(DeleteCommand command)
        {
            if (command == null) return $"expected removed entity {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected removed entity of type {typeof(T).Name} and {_explanation}, but got one of {command.EntityType.Name}";
            }
            return $"expected removed entity {_explanation}, but seems that it does not";
        }

        protected override bool IsActuallyValid(DeleteCommand effect)
        {
            if (effect == null) return false;
            if (effect.EntityType != typeof(T)) return false;
            return _predicate((T) effect.Entity);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((DeleteCommand)seb).Entity);
        }
    }
}
