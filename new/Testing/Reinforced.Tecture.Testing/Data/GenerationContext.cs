﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Tecture.Testing.Data
{
    public class GenerationContext
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
       

        public bool DefinedVariable(object target, out string identifier)
        {
            if (_definedObjects.ContainsKey(target))
            {
                identifier = $"v{_definedObjects[target]}";
                return true;
            }
            _counter++;
            _definedObjects[target] = _counter;
            identifier = $"v{_counter}";
            return false;
        }

        private readonly Dictionary<object,int> _definedObjects = new Dictionary<object, int>();
    }
}