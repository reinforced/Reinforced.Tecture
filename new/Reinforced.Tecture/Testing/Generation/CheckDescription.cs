using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Generation
{
    /// <summary>
    /// Do not inherit this class
    /// </summary>
    public abstract class CheckDescription
    {
        internal CheckDescription() { }

        public abstract MethodInfo Method { get; }

        public virtual IEnumerable<string> AdditionalUsings
        {
            get
            {
                yield break;
            }
        }

        public abstract Type[] MethodTypeArgumentsEvaluator(CommandBase command);

        public abstract object[] MethodArgumentsEvaluator(CommandBase command);

        internal static readonly Type[] EmptyTypes = new Type[0];

        internal static readonly object[] EmptyObjects = new object[0];

    }

    /// <summary>
    /// Description of check for particular command.
    /// Used by test generator in order to generate needed checks
    /// </summary>
    /// <typeparam name="TCommand">Type of command</typeparam>
    public abstract class CheckDescription<TCommand> : CheckDescription where TCommand : CommandBase
    {
        /// <summary>
        /// Extracts method info from lambda expression
        /// </summary>
        /// <param name="marker">Lambda expression in form of ()=>Checks.Some()</param>
        /// <returns>Method info</returns>
        protected MethodInfo UseMethod(Expression<Func<CommandCheck<TCommand>>> marker)
        {
            var bdy = marker.Body;

            Expression invok = bdy;
            if (invok.NodeType == ExpressionType.Convert)
            {
                var u = invok as UnaryExpression;
                invok = u.Operand;
            }

            var mex = invok as MethodCallExpression;
            if (mex==null)
                throw new TectureException("Check description marker must be invokation expression");

            var m = mex.Method;
            if (m.IsGenericMethod) return m.GetGenericMethodDefinition();
            return m;
        }

        public sealed override Type[] MethodTypeArgumentsEvaluator(CommandBase command)
        {
            if (command is TCommand tc) return GetTypeArguments(tc);
            throw new TectureException($"Command is of type {command.GetType()} whether required is {typeof(TCommand)}");
        }

        public sealed override object[] MethodArgumentsEvaluator(CommandBase command)
        {
            if (command is TCommand tc) return GetArguments(tc);
            throw new TectureException($"Command is of type {command.GetType()} whether required is {typeof(TCommand)}");
        }

        protected virtual Type[] GetTypeArguments(TCommand command)
        {
            return EmptyTypes;
        }

        protected virtual object[] GetArguments(TCommand command)
        {
            return EmptyObjects;
        }
    }
}