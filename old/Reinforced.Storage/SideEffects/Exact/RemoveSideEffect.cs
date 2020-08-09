using System;
using System.IO;

namespace Reinforced.Storage.SideEffects.Exact
{
    /// <summary>
    /// Side effect for entity removal
    /// </summary>
    [SideEffectCode("DEL")]
    public class RemoveSideEffect : SideEffectBase
    {
        public object Entity { get; internal set; }

        public Type EntityType { get; internal set; }

        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            var description = $"entity of type {EntityType.Name}";
            if (!string.IsNullOrEmpty(Annotation)) description = Annotation;

            if (Entity is IDescriptive e) description = e.Descibe();

            tw.Write($"Remove {description}");


            if (Debug != null) tw.Write($" ({Debug.Location})");
        }
    }
}
