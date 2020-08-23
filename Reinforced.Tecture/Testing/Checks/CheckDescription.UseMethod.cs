using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Checks
{
    public abstract partial class CheckDescription<TCommand> : CheckDescription where TCommand : CommandBase
    {
        private readonly List<ICheckParameter> _checkParameters = new List<ICheckParameter>();

        protected List<ICheckParameter> CheckParameters
        {
            get { return _checkParameters; }
        }

        public override IEnumerable<ICheckParameter> GetCheckParameters(CommandBase commandBase)
        {
            if (commandBase is TCommand tc) return GetCheckParameters(tc);
            throw new TectureException($"Command type mismatch: ${typeof(TCommand).Name} expected but {commandBase?.GetType().Name} got");
        }

        protected virtual IEnumerable<ICheckParameter> GetCheckParameters(TCommand commandBase)
        {
            return _checkParameters;
        }

        protected MethodInfo UseMethod(
            Expression<Func<IAnnotator, TCommand, CommandCheck<TCommand>>> marker)
        {
            var bdy = marker.Body;
            Expression invok = bdy;
            if (invok.NodeType == ExpressionType.Convert)
            {
                var u = invok as UnaryExpression;
                invok = u.Operand;
            }

            var mex = invok as MethodCallExpression;
            if (mex == null)
                throw new TectureException("Check description marker must be invokation expression");

            _checkParameters.Clear();
            ExtractParameters(marker.Parameters[1], marker.Parameters[0], mex.Arguments);

            var m = mex.Method;
            if (m.IsGenericMethod) return m.GetGenericMethodDefinition();
            return m;
        }

        private void ExtractParameters(ParameterExpression command, ParameterExpression annotator, IEnumerable<Expression> arguments)
        {
            foreach (var arg in arguments)
            {
                var a = arg;
                if (a is UnaryExpression ue)
                {
                    a = ue.Operand;
                }

                if (a is ConstantExpression ce)
                {
                    ExtractConstant(command, ce);
                    continue;
                }

                if (a is MemberExpression me)
                {
                    ExtractMember(command, me);
                    continue;
                }
                if (a is MethodCallExpression invex)
                {
                    ExtractInvokation(command, annotator, invex);
                    continue;
                }

                throw new TectureException($"Unknown check parameter expression: {arg}");
            }
        }

        private void ExtractConstant(ParameterExpression pe, ConstantExpression ce)
        {
            var lambda = Expression.Lambda(ce, pe).Compile();
            var cC = new CommandExtractCheckParameter()
            {
                Type = ce.Type,
                Extractor = lambda
            };
            _checkParameters.Add(cC);
        }

        private void ExtractMember(ParameterExpression pe, MemberExpression me)
        {
            var lambda = Expression.Lambda(me, pe).Compile();
            var cC = new CommandExtractCheckParameter()
            {
                Type = me.Type,
                Extractor = lambda
            };
            _checkParameters.Add(cC);
        }

        private void ExtractInvokation(ParameterExpression command, ParameterExpression annotator, MethodCallExpression invex)
        {
            if (invex.Object == annotator)
            {
                if (invex.Method == AnnotatorMethods.AssertionsMethod)
                {
                    var conv = Expression.Convert(invex.Arguments[0], typeof(object));
                    var lamb = Expression.Lambda(conv, command);
                    var le = lamb.Compile();
                    var cA = new AssertionCheckParameter()
                    {
                        Extractor = le,
                        Type = invex.Arguments[0].Type
                    };
                    _checkParameters.Add(cA);
                    return;
                }
            }

            var lambda = Expression.Lambda(invex, command).Compile();
            var cC = new CommandExtractCheckParameter()
            {
                Type = invex.Type,
                Extractor = lambda
            };
            _checkParameters.Add(cC);
        }
    }
}
