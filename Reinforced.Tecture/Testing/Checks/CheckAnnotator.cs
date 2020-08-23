using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Checks
{
    public interface IAnnotator
    {
        Func<object,bool> Assertions(object commandField);
    }
}
