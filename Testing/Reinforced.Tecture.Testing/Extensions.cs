using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Testing.Validation.Format;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// Extensions for Trace tooling code generation
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Generates assertions code for an object instance
        /// </summary>
        /// <param name="obj">Object to validate</param>
        /// <param name="variableName">Variable name</param>
        /// <param name="assertionName">Reference to assertion name</param>
        /// <typeparam name="T">Type of object to validate</typeparam>
        /// <returns>C# code to assert on this object</returns>
        public static string GenerateAssertions<T>(this T obj, string variableName, string assertionName = "Assert")
        {
            var expr = SyntaxFactory.IdentifierName(variableName);
            var assertRef = new AssertInstanceReference(obj, typeof(T), expr, variableName, new HashSet<string>())
            {
                AssertionName = assertionName
            };
            var result = TypeAssertionGenerator.AssertionFor(assertRef);
            var sb = new StringBuilder();
            var formatter = new CodeFormatter();
            foreach (var invocationExpressionSyntax in result)
            {
                var statement = SyntaxFactory.ExpressionStatement(invocationExpressionSyntax);
                var node = formatter.Visit(statement);
                sb.AppendLine(node.ToString());
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Generates validation code for trace
        /// </summary>
        /// <param name="trace">Trace instance</param>
        /// <param name="className">Desired trace class name</param>
        /// <param name="ns">Desired trace namespace</param>
        /// <param name="optOutChecks">Opt-out check flags</param>
        /// <returns>Generate output</returns>
        public static GenerationOutput GenerateValidation(this Trace trace, string className, string ns, HashSet<string> optOutChecks = null)
        {
            if (trace.IsLightTrace)
                throw new TectureException("Cannot generate validation using light trace");
            
            var generator = new ValidationGenerator(className, ns);
            generator.Before();
            generator.Generate(trace.Commands);
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
        public static GenerationOutput GenerateData(this Trace trace, string className, string ns, Action<CSharpTestDataGeneratorSetup> setup = null)
        {
            if (trace.IsLightTrace)
                throw new TectureException("Cannot capture test data using light trace");
            
            var tc = CSharpTestDataGeneratorSetup.Create(className, ns);
            setup?.Invoke(tc);

            var r = new CSharpCodeTestDataGenerator(tc);

            r.Proceed(trace.Queries);

            return new GenerationOutput(r);
        }
    }
}
