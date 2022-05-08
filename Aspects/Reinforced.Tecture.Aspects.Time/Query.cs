using System;
using System.Runtime.CompilerServices;
using Reinforced.Tecture.Aspects.Time.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects.Time
{
    public class Query : QueryAspect
    {
        internal Query()
        {
            
        }

        internal TestingContext Context => base.Context;
        internal int Index;
        
        
        public override void Dispose()
        { }
    }
}