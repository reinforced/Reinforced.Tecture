using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Checks
{
    internal static class AnnotatorMethods
    {
        public static readonly MethodInfo AssertionsMethod;
        static AnnotatorMethods()
        {
            Expression<Func<IAnnotator, object>> vm = x => x.Assertions(null);
            AssertionsMethod = (vm.Body as MethodCallExpression)?.Method;
        }
    }
    /// <summary>
    /// Description of check for particular command.
    /// Used by test generator in order to generate needed checks
    /// </summary>
    /// <typeparam name="TCommand">Type of command</typeparam>
    public abstract partial class CheckDescription<TCommand> : CheckDescription where TCommand : CommandBase
    {
        /// <inheritdoc />
        public sealed override Type[] MethodTypeArgumentsEvaluator(CommandBase command)
        {
            if (command is TCommand tc) return GetTypeArguments(tc);
            throw new TectureException($"Command is of type {command.GetType()} whether required is {typeof(TCommand)}");
        }

        /// <summary>
        /// Gets list of check factory method type parameters from particular command
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>Types array to parametrize check factory by</returns>
        protected virtual Type[] GetTypeArguments(TCommand command)
        {
            return EmptyTypes;
        }

        /// <inheritdoc />
        public override bool IsNeeded(CommandBase command)
        {
            if (command is TCommand tc) return IsCheckNeeded(tc);
            return false;
        }

        /// <summary>
        /// Gets whether particular check is needed on supplied command
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>True if this check is needed on the command, false otherwise</returns>
        protected virtual bool IsCheckNeeded(TCommand command)
        {
            return true;
        }

    }
}
