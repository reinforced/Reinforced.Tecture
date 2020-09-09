using System;
using System.IO;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Relate
{
    /// <summary>
    /// Command that creates 1-to-many relation
    /// </summary>
    [CommandCode("+REL")]
    public class Relate : CommandBase
    {
        /// <summary>
        /// Gets primary end of relation
        /// </summary>
        public object Primary { get; internal set; }

        /// <summary>
        /// Gets type of primary end of relation
        /// </summary>
        public Type PrimaryType { get; internal set; }

        /// <summary>
        /// Gets secondary end of relation
        /// </summary>
        public object Secondary { get; internal set; }

        /// <summary>
        /// Gets type of secondary end of relation
        /// </summary>
        public Type SecondaryType { get; internal set; }

        /// <summary>
        /// Gets foreign key to create relation by
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
                if (Primary is IDescriptive e) primaryDescription = e.Descibe();

                var secondaryyDescription = $"entity of type {SecondaryType.Name}";
                if (Secondary is IDescriptive e2) secondaryyDescription = e2.Descibe();

                tw.Write($"Reference {primaryDescription} from {secondaryyDescription} by {ForeignKeySpecifier}");
            }

            if (Debug != null) tw.Write($" ({Debug.Location})");
        }

        /// <inheritdoc />
        protected override CommandBase DeepCloneForTracing()
        {
            return new Relate()
            {
                PrimaryType = PrimaryType,
                SecondaryType = SecondaryType,
                Primary = Primary.DeepClone(),
                Secondary = Secondary.DeepClone(),
                ForeignKeySpecifier = ForeignKeySpecifier
            };
        }
    }
}
