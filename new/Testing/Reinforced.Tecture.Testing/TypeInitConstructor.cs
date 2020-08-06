using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing
{
    public static class TypeInitConstructor
    {
        private static IEnumerable<SyntaxNodeOrToken> ComaSeparated(IEnumerable<TypeSyntax> types)
        {
            bool first = true;
            foreach (var typeSyntax in types)
            {
                if (!first) yield return Token(SyntaxKind.CommaToken);
                else first = false;
                yield return typeSyntax;
            }
        }

        private static string NormalizeTypeName(String typeName)
        {
            var idx = typeName.IndexOf("`");
            if (idx >= 0)
            {
                return typeName.Substring(0, idx);
            }

            return typeName;
        }
        public static TypeSyntax TypeName(this Type t, HashSet<string> usings = null)
        {
            if (!t.IsGenericType && !t.IsGenericTypeDefinition)
            {
                if (usings != null)
                {
                    if (!usings.Contains(t.Namespace)) usings.Add(t.Namespace);
                }
                return IdentifierName(t.Name);
            }

            if (t.IsGenericType)
            {
                var def = t.GetGenericTypeDefinition();
                var args = t.GetGenericArguments();
                var gn = GenericName(Identifier(NormalizeTypeName(def.Name)));
                TypeArgumentListSyntax argsSyntax;
                argsSyntax = TypeArgumentList(
                    args.Length == 1
                        ? SingletonSeparatedList(args[0].TypeName(usings))
                        : SeparatedList<TypeSyntax>(ComaSeparated(args.Select(v => v.TypeName(usings))))
                        );

                return gn.WithTypeArgumentList(argsSyntax);
            }

            throw new Exception("Cannot generaty type name");
        }

        #region Helpers

        /// <summary>
        ///     Determines if type is enumerable regardless of generic spec
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns>True if type is enumerable (incl. array type). False otherwise.</returns>
        public static bool IsEnumerable(this Type t)
        {
            if (t == typeof(string)) return false;
            if (t.IsArray) return true;
            if (typeof(IEnumerable).IsAssignableFrom(t)) return true;
            if (t.IsGenericType)
            {
                var tg = t.GetGenericTypeDefinition();
                if (tg.GetInterfaces().Any(x => x.Name == "IEnumerable`1")) return true;
            }
            return false;
        }

        public static Type ElementType(this Type t)
        {
            if (!t.IsEnumerable())
                return null;

            if (t.IsArray) return t.GetElementType();
            if (t.IsGenericType)
            {
                var tg = t.GetGenericTypeDefinition();
                if (tg.GetInterfaces().Any(x => x.Name=="IEnumerable`1")) return t.GetGenericArguments()[0];
            }

            return typeof(object);

        }

        /// <summary>
        /// Hash set of all numeric types
        /// </summary>
        public static readonly HashSet<Type> NumericTypes = new HashSet<Type>(new[]
        {
            typeof (byte),
            typeof (sbyte),
            typeof (short),
            typeof (ushort),
            typeof (int),
            typeof (uint),
            typeof (long),
            typeof (ulong),
            typeof (float),
            typeof (double),
            typeof (decimal),
            typeof (byte?),
            typeof (sbyte?),
            typeof (short?),
            typeof (ushort?),
            typeof (int?),
            typeof (uint?),
            typeof (long?),
            typeof (ulong?),
            typeof (float?),
            typeof (double?),
            typeof (decimal?)
        });

        /// <summary>
        /// Hash set of all integer types
        /// </summary>
        public static readonly HashSet<Type> IntegerTypes = new HashSet<Type>(new[]
        {
            typeof (byte),
            typeof (sbyte),
            typeof (short),
            typeof (ushort),
            typeof (int),
            typeof (uint),
            typeof (long),
            typeof (ulong),
            typeof (byte?),
            typeof (sbyte?),
            typeof (short?),
            typeof (ushort?),
            typeof (int?),
            typeof (uint?),
            typeof (long?),
            typeof (ulong?)
        });
        /// <summary>
        ///     Determines is type derived from Nullable or not
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns>True if type is nullable value type. False otherwise</returns>
        public static bool IsNullable(this Type t)
        {
            return (t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }
        #endregion

        public static bool IsInlineable(Type t)
        {
            if (t.IsNullable())
            {
                var nt = Nullable.GetUnderlyingType(t);
                return IsInlineable(nt);
            }
            if (NumericTypes.Contains(t)) return true;
            if (t == typeof(bool)) return true;
            if (t == typeof(string)) return true;
            if (t == typeof(Guid)) return true;
            if (t == typeof(DateTime)) return true;
            if (t.IsEnum) return true;
            return false;
        }


        public static ExpressionSyntax Construct(Type t, object value)
        {
            if (value == null) return SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);
            if (t.IsNullable())
            {
                var nt = Nullable.GetUnderlyingType(t);
                return Construct(nt, Convert.ChangeType(value, nt));
            }

            if (NumericTypes.Contains(t))
            {
                return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, AutoLiteral(t, value));
            }

            if (t == typeof(string)) return String(value.ToString());
            if (t == typeof(bool)) return Bool((bool)value);
            if (t.IsEnum)
            {
                return EnumValue(t, value);
            }

            if (t == typeof(Guid)) return GuidValue((Guid)value);
            if (t == typeof(DateTime)) return DateTimeValue((DateTime)value);

            throw new Exception($"Add type constructor for {t}");
        }

        private static ExpressionSyntax DateTimeValue(DateTime value)
        {
            return SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName("DateTime"))
                .WithArgumentList(
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        SyntaxFactory.Literal(value.Year))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        SyntaxFactory.Literal(value.Month))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        SyntaxFactory.Literal(value.Day))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        SyntaxFactory.Literal(value.Hour))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        SyntaxFactory.Literal(value.Minute))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        SyntaxFactory.Literal(value.Second))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        SyntaxFactory.Literal(value.Millisecond))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.Argument(
                                    SyntaxFactory.MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        SyntaxFactory.IdentifierName("DateTimeKind"),
                                        SyntaxFactory.IdentifierName("Utc")))
                            })));
        }

        private static ExpressionSyntax GuidValue(Guid value)
        {
            var strGuid = value.ToString();
            return SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName("Guid"),
                        SyntaxFactory.IdentifierName("Parse")))
                .WithArgumentList(
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                            SyntaxFactory.Argument(
                                SyntaxFactory.LiteralExpression(
                                    SyntaxKind.StringLiteralExpression,
                                    SyntaxFactory.Literal(strGuid))))));
        }

        private static ExpressionSyntax EnumValue(Type t, object value)
        {
            var valName = Enum.GetName(t, value);
            return SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.IdentifierName(t.Name),
                SyntaxFactory.IdentifierName(valName));
        }

        private static SyntaxToken AutoLiteral(Type valueType, object value)
        {
            if (valueType == typeof(Byte)) //holy guacamole!
            {
                return SyntaxFactory.Literal((byte)value);
            }
            var t = typeof(SyntaxFactory);
            var literalMethods = t.GetMethod("Literal", new[] { valueType });
            if (literalMethods == null) throw new Exception("Cannot find appropriate literal method");

            return (SyntaxToken)literalMethods.Invoke(null, new[] { value });
        }
        private static ExpressionSyntax String(string value)
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                SyntaxFactory.Literal($"@\"{value}\"", value));
        }

        private static ExpressionSyntax Bool(bool value)
        {
            if (value) return SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression);
            return SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression);
        }

    }
}
