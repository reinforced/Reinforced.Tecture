using System;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Update
{
    public class UpdatePredicateCheck<T> : CommandCheck<Command.Update.Update>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;
        private readonly Func<T, bool> _predicate;
        private readonly string _explanation;

        public UpdatePredicateCheck(Func<T, bool> predicate, string explanation, Memorize<T> mem = null)
        {
            _predicate = predicate;
            _explanation = explanation;
            _memorizedValue = mem;
        }

        protected override string GetMessage(Command.Update.Update command)
        {
            if (command == null) return $"expected updated entity {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected updated entity of type {typeof(T).Name} and {_explanation}, but got one of {command.EntityType.Name}";
            }
            return $"expected updated entity {_explanation}, but seems that it does not";
        }

        protected override bool IsActuallyValid(Command.Update.Update effect)
        {
            if (effect == null) return false;
            if (effect.EntityType != typeof(T)) return false;
            return _predicate((T) effect.Entity);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((Command.Update.Update)seb).Entity);
        }
    }
}
