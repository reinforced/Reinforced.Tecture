using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Add
{
    sealed class AddCheckDescription : CheckDescription<Commands.Add.Add>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => AddChecks.Add<object>(a.Assertions(c.Entity), c.Annotation));

        protected override Type[] GetTypeArguments(Commands.Add.Add command)
        {
            return new Type[] { command.EntityType };
        }

        

        private static object Extract(Commands.Add.Add cmd)
        {
            return cmd.Entity;
        }
        protected override IEnumerable<ICheckParameter> GetCheckParameters(Commands.Add.Add commandBase)
        {
            var props = commandBase.EntityType.GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .Where(x => x.Name != nameof(IPrimaryKey<int>.PrimaryKey));

            if (commandBase.Entity is IPrimaryKey pk)
            {
                var pkprops = pk.KeyProperties();
                props = props.Except(pkprops);
            }

            yield return new AssertionCheckParameter()
            {
                Extractor = new Func<Commands.Add.Add, object>(Extract),
                Type = commandBase.EntityType,
                PropertiesToAssert = props.ToArray()
            };
            yield return CheckParameters[1];
        }
    }

    /// <summary>
    /// Descriptions
    /// </summary>
    public static class Descriptions
    {
        /// <summary>
        /// Basic checks for Add command
        /// </summary>
        /// <param name="c">Checks builder</param>
        public static void Basic(this ChecksBuilderFor<Commands.Add.Add> c) => c.Enlist(new AddCheckDescription());
    }
}
