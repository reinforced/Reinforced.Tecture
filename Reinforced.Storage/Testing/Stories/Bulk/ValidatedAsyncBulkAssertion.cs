using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Bulk
{
    public class ValidatedAsyncBulkAssertion : SideEffectAssertion<AsyncBulkSideEffect>
    {

        public override string GetMessage(AsyncBulkSideEffect effect)
        {
            if (effect == null) return $"async bulk operation expected but story unexpectedly ends";

            if (!Environment.AsyncBulkOperations.HasAssumption(effect))
            {
                var name = string.IsNullOrEmpty(effect.Annotation) ? string.Empty : effect.Annotation;
                return $"no assumption/validation for async bulk operation {name}";
            }

            return null;//never
        }

        public override bool IsValid(AsyncBulkSideEffect effect)
        {
            if (effect == null) return false;
            return Environment.AsyncBulkOperations.HasAssumption(effect);
        }
    }
}
