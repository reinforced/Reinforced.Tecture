using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Storage.Adapters;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    public partial class EntityFrameworkDbContextAdapter
    {

        public string GetTableName(Type t)
        {
            return EntitiesMapper.EntitiesMapper.GetTableName(_dataContext, t);
        }

        public string GetColumnName(Type t, string propertyName)
        {
            return EntitiesMapper.EntitiesMapper.GetColumnName(_dataContext, t, propertyName);
        }

        public bool IsEntityType(Type t)
        {
            return EntitiesMapper.EntitiesMapper.IsEntityType(_dataContext, t);
        }

        public IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn)
        {
            return EntitiesMapper.EntitiesMapper.GetJoinKeys(_dataContext, sourceEntity, sourceColumn);
        }
    }
}
