using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;

namespace Reinforced.Tecture.Aspects.Orm.Testing.Checks.UpdatePk
{
    sealed class UpdatePkDescription : CheckDescription<Commands.UpdatePk.UpdatePk>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => UpdatePkChecks.UpdateByPK<object>(c.Annotation, null));

        protected override Type[] GetTypeArguments(Commands.UpdatePk.UpdatePk command)
        {
            return new Type[] { command.EntityType };
        }

        protected override IEnumerable<ICheckParameter> GetCheckParameters(Commands.UpdatePk.UpdatePk commandBase)
        {
            var bs = base.GetCheckParameters(commandBase);
            yield return bs.First();

            for (int i = 0; i < commandBase.KeyValues.Length; i++)
            {
                int x = i;
                Func<Commands.UpdatePk.UpdatePk, object> fn = c => c.KeyValues[x];
                yield return new CommandExtractCheckParameter()
                {
                    Extractor = fn,
                    Type = commandBase.KeyValues[i] == null ? typeof(object) : commandBase.KeyValues[i].GetType()
                };
            }
        }
    }

    sealed class UpdatePkCheckDescription : CheckDescription<Commands.UpdatePk.UpdatePk>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => UpdatePkChecks.UpdatedValues<object>(c.UpdateValuesStringKeys, c.Annotation));

        protected override Type[] GetTypeArguments(Commands.UpdatePk.UpdatePk command)
        {
            return new Type[] { command.EntityType };
        }
    }

    /// <summary>
    /// Descriptions
    /// </summary>
    public static class Descriptions
    {
        /// <summary>
        /// Basic checks for UpdatePk command
        /// </summary>
        /// <param name="c">Checks builder</param>
        public static void Basic(this ChecksBuilderFor<Commands.UpdatePk.UpdatePk> c)
        {
            c.Enlist(new UpdatePkDescription());
            c.Enlist(new UpdatePkCheckDescription());
        }
    }
}
