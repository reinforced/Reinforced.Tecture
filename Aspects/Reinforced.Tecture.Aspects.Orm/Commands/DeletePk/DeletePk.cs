using System;
using System.IO;
using System.Linq;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Aspects.Orm.Commands.DeletePk
{
    /// <summary>
    /// Command for deletion by primary key
    /// </summary>
    [CommandCode("DPK")]
    public class DeletePk : CommandBase
    {
        /// <summary>
        /// Gets entity type to be deleted
        /// </summary>
        public Type EntityType { get; internal set; }

        /// <summary>
        /// Gets primary key values
        /// </summary>
        public object[] KeyValues { get; internal set; }

        private IPrimaryKey CreateInstance(Type t)
        {
            try
            {
                return (IPrimaryKey)Activator.CreateInstance(t);
            }
            catch (Exception)
            {
                return (IPrimaryKey)t.InstanceNonpublic();
            }
        }
        /// <inheritdoc />
        public override void Describe(TextWriter tw)
        {
            if (string.IsNullOrEmpty(Annotation))
            {
                tw.Write($"Delete {EntityType.Name} by PK");
                if (KeyValues.Length > 1) tw.Write("s: ");
                else tw.Write(": ");
                var instance = CreateInstance(EntityType);
                var properties = instance.KeyProperties();

                for (int i = 0; i < properties.Length; i++)
                {
                    tw.Write(properties[i].Name);
                    tw.Write(" = ");
                    tw.Write(KeyValues[i]);
                }
            }
            else
            {
                tw.Write(Annotation);
            }
        }

        /// <inheritdoc />
        protected override CommandBase DeepCloneForTracing()
        {
            return new DeletePk()
            {
                KeyValues = KeyValues.Select(x => DeepCloner.DeepClone(x)).ToArray(),
                EntityType = EntityType
            };
        }
    }
}
