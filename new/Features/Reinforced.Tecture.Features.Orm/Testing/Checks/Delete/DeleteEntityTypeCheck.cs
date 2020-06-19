using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Delete
{
    public class DeleteEntityTypeCheck<T> : CommandCheck<Command.Delete.Delete>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public DeleteEntityTypeCheck(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }
        protected override string GetMessage(Command.Delete.Delete command)
        {
            if (command == null) return $"expected removed entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected removed entity of type {typeof(T).Name}, but got one of {command.EntityType.Name}";
        }

        protected override bool IsActuallyValid(Command.Delete.Delete effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((Command.Delete.Delete)seb).Entity);
        }
    }
}
