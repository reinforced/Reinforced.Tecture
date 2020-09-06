using System;
using System.Collections.Generic;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.SqlStroke.Testing.Checks
{
    /// <summary>
    /// SQL command text check
    /// </summary>
    public class SqlCommandTextCheck : CommandCheck<Sql>
    {
        private readonly string _commandText;

        internal SqlCommandTextCheck(string commandText)
        {
            _commandText = commandText;
        }

        private string TakeFirstLine(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var i = s.IndexOf("\n", StringComparison.Ordinal);
            return s.Substring(0, i) + " <...>";
        }

        /// <summary>
        /// Gets error message (called only if command is not valid)
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>Error message</returns>
        protected override string GetMessage(Sql command)
        {
            if (command == null) return $"expected SQL command, but story unexpectedly ends";
            var p = command.Preview;
            return $"expect SQL command '{TakeFirstLine(_commandText)}', but '{TakeFirstLine(p.Query)}' got";
        }

        private IEnumerable<char> WithoutWhitespace(string x)
        {
            for (int j = 0; j < x.Length; j++)
            {
                if (!char.IsWhiteSpace(x[j]))
                    yield return x[j];
            }
        }

        private bool TokenizeCompare(string a, string b)
        {
            if (a == null && b != null) return false;
            if (a != null && b == null) return false;
            using (var s1 = WithoutWhitespace(a).GetEnumerator())
            using (var s2 = WithoutWhitespace(b).GetEnumerator())
            {
                bool c1 = s1.MoveNext();
                bool c2 = s2.MoveNext();
                while (c1 && c2)
                {
                    if (s1.Current != s2.Current)
                        return false;

                    c1 = s1.MoveNext();
                    c2 = s2.MoveNext();
                }

                return c1 == c2;
            }
        }
        /// <summary>
        /// Gets whether particular command instance is valid or not
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>True if command is valid, false otherwise</returns>
        protected override bool IsActuallyValid(Sql command)
        {
            if (command == null) return false;
            var p = command.Preview;

            return TokenizeCompare(p.Query, _commandText);
        }
    }
}
