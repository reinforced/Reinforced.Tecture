using System;
using System.IO;
using System.Text;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Add
{
    /// <summary>
    /// Command for entity addition
    /// </summary>
    public class Add : CommandBase
    {
        public override string Code => "ADD";
        internal Add() { }

        /// <summary>
        /// Entity to be added
        /// </summary>
        [Validated("added entity")]
        public object Entity { get; internal set; }

        /// <summary>
        /// Type of entity to be added
        /// </summary>
        [Validated("type of added entity")]
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

            tw.Write($"Add {description}");


            if (Debug != null) tw.Write($" ({Debug.Location})");
        }

        /// <inheritdoc />
        protected override CommandBase DeepCloneForTracing()
        {
            return new Add()
            {
                Entity = DeepCloner.DeepClone(Entity),
                EntityType = EntityType
            };
        }
    }

    /// <summary>
    /// Generic entity add command
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Add<T> : Add, IAddition<T>
    {
        internal Add(T entity)
        {
            Entity = entity;
            EntityType = typeof(T);
        }
    }
}
