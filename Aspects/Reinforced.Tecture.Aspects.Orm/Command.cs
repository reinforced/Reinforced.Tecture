using System;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.Commands.Delete;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;
using Reinforced.Tecture.Aspects.Orm.Commands.Derelate;
using Reinforced.Tecture.Aspects.Orm.Commands.Relate;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Aspects.Orm
{

    /// <summary>
    /// ORM command aspect
    /// </summary>
    public abstract class Command : CommandAspect, Produces<Add, Delete, Update, Relate, Derelate, DeletePk, UpdatePk>
    {
        internal bool IsSubjectCore(Type t)
        {
            return IsSubject(t);
        }

        /// <summary>
        /// Gets whether specified type can be handled by this aspect
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <returns>True when supplied type is valid to be handled within this aspect</returns>
        protected abstract bool IsSubject(Type t);
        
    }
}
