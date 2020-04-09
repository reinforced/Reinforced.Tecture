using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Checks.Add
{
    public class AddEntityTypeCheck<T> : CommandCheck<Commands.Add.Add>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public AddEntityTypeCheck(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }

        protected override string GetMessage(Commands.Add.Add command)
        {
            if (command == null) return $"expected added entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected added entity of type {typeof(T).Name}, but got one of {command.EntityType.Name}";
        }

        protected override bool IsActuallyValid(Commands.Add.Add effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((Commands.Add.Add)seb).Entity);
        }
    }
}
