using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Delete
{
    sealed class DeleteCheckDescription : CheckDescription<Commands.Delete.Delete>
    {
        public override MethodInfo Method =>
            UseMethod((a,c) => DeleteChecks.Delete<object>(a.Assertions(c.Entity),c.Annotation));

        protected override Type[] GetTypeArguments(Commands.Delete.Delete command)
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
        /// Basic checks for Delete command
        /// </summary>
        /// <param name="c">Checks builder</param>
        public static void Basic(this ChecksBuilderFor<Commands.Delete.Delete> c) => c.Enlist(new DeleteCheckDescription());
    }
}
