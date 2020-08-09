using System;
using System.IO;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Commands.Exact;

namespace Reinforced.Tecture.Features.Orm.Commands.Derelate
{
    [CommandCode("-REL")]
    public class Derelate : CommandBase
    {
        public object Primary { get; internal set; }

        public Type PrimaryType { get; internal set; }

        public object Secondary { get; internal set; }

        public Type SecondaryType { get; internal set; }

        public string ForeignKeySpecifier { get; internal set; }

        /// <summary>
        /// Describes actions that are being performed within command
        /// </summary>
        /// <param name="tw">Log writer</param>
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

                tw.Write($"Remove reference from {secondaryyDescription} to {primaryDescription} by {ForeignKeySpecifier}");
            }

            if (Debug != null) tw.Write($" ({Debug.Location})");
        }
    }
}
