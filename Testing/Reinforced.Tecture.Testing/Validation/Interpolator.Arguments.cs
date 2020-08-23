using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Validation
{
    partial class CSharpUnitTestGenerator
    {
        private IEnumerable<ExpressionSyntax> ProduceArguments(CommandBase command, CheckDescription desc)
        {
            var args = desc.CheckParameters;
            foreach (var o in args)
            {
                if (o is CommandExtractCheckParameter cecp)
                {
                    yield return ExtractFromCommand(command, cecp);
                }

                if (o is AssertionCheckParameter acp)
                {
                    continue;
                }
            }
        }

        private ExpressionSyntax ExtractFromCommand(CommandBase command, CommandExtractCheckParameter cecp)
        {
            var value = cecp.Extractor.DynamicInvoke(command);
            ExpressionSyntax result;
            if (cecp.Type.IsInlineable())
            {
                EnsureUsing(cecp.Type);
                result = TypeInitConstructor.Construct(cecp.Type, value);
            }
            else
            {
                throw new Exception($"{cecp.Type} is not inlineable into tests");
            }

            return result;
        }

        //private ExpressionSyntax ExtractFromCommand(CommandBase command, CommandExtractCheckParameter cecp)
        //{
        //    var value = cecp.Extractor(command);
        //    ExpressionSyntax result;
        //    if (cecp.Type.IsInlineable())
        //    {
        //        EnsureUsing(cecp.Type);
        //        result = TypeInitConstructor.Construct(cecp.Type, value);
        //    }
        //    else
        //    {
        //        throw new Exception($"{cecp.Type} is not inlineable into tests");
        //    }

        //    return result;
        //}

    }
}
