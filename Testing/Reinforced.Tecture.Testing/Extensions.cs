using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Tecture.Testing.Generation;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing
{
    public static class Extensions
    {
        public static GenerationOutput GenerateUnitTest(this Trace trace, string className, string ns, Action<UnitTestGenerator> config = null)
        {
            UnitTestGenerator tg = new UnitTestGenerator();
            config(tg);

            var generator = new CSharpUnitTestGenerator(className, ns);
            generator.Before();
            tg.Proceed(trace.Commands,generator);
            generator.After();
            return new GenerationOutput(generator);
        }

        public static GenerationOutput GenerateTestData(this Trace trace, string className, string ns, Action<CSharpTestDataGeneratorSetup> setup = null)
        {
            var tc = CSharpTestDataGeneratorSetup.Create(className, ns);
            setup?.Invoke(tc);

            var r = new CSharpCodeTestDataGenerator(tc);

            r.Proceed(trace.Queries);

            return new GenerationOutput(r);
        }
    }
}
