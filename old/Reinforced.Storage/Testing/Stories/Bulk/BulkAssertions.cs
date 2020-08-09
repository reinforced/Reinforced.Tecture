using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Bulk
{
    public static class BulkAssertions
    {
        public static ValidatedBulkAssertion ValidatedBulk() => new ValidatedBulkAssertion();
        public static ValidatedAsyncBulkAssertion ValidatedAsyncBulk() => new ValidatedAsyncBulkAssertion();
    }
}
