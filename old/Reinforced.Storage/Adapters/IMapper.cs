using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reinforced.Storage.Adapters
{
    public interface IMapper
    {
        string GetTableName(Type t);

        string GetColumnName(Type t, string propertyName);

        bool IsEntityType(Type t);

        IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn);
    }

    public class AssociationFields
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}