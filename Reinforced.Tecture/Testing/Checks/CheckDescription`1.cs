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
        public sealed override Type[] MethodTypeArgumentsEvaluator(CommandBase command)
        {
            if (command is TCommand tc) return GetTypeArguments(tc);
            throw new TectureException($"Command is of type {command.GetType()} whether required is {typeof(TCommand)}");
        }

        protected virtual Type[] GetTypeArguments(TCommand command)
        {
            return EmptyTypes;
        }

    }
}
