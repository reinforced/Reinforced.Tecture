using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Checks.Delete
{
    public class DeleteEntityTypeCheck<T> : CommandCheck<Commands.Delete.Delete>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public DeleteEntityTypeCheck(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }
        protected override string GetMessage(Commands.Delete.Delete command)
        {
            if (command == null) return $"expected removed entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected removed entity of type {typeof(T).Name}, but got one of {command.EntityType.Name}";
        }

        protected override bool IsActuallyValid(Commands.Delete.Delete effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((Commands.Delete.Delete)seb).Entity);
        }
    }
}
