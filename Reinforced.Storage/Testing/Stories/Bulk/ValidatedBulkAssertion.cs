using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Bulk
{
    public class ValidatedBulkAssertion : SideEffectAssertion<BulkSideEffect>
    {

        public override string GetMessage(BulkSideEffect effect)
        {
            if (effect == null) return $"bulk operation expected but story unexpectedly ends";

            if (!Environment.BulkOperations.HasAssumption(effect))
            {
                var name = string.IsNullOrEmpty(effect.Annotation) ? string.Empty : effect.Annotation;
                return $"no assumption/validation for bulk operation {name}";
            }

            return null;//never
        }

        public override bool IsValid(BulkSideEffect effect)
        {
            if (effect == null) return false;
            return Environment.BulkOperations.HasAssumption(effect);
        }
    }
}
