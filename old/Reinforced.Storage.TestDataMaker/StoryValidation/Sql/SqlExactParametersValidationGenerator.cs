using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Sql;
using Reinforced.Storage.TestCodeMaker.TestData;
using Reinforced.Storage.Testing.Stories.Common;
using Reinforced.Storage.Testing.Stories.Sql;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Sql
{
    public class SqlExactParametersValidationGenerator : ValidationGenerator<DirectSqlSideEffect>
    {
        public override InvocationExpressionSyntax Generate(DirectSqlSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            if (effect.Parameters.Length == 0)
            {
                staticUsings = null;
                regularUsings = null;
                return null;
            }
            staticUsings = new[] { typeof(SqlAssertions).FullName };
            regularUsings = null;
            var param = new List<ExpressionSyntax>();
            var usings = new List<string>();
            foreach (var effectParameter in effect.Parameters)
            {
                if (effectParameter==null)
                    param.Add(LiteralExpression(SyntaxKind.NullLiteralExpression));
                else
                {
                    param.Add(TypeInitConstructor.Construct(effectParameter.GetType(),effectParameter,out string genUsing));
                    if (genUsing!=null) usings.Add(genUsing);
                }
            }

            regularUsings = usings.ToArray();
            return Invoke(nameof(SqlAssertions.SqlExactParameters), param.ToArray());
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public  static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<DirectSqlSideEffect> ExactParameters(this PerSideEffectGeneratorSetup<DirectSqlSideEffect> x)
        {
            return x.Register(new SqlExactParametersValidationGenerator());
        }
    }
}
