using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal
{
    partial class StrokeProcessor
    {
        private readonly Dictionary<Type, string> _temporaryTypeMaps = new Dictionary<Type, string>();
        public void RegisterTemporaryTypeMap(Type t, string tableName)
        {
            _temporaryTypeMaps[t] = tableName;
        }

        public void RemoveTemporaryTypeMap(Type t)
        {
            _temporaryTypeMaps.Remove(t);
        }

        public string GetTableName(Type t)
        {
            if (_temporaryTypeMaps.ContainsKey(t)) return _temporaryTypeMaps[t];
            return _mapper.GetTableName(t);
        }

        public string GetColumnName(Type t, string propertyName)
        {
            if (_temporaryTypeMaps.ContainsKey(t)) return propertyName;
            return _mapper.GetColumnName(t, propertyName);
        }

        public bool IsEntityType(Type t)
        {
            if (_temporaryTypeMaps.ContainsKey(t)) return true;
            return _mapper.IsEntityType(t);
        }

        public IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn)
        {
            return _mapper.GetJoinKeys(sourceEntity, sourceColumn);
        }
    }
}
