using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Reinforced.Tecture.Testing.Validation
{
    class Annotations
    {
        public static SyntaxAnnotation ThenArgument = new SyntaxAnnotation("then");

        public static SyntaxAnnotation IfStatement = new SyntaxAnnotation("ifs");
    }
}
