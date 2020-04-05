using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Delete
{
    public class DeleteEntityTypeCheck<T> : CommandCheck<DeleteCommand>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public DeleteEntityTypeCheck(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }
        protected override string GetMessage(DeleteCommand command)
        {
            if (command == null) return $"expected removed entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected removed entity of type {typeof(T).Name}, but got one of {command.EntityType.Name}";
        }

        protected override bool IsActuallyValid(DeleteCommand effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(CommandBase seb)
        {
            _memorizedValue.SetValue(((DeleteCommand)seb).Entity);
        }
    }
}
