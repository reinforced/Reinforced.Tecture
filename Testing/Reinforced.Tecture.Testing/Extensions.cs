using System;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// Extensions for Trace tooling code generation
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Generates validation for trace
        /// </summary>
        /// <param name="trace">Trace instance</param>
        /// <param name="className">Desired trace class name</param>
        /// <param name="ns">Desired trace namespace</param>
        /// <param name="config">Generator configuration</param>
        /// <returns>Generate output</returns>
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

        /// <summary>
        /// Generates test data out of trace
        /// </summary>
        /// <param name="trace">Trace instance</param>
        /// <param name="className">Desired trace class name</param>
        /// <param name="ns">Desired trace namespace</param>
        /// <param name="setup">Test data generator setup</param>
        /// <returns>Generated output</returns>
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
