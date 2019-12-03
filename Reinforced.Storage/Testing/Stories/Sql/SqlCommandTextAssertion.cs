using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Sql
{
    public enum CommandTextAssertionType
    {
        /// <summary>
        /// Resulting SQL must exactly match supplied one
        /// </summary>
        Exact,

        /// <summary>
        /// Resulting SQL must contain supplied pattern
        /// </summary>
        Contains
    }
    public class SqlCommandTextAssertion : SideEffectAssertion<DirectSqlSideEffect>
    {
        private readonly StringComparison _comparison;
        private readonly string _pattern;
        private readonly CommandTextAssertionType _assertionType;

        public SqlCommandTextAssertion(StringComparison comparison, string pattern, CommandTextAssertionType assertionType)
        {
            _comparison = comparison;
            _pattern = pattern;
            _assertionType = assertionType;
        }

        public override string GetMessage(DirectSqlSideEffect effect)
        {
            if (effect == null)
                return $"expected direct SQL with particular command text, but story unexpectedly ends";

            string name = "Following direct SQL";

            if (!string.IsNullOrEmpty(effect.Annotation))
                name = $"SQL for '{effect.Annotation}'";

            if (_assertionType == CommandTextAssertionType.Exact)
            {
                return $@"
{name}:
---
{effect.Command}
---
must EXACTLY MATCH ({_comparison}) this:
---
{_pattern}
---
But it does not.";
            }

            return $@"
{name}:
---
{effect.Command}
---
must contain ({_comparison}) following pattern:
---
{_pattern}
---
But it does not.";
        }

        public override bool IsValid(DirectSqlSideEffect effect)
        {
            if (effect == null) return false;
            if (_assertionType == CommandTextAssertionType.Exact)
            {
                return string.Equals(effect.Command, _pattern, _comparison);
            }

            return effect.Command.IndexOf(_pattern, _comparison) > 0;
        }
    }
}
