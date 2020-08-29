using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Cloning
{
    public class TypeCloneTooling
    {
        /// <summary>
        /// Delegate of (instance, DeepCloneOperator) => Object
        /// </summary>
        public Delegate Shallow { get; set; }


        /// <summary>
        /// Delegate of (sourceInstance, targetInstance, DeepCloneOperator)
        /// </summary>
        public Delegate Bind { get; set; }
    }
}
