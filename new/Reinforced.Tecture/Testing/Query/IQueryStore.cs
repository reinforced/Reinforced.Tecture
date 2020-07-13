using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Testing.Query
{
    public enum QueryMemorizeState
    {
        Put,
        Get
    }

    public interface IQueryStore
    {
        QueryMemorizeState State { get; set; }

        void Put(string hash, object result);

        T Get<T>(string hash);

        string CurrentHash { get; }
    }
}
