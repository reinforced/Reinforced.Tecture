using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Testing.Validation
{
    public partial class ValidationGenerator
    {
        public static HashSet<Type> ForbiddenTypes = new HashSet<Type>()
        {
            typeof(Delegate),
            typeof(Expression)
        };
    }
}
