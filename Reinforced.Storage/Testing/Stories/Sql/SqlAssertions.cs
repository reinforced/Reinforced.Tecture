using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Sql
{
    public static class SqlAssertions
    {
        public static SqlCommandTextAssertion SqlCommand(string pattern, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase) => new SqlCommandTextAssertion(comparison, pattern, CommandTextAssertionType.Contains);
        public static SqlCommandTextAssertion SqlExactCommand(string pattern, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase) => new SqlCommandTextAssertion(comparison, pattern, CommandTextAssertionType.Exact);
        public static SqlExactParametersAssertion SqlExactParameters(params object[] parmeters) => new SqlExactParametersAssertion(parmeters);
        public static SqlParamPredicateAssertion SqlParameters(params SqlParameterValidator[] validators) => new SqlParamPredicateAssertion(validators, false);
        public static SqlParamPredicateAssertion SqlAllParameters(params SqlParameterValidator[] validators) => new SqlParamPredicateAssertion(validators, true);
    }
}
