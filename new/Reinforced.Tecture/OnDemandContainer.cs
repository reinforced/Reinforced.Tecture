using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture
{
    struct ContainerRecord
    {
        
    }
    class OnDemandContainer
    {
        private object Resolve(ContainerRecord record)
        {

        }

        #region Keyed
        private Dictionary<string, ContainerRecord> _keyed = new Dictionary<string, ContainerRecord>();
        private Dictionary<string, object> _keyedInstaces = new Dictionary<string, object>();

        private object _keyedRegisterLocker = new object();
        private object _keyedInstanceLocker = new object();
        #endregion

        #region Typed

        private Dictionary<Type, ContainerRecord> _typed = new Dictionary<Type, ContainerRecord>();
        private Dictionary<string, object> _typedInstances = new Dictionary<string, object>();

        #endregion
        

        
    }
}
