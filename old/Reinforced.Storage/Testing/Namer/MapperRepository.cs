using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Storage.Adapters;

namespace Reinforced.Storage.Testing.Namer
{
    /// <summary>
    /// Namer repository to mock data context operations for SQL stroke.
    /// This class is JSON-serializable and deserializable
    /// </summary>
    public class MapperRepository : IMapper
    {
        public Dictionary<string,Dictionary<string,string>> TypesToColumnToNames { get; private set; }

        public Dictionary<string,string> TypesToTableNames { get; private set; }

        public Dictionary<string,Dictionary<string, AssociationFields[]>> TypeToColumnToAssociations { get; private set; }

        public HashSet<string> FullEntityNames { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public MapperRepository()
        {
            TypesToColumnToNames = new Dictionary<string, Dictionary<string, string>>();
            TypesToTableNames = new Dictionary<string, string>();
            TypeToColumnToAssociations = new Dictionary<string, Dictionary<string, AssociationFields[]>>();
            FullEntityNames = new HashSet<string>();
        }

        public string GetTableName(Type t)
        {
            if (!TypesToTableNames.ContainsKey(t.FullName))
            {
                Console.WriteLine($"No mapping between {t} and corresponding table. Maybe namer repository needs update?");
                return null;
            }

            return TypesToTableNames[t.FullName];
        }

        public string GetColumnName(Type t, string propertyName)
        {
            if (!TypesToColumnToNames.ContainsKey(t.FullName))
                throw new Exception($"No column names for {t}. Maybe namer repository needs update?");
            var ttc = TypesToColumnToNames[t.FullName];
            if (!ttc.ContainsKey(propertyName))
                throw new Exception($"No column name for property {propertyName} of {t}. Maybe namer repository needs update?");
            return ttc[propertyName];
        }

        public bool IsEntityType(Type t)
        {
            return FullEntityNames.Contains(t.FullName);
        }

        public IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn)
        {
            if (!TypeToColumnToAssociations.ContainsKey(sourceEntity.FullName))
                throw new Exception($"No associations mapping for {sourceEntity}. Maybe namer repository needs update?");
            var assoc = TypeToColumnToAssociations[sourceEntity.FullName];
            if (!assoc.ContainsKey(sourceColumn.Name))
                throw new Exception($"No associations for column {sourceColumn} of {sourceEntity}. Maybe namer repository needs update?");

            return assoc[sourceColumn.Name];
        }
    }
}
