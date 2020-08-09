using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Storage.TestCodeMaker.TestData
{
    internal class LateBoundProperty
    {
        public PropertyInfo Property { get; set; }
        public NongenericExtractProfile Profile { get; set; }
        public Delegate OverrideCollection { get; set; }
        public bool IsCollection { get; set; }
    }
    /// <summary>
    /// Test data extraction profile (non-generic version for internal use)
    /// </summary>
    public class NongenericExtractProfile
    {
        internal NongenericExtractProfile(Type typeRef)
        {
            TypeRef = typeRef;
            var allProperties = typeRef.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var inline = new List<PropertyInfo>();

            foreach (var propertyInfo in allProperties)
            {
                if (TypeInitConstructor.IsInlineable(propertyInfo.PropertyType))
                {
                    inline.Add(propertyInfo);
                }
            }

            InlineProperties = inline.ToArray();
            _defaultInstance = Activator.CreateInstance(typeRef);
        }

        internal Dictionary<PropertyInfo,LateBoundProperty> LateBound { get; private set; } = new Dictionary<PropertyInfo, LateBoundProperty>();

        private readonly object _defaultInstance;
        internal Type TypeRef { get; private set; }
        internal PropertyInfo[] InlineProperties { get; private set; }


        internal ObjectCreationExpressionSyntax New(object instance, List<string> usings)
        {
            List<ExpressionSyntax> initNodes = new List<ExpressionSyntax>();

            foreach (var propertyInfo in InlineProperties)
            {
                var value = propertyInfo.GetValue(instance);
                var defValue = propertyInfo.GetValue(_defaultInstance);
                if (defValue != value)
                {
                    var pName = propertyInfo.Name;
                    var ae = SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                        SyntaxFactory.IdentifierName(pName),
                        TypeInitConstructor.Construct(propertyInfo.PropertyType, value,
                            out string usingDir)).WithTrailingTrivia(SyntaxFactory.LineFeed);

                    initNodes.Add(ae);
                    if (usingDir != null) usings.Add(usingDir);
                }
            }
            var inExp = SyntaxFactory.InitializerExpression(SyntaxKind.ObjectInitializerExpression,
                SyntaxFactory.SeparatedList(initNodes.ToArray()));
            var result = SyntaxFactory.ObjectCreationExpression(SyntaxFactory.ParseTypeName(TypeRef.Name))
                .WithArgumentList(
                    SyntaxFactory.ArgumentList()
                        .WithCloseParenToken(
                            SyntaxFactory.Token(
                                SyntaxFactory.TriviaList(),
                                SyntaxKind.CloseParenToken,
                                SyntaxFactory.TriviaList(
                                    SyntaxFactory.LineFeed))))
                .WithInitializer(inExp);

            return result;
        }
    }

    /// <summary>
    /// Point of configuration of extract profile
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtractProfile<T> : NongenericExtractProfile
    {
        internal ExtractProfile() : base(typeof(T))
        {
            
        }
    }
}
