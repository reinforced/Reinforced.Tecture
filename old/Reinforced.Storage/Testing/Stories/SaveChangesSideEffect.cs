using System.IO;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.Stories
{
    /// <summary>
    /// Save changes meta side-effect
    /// </summary>
    [SideEffectCode("SAVE")]
    public class SaveChangesSideEffect : SideEffectBase
    {
        internal SaveChangesSideEffect() { }
        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            tw.Write("====== Saved to storage =====");
        }
    }
}
