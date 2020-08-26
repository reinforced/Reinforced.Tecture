using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Tecture.Features.Orm.Commands.DeletePk
{
    [CommandCode("DPK")]
    public class DeletePk : CommandBase
    {
        public Type EntityType { get; internal set; }

        public object[] KeyValues { get; internal set; }

        private IPrimaryKey CreateInstance(Type t)
        {
            try
            {
                return (IPrimaryKey)Activator.CreateInstance(t);
            }
            catch (Exception e)
            {
                return (IPrimaryKey)t.InstanceNonpublic();
            }
        }

        public override void Describe(TextWriter tw)
        {
            if (string.IsNullOrEmpty(Annotation))
            {
                tw.Write($"Delete {EntityType.Name} by PK");
                if (KeyValues.Length>1) tw.Write("s: ");
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
        }
    }
}
