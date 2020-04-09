using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Integrate
{
    public interface IRuntimeLocator
    {
        IEnumerable<TRuntime> GetRuntimes<TRuntime>(Func<TRuntime, bool> predicate = null) where TRuntime : ITectureRuntime;
    }
}