using System;
using System.IO;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Add
{
    /// <summary>
    /// Command for entity addition
    /// </summary>
    [CommandCode("ADD")]
    public class Add : CommandBase
    {
        internal Add() { }

        /// <summary>
        /// Entity to be added
        /// </summary>
        public object Entity { get; internal set; }

        /// <summary>
        /// Type of entity to be added
        /// </summary>
        public Type EntityType { get; internal set; }


        /// <inheritdoc />
        public override void Describe(TextWriter tw)
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
                Entity = Entity.DeepClone(),
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
