using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Sql;
using Reinforced.Storage.Testing.Stories.Sql;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Sql
{
    public class SqlExactCommandValidationGenerator : ValidationGenerator<DirectSqlSideEffect>
    {
        public override InvocationExpressionSyntax Generate(DirectSqlSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            staticUsings = new[] { typeof(SqlAssertions).FullName };
            regularUsings = null;

            return Invoke(nameof(SqlAssertions.SqlExactCommand), LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                Literal($"@\"{effect.Command}\"",effect.Command)));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public  static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<DirectSqlSideEffect> ExactCommand(this PerSideEffectGeneratorSetup<DirectSqlSideEffect> x)
        {
            return x.Register(new SqlExactCommandValidationGenerator());
        }
    }
}
