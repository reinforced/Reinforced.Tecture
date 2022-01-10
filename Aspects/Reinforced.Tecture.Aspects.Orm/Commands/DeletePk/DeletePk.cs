using System;
using System.IO;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects.Orm.Commands.DeletePk
{
    /// <summary>
    /// Command for deletion by primary key
    /// </summary>
    public class DeletePk : CommandBase
    {
        public override string Code => "DPK";
        /// <summary>
        /// Gets entity type to be deleted
        /// </summary>
        [Validated("type of entity to delete")]
        public Type EntityType { get; internal set; }

        /// <summary>
        /// Gets primary key values
        /// </summary>
        [Validated("primary key")]
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
        
        protected override string ToStringActually()
        {
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                Describe(tw);
                tw.Flush();
            }

            return sb.ToString();
        }

        /// <inheritdoc cref="CommandBase" />
        private void Describe(TextWriter tw)
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
