using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Command
{
    public abstract class Orm : CommandFeature
    {
        internal bool IsSubjectCore(Type t)
        {
            return IsSubject(t);
        }

        protected abstract bool IsSubject(Type t);

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            
        }
    }
}
