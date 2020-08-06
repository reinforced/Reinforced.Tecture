using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Tecture.Testing.Data
{
    class GenerationContext
    {
        public HashSet<string> Usings { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public GenerationContext(HashSet<string> usings)
        {
            Usings = usings;
        }

        public void AddUsing(string usng)
        {
            if (string.IsNullOrEmpty(usng)) return;
            if (!Usings.Contains(usng)) Usings.Add(usng);
        }

        public Queue<StatementSyntax> Statements { get; } = new Queue<StatementSyntax>();

        private int _counter;
        public string DefineVariable()
        {
            _counter++;
            return $"v{_counter}";
        }
    }
}
