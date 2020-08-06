using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection
{
    /// <summary>
    /// Strategy for collection syntax creation
    /// </summary>
    public interface ICollectionCreationStrategy
    {
        /// <summary>
        /// Produces collection creation syntax out of member expressions
        /// </summary>
        /// <param name="members">Set of member expressions</param>
        /// <param name="usings">Usings collection that is being produced by collection generation process</param>
        /// <returns>Collection creation syntax</returns>
        ExpressionSyntax Generate(IEnumerable<ExpressionSyntax> members, HashSet<string> usings);
    }
}
