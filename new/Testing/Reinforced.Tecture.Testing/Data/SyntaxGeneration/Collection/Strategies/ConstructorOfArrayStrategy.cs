using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection.Strategies
{
    class ConstructorOfArrayStrategy : ICollectionCreationStrategy
    {
        private readonly Type _collectionType;
        private readonly ArrayCreationStrategy _arrayStrategy;
        public ConstructorOfArrayStrategy(Type collectionType, Type elementType)
        {
            _collectionType = collectionType;
            _arrayStrategy = new ArrayCreationStrategy(elementType);
        }

        public ExpressionSyntax Generate(IEnumerable<ExpressionSyntax> members, HashSet<string> usings)
        {
            var array = _arrayStrategy.GenerateArray(members,usings);
            return ObjectCreationExpression(_collectionType.TypeName(usings))
                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(array))));
        }
    }
}
