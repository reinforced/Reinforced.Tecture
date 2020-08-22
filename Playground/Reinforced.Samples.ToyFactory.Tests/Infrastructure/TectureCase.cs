using System;
using System.IO;
using Reinforced.Tecture;
using Reinforced.Tecture.Features.Orm.Testing.Checks;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Samples.ToyFactory.Tests.Infrastructure
{
    public class TectureCase : IDisposable
    {
        private readonly ITecture _instance;
        private readonly string _caseName;
        private readonly string _ns;
        private readonly string _rootDir;
        private readonly bool _generateStuff;

        public TectureCase(ITecture instance, string caseName, string ns, string rootDir, bool generateStuff)
        {
            _instance = instance;
            _caseName = caseName;
            _ns = ns;
            _rootDir = rootDir;
            _generateStuff = generateStuff;
            _instance.BeginTrace();
        }

        private Trace _trace;

        private Trace Trace
        {
            get
            {
                if (_trace == null) _trace = _instance.EndTrace();
                return _trace;
            }
        }

        public void GenerateUnitTest()
        {
            var className = $"Validation";
            var go = Trace.GenerateUnitTest(className, _ns, g => { g.CheckOrm(); });
            var s = go.ToFullString();
            go.ToFile(Path.Combine(_rootDir, $"{className}.cs"));
        }

        public void GenerateValidation()
        {
            var className = $"TestData";
            var go = Trace.GenerateTestData(className, _ns);
            var s = go.ToFullString();
            go.ToFile(Path.Combine(_rootDir, $"{className}.cs"));
        }

        public void Validate<T>() where T : ValidationBase, new()
        {
            var vb = new T();
            vb.Validate(Trace);
        }


        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (_generateStuff)
            {
                GenerateUnitTest();
                GenerateValidation();
            }
            _instance.Dispose();
        }
    }
}
