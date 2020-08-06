using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Savers
{
    /// <summary>
    /// Base for all savers
    /// </summary>
    public abstract class SaverBase
    {
        internal void SaveInternal()
        {
            Save();
        }

        internal Task SaveInternalAsync()
        {
            return SaveAsync();
        }

        internal abstract IEnumerable<Type> ServingCommandTypes { get; }

        internal abstract CommandRunner GetRunner(CommandBase cb);

        internal TestData TestDataInstance;

        /// <summary>
        /// Reference to test data
        /// </summary>
        protected TestData TestData
        {
            get { return TestDataInstance; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void Save();

        protected abstract Task SaveAsync();

    }
}
