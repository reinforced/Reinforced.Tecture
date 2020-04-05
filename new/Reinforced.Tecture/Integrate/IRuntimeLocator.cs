using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Integrate
{
    /// <summary>
    /// Tecture runtime locator interface
    /// </summary>
    public interface IRuntimeLocator
    {
        IEnumerable<TRuntime> GetRuntimes<TRuntime>(Func<TRuntime, bool> predicate) where TRuntime : ITectureRuntime;
    }
}
