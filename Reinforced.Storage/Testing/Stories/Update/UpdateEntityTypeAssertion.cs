using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Update
{
    public class UpdateEntityTypeAssertion<T> : SideEffectAssertion<UpdateSideEffect>, IMemorizing
    {
        private readonly Memorize<T> _memorizedValue;

        public UpdateEntityTypeAssertion(Memorize<T> mem = null)
        {
            _memorizedValue = mem;
        }
        public override string GetMessage(UpdateSideEffect effect)
        {
            if (effect == null) return $"expected Updateed entity of type {typeof(T).Name}, but story unexpectedly ends";
            return $"expected Updateed entity of type {typeof(T).Name}, but got one of {effect.EntityType.Name}";
        }

        public override bool IsValid(UpdateSideEffect effect)
        {
            if (effect == null) return false;
            return effect.EntityType == typeof(T);
        }

        public void Memorize(SideEffectBase seb)
        {
            _memorizedValue.SetValue(((UpdateSideEffect)seb).Entity);
        }
    }
}
