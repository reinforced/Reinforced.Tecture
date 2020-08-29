using System;
using System.IO;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Tecture.Features.Orm.Commands.Add
{
    /// <summary>
    /// Command for entity addition
    /// </summary>
    [CommandCode("ADD")]
    public class Add : CommandBase
    {
        internal Add() { }
        public object Entity { get; internal set; }

        public Type EntityType { get; internal set; }


        /// <summary>
        /// Describes actions that are being performed within command
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {

            var description = $"entity of type {EntityType.Name}";
            if (!string.IsNullOrEmpty(Annotation)) description = Annotation;

            if (Entity is IDescriptive e) description = e.Descibe();

            tw.Write($"Add {description}");


            if (Debug != null) tw.Write($" ({Debug.Location})");
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            return new Add()
            {
                Entity = Entity.DeepClone(),
                EntityType = EntityType
            };
        }
    }

    public class Add<T> : Add, IAddition<T>
    {
        internal Add(T entity)
        {
            Entity = entity;
            EntityType = typeof(T);
        }
    }
}
