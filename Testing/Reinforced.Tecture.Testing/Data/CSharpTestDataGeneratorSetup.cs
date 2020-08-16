using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing.Data
{
    public class CSharpTestDataGeneratorSetup
    {
        internal CSharpTestDataGeneratorSetup() { }

        internal Action<Hijack> _hijackConfig;
        internal CollectionStrategies _collectionStrategies;
        internal string _className;
        internal string _namespace;

        internal static CSharpTestDataGeneratorSetup Create(string className, string ns)
        {
            if (string.IsNullOrEmpty(className)) throw new ArgumentNullException(nameof(className));
            if (string.IsNullOrEmpty(ns)) throw new ArgumentNullException(nameof(ns));

            var r = new CSharpTestDataGeneratorSetup();
            r._className = className;
            r._namespace = ns;
            return r;
        }

        public CSharpTestDataGeneratorSetup WithHijack(Action<Hijack> hijackSetup)
        {
            _hijackConfig = hijackSetup;
            return this;
        }

        public CSharpTestDataGeneratorSetup WithCollectionStrategies(CollectionStrategies cs)
        {
            _collectionStrategies = cs;
            return this;
        }
    }
}
