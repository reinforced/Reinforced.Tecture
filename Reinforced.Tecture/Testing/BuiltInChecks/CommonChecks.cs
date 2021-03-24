using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Testing.BuiltInChecks
{
    /// <summary>
    /// Set of core story checks
    /// </summary>
    public static class CommonChecks
    {
        /// <summary>
        /// Checks that command is annotated with particular annotation
        /// </summary>
        /// <param name="annotationText">Annotation text</param>
        /// <returns>Annotation check</returns>
        public static AnnotationCheck Annotated(string annotationText) => new AnnotationCheck(annotationText);

        /// <summary>
        /// Checks that comment appears in this place of story
        /// </summary>
        /// <param name="content">Comment content (optional)</param>
        /// <returns>Comment check</returns>
        public static CommentCheck Comment(string content = null) => new CommentCheck(content);

        /// <summary>
        /// Checks that command is for specific channel
        /// </summary>
        /// <typeparam name="T">Type of channel</typeparam>
        /// <returns></returns>
        public static ChannelTypeCheck To<T>() where T : Channel => new ChannelTypeCheck(typeof(T));


    }
}
