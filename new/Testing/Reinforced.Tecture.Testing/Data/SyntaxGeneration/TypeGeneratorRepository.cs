using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration
{
    class TypeGeneratorRepository
    {
        private readonly Dictionary<Type,Generator> _generators = new Dictionary<Type, Generator>();

        public Generator GetGeneratorFor(Type t)
        {
            EnsureGeneratorFor(t);
            return _generators[t];
        }

        public void EnsureGeneratorFor(Type t)
        {
            if (!_generators.ContainsKey(t))
            {
                _generators[t] = new Generator(t, this);
            }
        }
    }
}
