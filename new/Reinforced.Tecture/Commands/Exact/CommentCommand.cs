using System.IO;

namespace Reinforced.Tecture.Commands.Exact
{
    /// <summary>
    /// Command that does nothing except explainig things
    /// </summary>
    [CommandCode("COMMENT")]
    public class CommentCommand : CommandBase
    {
        /// <summary>
        /// Describes actions that are being performed within command
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            tw.Write(Annotation);
            if (Debug != null) tw.Write($" ({Debug.Location})");
        }
    }
}
