using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Add
{
    public class AddEntityTypeCheck<T> : CommandCheck<Features.Orm.Command.Add.Add>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public AddEntityTypeCheck(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }

        protected override string GetMessage(Features.Orm.Command.Add.Add command)
        {
            if (command == null) return $"expected added entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected added entity of type {typeof(T).Name}, but got one of {command.EntityType.Name}";
        }

        protected override bool IsActuallyValid(Features.Orm.Command.Add.Add effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((Features.Orm.Command.Add.Add)seb).Entity);
        }
    }
}
