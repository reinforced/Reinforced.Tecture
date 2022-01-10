using System;
using System.IO;
using System.Text;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Delete
{
    /// <summary>
    /// Command for entity removal
    /// </summary>
    public sealed class Delete : CommandBase
    {
        public override string Code => "DEL";
        internal Delete() { }

        [Validated("entity to remove")]
        public object Entity { get; internal set; }

        [Validated("type of entity to delete")]
        public Type EntityType { get; internal set; }


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
            var description = $"entity of type {EntityType.Name}";
            if (!string.IsNullOrEmpty(Annotation)) description = Annotation;

            if (Entity is IDescriptive e) description = e.Describe();

            tw.Write($"Delete {description}");


            if (Debug != null) tw.Write($" ({Debug.Location})");
        }

        /// <inheritdoc />
        protected override CommandBase DeepCloneForTracing()
        {
            return new Delete()
            {
                Entity = DeepCloner.DeepClone(Entity),
                EntityType = EntityType
            };
        }
    }
}
