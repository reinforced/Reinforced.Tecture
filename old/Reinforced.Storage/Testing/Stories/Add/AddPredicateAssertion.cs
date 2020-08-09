using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Add
{
    public class AddPredicateAssertion<T> : CommandCheck<AddSideEffect>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;
        private readonly Func<T, bool> _predicate;
        private readonly string _explanation;

        public AddPredicateAssertion(Func<T, bool> predicate, string explanation, Memorize<T> mem = null)
        {
            _predicate = predicate;
            _explanation = explanation;
            _memorizedValue = mem;
        }

        public override string GetMessage(AddSideEffect command)
        {
            if (command == null) return $"expected added entity {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected added entity of type {typeof(T).Name} and {_explanation}, but got one of {command.EntityType.Name}";
            }
            return $"expected added entity {_explanation}, but seems that it does not";
        }

        public override bool IsActuallyValid(AddSideEffect effect)
        {
            if (effect == null) return false;
            if (effect.EntityType != typeof(T)) return false;
            return _predicate((T) effect.Entity);
        }

        public void Memorize(SideEffectBase seb)
        {
            _memorizedValue.SetValue(((AddSideEffect)seb).Entity);
        }
    }
}
