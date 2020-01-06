using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Integrate
{
    public interface IResolve
    {
        object GetInstance(Type t);
    }
}
