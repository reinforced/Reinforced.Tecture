using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Common
{
    public static class CommonAssertions
    {
        public static AnnotationAssertion Annotated(string annotationText) => new AnnotationAssertion(annotationText);
        public static SaveAssertion Saved() => new SaveAssertion();
        public static CommentCheck Comment(string content = null) => new CommentCheck(content);
    }
}
