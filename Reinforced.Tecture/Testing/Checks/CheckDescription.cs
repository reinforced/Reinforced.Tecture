using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Checks
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

        public abstract IEnumerable<ICheckParameter> GetCheckParameters(CommandBase commandBase);

        internal static readonly Type[] EmptyTypes = new Type[0];

        internal static readonly object[] EmptyObjects = new object[0];

    }

    
}