using System;
using System.Reflection;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Aspects.Orm.Testing.Checks.Update
{
    sealed class UpdateCheckDescription : CheckDescription<Commands.Update.Update>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => UpdateChecks.Update<object>(a.Assertions(c.Entity), c.Annotation));

        protected override Type[] GetTypeArguments(Commands.Update.Update command)
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
        /// Basic checks for Update command
        /// </summary>
        /// <param name="c">Checks builder</param>
        public static void Basic(this ChecksBuilderFor<Commands.Update.Update> c) => c.Enlist(new UpdateCheckDescription());
    }
}
