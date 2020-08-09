using System.IO;

namespace Reinforced.Storage.SideEffects.Exact
{
    /// <summary>
    /// Side effect that does nothing except explainig things
    /// </summary>
    [SideEffectCode("COMMENT")]
    public class CommentSideEffect : SideEffectBase
    {
        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            tw.Write(Annotation);
            if (Debug != null) tw.Write($" ({Debug.Location})");
        }
    }
}
