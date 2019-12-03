using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Common;
using Reinforced.Storage.Testing.Stories;
using Reinforced.Storage.Testing.Stories.Common;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Common
{
    public class SaveValidationGenerator : ValidationGenerator<SaveChangesSideEffect>
    {
        public override InvocationExpressionSyntax Generate(SaveChangesSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            staticUsings = new[] {typeof(CommonAssertions).FullName};
            regularUsings = null;

            return Invoke(nameof(CommonAssertions.Saved));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<SaveChangesSideEffect> Validate(this PerSideEffectGeneratorSetup<SaveChangesSideEffect> x)
        {
            return x.Register(new SaveValidationGenerator());
        }
    }
}
