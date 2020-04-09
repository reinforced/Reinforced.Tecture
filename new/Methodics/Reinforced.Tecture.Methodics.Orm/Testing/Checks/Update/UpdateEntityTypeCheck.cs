using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Update;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Checks.Update
{
    public class UpdateEntityTypeCheck<T> : CommandCheck<Commands.Update.Update>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public UpdateEntityTypeCheck(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }
        protected override string GetMessage(Commands.Update.Update command)
        {
            if (command == null) return $"expected Updateed entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected Updateed entity of type {typeof(T).Name}, but got one of {command.EntityType.Name}";
        }

        protected override bool IsActuallyValid(Commands.Update.Update effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((Commands.Update.Update)seb).Entity);
        }
    }
}
