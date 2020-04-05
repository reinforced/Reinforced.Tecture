using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Bulk
{
    public class ValidatedBulkAssertion : CommandCheck<BulkSideEffect>
    {

        public override string GetMessage(BulkSideEffect command)
        {
            if (command == null) return $"bulk operation expected but story unexpectedly ends";

            if (!Environment.BulkOperations.HasAssumption(command))
            {
                var name = string.IsNullOrEmpty(command.Annotation) ? string.Empty : command.Annotation;
                return $"no assumption/validation for bulk operation {name}";
            }

            return null;//never
        }

        public override bool IsActuallyValid(BulkSideEffect effect)
        {
            if (effect == null) return false;
            return Environment.BulkOperations.HasAssumption(effect);
        }
    }
}
