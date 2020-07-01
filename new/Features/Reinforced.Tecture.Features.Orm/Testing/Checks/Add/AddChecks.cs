using System;
using System.Reflection;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Generation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Add
{
    public static class AddChecks
    {
        public static AddEntityTypeCheck<T> Add<T>(Memorize<T> mem = null) => new AddEntityTypeCheck<T>();
        public static AddPredicateCheck<T> Add<T>(Func<T, bool> predicate, string explanation, Memorize<T> mem = null) => new AddPredicateCheck<T>(predicate, explanation);
    }

    sealed class AddCheckDescription : CheckDescription<Command.Add.Add>
    {
        public override MethodInfo Method => 
            UseMethod(() => AddChecks.Add<object>(null));

        protected override Type[] GetTypeArguments(Command.Add.Add command)
        {
            return new Type[] { command.EntityType };
        }
    }
}
