using System;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration
{
    class TypeGeneratorRepository
    {
        public CollectionStrategies CollectionStrategies { get; }

        public Hijack Hijack { get; }

        private readonly Dictionary<Type, Generator> _generators = new Dictionary<Type, Generator>();

        public TypeGeneratorRepository(Hijack hijack, CollectionStrategies collectionStrategies = null)
        {
            Hijack = hijack;
            CollectionStrategies = collectionStrategies ?? new CollectionStrategies();
        }

        public IGenerator GetGeneratorFor(Type t)
        {

            EnsureGeneratorFor(t);
            return _generators[t];
        }

        public void EnsureGeneratorFor(Type t)
        {
            if (!_generators.ContainsKey(t))
            {
                var tm = new TypeMeta(t,Hijack);
                if (t.IsAnonymousType()) _generators[t] = new AnonymousDataGenerator(tm,this);
                else _generators[t] = new Generator(tm, this);
            }
        }
    }
}
