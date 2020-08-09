using System.Collections.Generic;

namespace Reinforced.Storage.Defaults.EntityFramework.EntitiesMapper
{
    /// <summary>
    /// Cache entry that contains Type-Table mapping and particular fields mappings
    /// </summary>
    internal class TypeCacheEntry
    {
        /// <summary>
        /// Table name that type maps to
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Property name - DB field name mappings
        /// </summary>
        public Dictionary<string, string> FieldNames { get; set; }

        /// <summary>
        /// Locker object
        /// </summary>
        public readonly object _typeEntitiesLocker = new object();

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TypeCacheEntry()
        {
            FieldNames = new Dictionary<string, string>();
        }
    }
}
