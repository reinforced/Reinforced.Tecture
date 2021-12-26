using System.Collections.Generic;

namespace Reinforced.Tecture.Aspects.Cache
{
    public class Hierarchy<T>
    {
        public T Data { get; set; }
        public List<Hierarchy<T>> Children { get; set; }
    }
}