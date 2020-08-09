using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture
{
    public interface Feature : IDisposable { }

    /// <summary>
    /// Query feature type
    /// </summary>
    public interface QueryFeature : Feature { }

    /// <summary>
    /// Command feature type
    /// </summary>
    public interface CommandFeature : Feature { }
}
