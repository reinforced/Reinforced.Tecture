using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection.Strategies
{
    public class TupleCreationStrategy : ICollectionCreationStrategy
    {
        /// <summary>
        /// Produces collection creation syntax out of member expressions
        /// </summary>
        /// <param name="members">Set of member expressions</param>
        /// <param name="usings">Usings collection that is being produced by collection generation process</param>
        /// <returns>Collection creation syntax</returns>
        public ExpressionSyntax Generate(IEnumerable<ExpressionSyntax> members, HashSet<string> usings)
        {
            var sepList = SeparatedList<ArgumentSyntax>(members.Select(Argument).ComaSeparated());
            return TupleExpression(sepList);
        }
    }
}
