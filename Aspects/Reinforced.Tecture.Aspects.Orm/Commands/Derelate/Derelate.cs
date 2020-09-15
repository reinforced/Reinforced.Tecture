using System;
using System.IO;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Derelate
{
    /// <summary>
    /// Command that breaks 1-to-many relation
    /// </summary>
    [CommandCode("-RF")]
    public class Derelate : CommandBase
    {
        /// <summary>
        /// Gets primary end of relation
        /// </summary>
        public object Primary { get; internal set; }

        /// <summary>
        /// Gets secondary end of relation
        /// </summary>
        public Type PrimaryType { get; internal set; }

        /// <summary>
        /// Gets foreign key to break relation by
        /// </summary>
        public string ForeignKeySpecifier { get; internal set; }

        /// <inheritdoc />
        public override void Describe(TextWriter tw)
        {
            if (!string.IsNullOrEmpty(Annotation))
            {
                tw.Write(Annotation);
            }
            else
            {
                var primaryDescription = $"entity of type {PrimaryType.Name}";
                if (Primary is IDescriptive e) primaryDescription = e.Describe();

                tw.Write($"Remove reference to {primaryDescription} by {ForeignKeySpecifier}");
            }

            if (Debug != null) tw.Write($" ({Debug.Location})");
        }

        /// <inheritdoc />
        protected override CommandBase DeepCloneForTracing()
        {
            return new Derelate()
            {
                Primary = Primary.DeepClone(),
                PrimaryType = PrimaryType,
                ForeignKeySpecifier = ForeignKeySpecifier
            };
        }
    }
}
