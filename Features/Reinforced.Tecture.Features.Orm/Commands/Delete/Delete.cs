using System;
using System.IO;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Features.Orm.Commands.Delete
{
    /// <summary>
    /// Command for entity removal
    /// </summary>
    [CommandCode("DEL")]
    public sealed class Delete : CommandBase
    {
        internal Delete() { }

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

            tw.Write($"Delete {description}");


            if (Debug != null) tw.Write($" ({Debug.Location})");
        }
    }
}
