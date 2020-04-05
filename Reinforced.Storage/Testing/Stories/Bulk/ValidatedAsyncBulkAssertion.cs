using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Bulk
{
    public class ValidatedAsyncBulkAssertion : CommandCheck<AsyncBulkSideEffect>
    {

        public override string GetMessage(AsyncBulkSideEffect command)
        {
            if (command == null) return $"async bulk operation expected but story unexpectedly ends";

            if (!Environment.AsyncBulkOperations.HasAssumption(command))
            {
                var name = string.IsNullOrEmpty(command.Annotation) ? string.Empty : command.Annotation;
                return $"no assumption/validation for async bulk operation {name}";
            }

            return null;//never
        }

        public override bool IsActuallyValid(AsyncBulkSideEffect effect)
        {
            if (effect == null) return false;
            return Environment.AsyncBulkOperations.HasAssumption(effect);
        }
    }
}
