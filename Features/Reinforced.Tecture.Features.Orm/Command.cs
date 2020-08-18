using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.Commands.Delete;
using Reinforced.Tecture.Features.Orm.Commands.Derelate;
using Reinforced.Tecture.Features.Orm.Commands.Relate;
using Reinforced.Tecture.Features.Orm.Commands.Update;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Features.Orm
{
    public abstract class Command : CommandFeature, Produces<Add, Delete, Update, Relate, Derelate>
    {
        internal bool IsSubjectCore(Type t)
        {
            return IsSubject(t);
        }

        protected abstract bool IsSubject(Type t);

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();
    }
}
