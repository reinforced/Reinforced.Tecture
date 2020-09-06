using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.DeletePk
{
    sealed class DeletePkDescription : CheckDescription<Commands.DeletePk.DeletePk>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => DeletePKChecks.DeleteByPK<object>(c.Annotation, null));

        protected override Type[] GetTypeArguments(Commands.DeletePk.DeletePk command)
        {
            return new Type[] { command.EntityType };
        }

        protected override IEnumerable<ICheckParameter> GetCheckParameters(Commands.DeletePk.DeletePk commandBase)
        {
            var bs = base.GetCheckParameters(commandBase);
            yield return bs.First();

            for (int i = 0; i < commandBase.KeyValues.Length; i++)
            {
                int x = i;
                Func<Commands.DeletePk.DeletePk, object> fn = c => c.KeyValues[x];
                yield return new CommandExtractCheckParameter()
                {
                    Extractor = fn,
                    Type = commandBase.KeyValues[i] == null ? typeof(object) : commandBase.KeyValues[i].GetType()
                };
            }
        }
    }

    /// <summary>
    /// Descriptions
    /// </summary>
    public static class Descriptions
    {
        /// <summary>
        /// Basic checks for DeletePk command
        /// </summary>
        /// <param name="c">Checks builder</param>
        public static void Basic(this ChecksBuilderFor<Commands.DeletePk.DeletePk> c) => c.Enlist(new DeletePkDescription());
    }
}
