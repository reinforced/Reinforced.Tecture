using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Remove
{
    public class RemoveEntityTypeAssertion<T> : SideEffectAssertion<RemoveSideEffect>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public RemoveEntityTypeAssertion(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }
        public override string GetMessage(RemoveSideEffect effect)
        {
            if (effect == null) return $"expected removed entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected removed entity of type {typeof(T).Name}, but got one of {effect.EntityType.Name}";
        }

        public override bool IsValid(RemoveSideEffect effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(SideEffectBase seb)
        {
            _memorizedValue.SetValue(((RemoveSideEffect)seb).Entity);
        }
    }
}
