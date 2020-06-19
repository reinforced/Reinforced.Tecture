using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Update
{
    public class UpdateEntityTypeCheck<T> : CommandCheck<Command.Update.Update>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public UpdateEntityTypeCheck(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }
        protected override string GetMessage(Command.Update.Update command)
        {
            if (command == null) return $"expected Updateed entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected Updateed entity of type {typeof(T).Name}, but got one of {command.EntityType.Name}";
        }

        protected override bool IsActuallyValid(Command.Update.Update effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((Command.Update.Update)seb).Entity);
        }
    }
}
