using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Testing.Generation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Delete
{
    sealed class DeleteCheckDescription : CheckDescription<Commands.Delete.Delete>
    {
        public override MethodInfo Method =>
            UseMethod(() => DeleteChecks.Delete<object>(null));

        protected override Type[] GetTypeArguments(Commands.Delete.Delete command)
        {
            return new Type[] { command.EntityType };
        }
    }

    public static class Descriptions
    {
        public static void Basic(this ChecksConfigurator<Commands.Delete.Delete> c) => c.Add(new DeleteCheckDescription());
    }
}
