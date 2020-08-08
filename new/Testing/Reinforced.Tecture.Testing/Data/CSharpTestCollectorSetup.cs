using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection;

namespace Reinforced.Tecture.Testing.Data
{
    public class CSharpTestCollectorSetup
    {
        internal Action<Hijack> _hijackConfig;
        internal CollectionStrategies _collectionStrategies;
        internal string _className;
        internal string _namespace;
        internal string _filenameToUpdate;

        public static CSharpTestCollectorSetup Create(string className, string ns)
        {
            if (string.IsNullOrEmpty(className)) throw new ArgumentNullException(nameof(className));
            if (string.IsNullOrEmpty(ns)) throw new ArgumentNullException(nameof(ns));

            var r = new CSharpTestCollectorSetup();
            r._className = className;
            r._namespace = ns;
            return r;
        }

        public CSharpTestCollectorSetup WithHijack(Action<Hijack> hijackSetup)
        {
            _hijackConfig = hijackSetup;
            return this;
        }

        public CSharpTestCollectorSetup WithCollectionStrategies(CollectionStrategies cs)
        {
            _collectionStrategies = cs;
            return this;
        }

        public CSharpCodeTestCollector ToFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            _filenameToUpdate = fileName;
            
            return new CSharpCodeTestCollector(this);
        }
    }
}
