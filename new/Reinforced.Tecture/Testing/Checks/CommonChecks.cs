using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Testing.Checks
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
        /// Checks that save happening in this place of story
        /// </summary>
        /// <returns>Save check</returns>
        public static SaveCheck Saved() => new SaveCheck();

        /// <summary>
        /// Checks that command appears in this place of story
        /// </summary>
        /// <param name="content">Comment content (optional)</param>
        /// <returns>Comment check</returns>
        public static CommentCheck Comment(string content = null) => new CommentCheck(content);
    }
}
