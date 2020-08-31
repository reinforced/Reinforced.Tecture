using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Reinforced.Tecture.Features.SqlStroke.Infrastructure
{
    class FakeMapper : IMapper
    {
        public string GetTableName(Type t)
        {
            return t.Name;
        }

        /// <summary>
        /// Obtains column name mapped to DB
        /// </summary>
        /// <param name="t">Entity type</param>
        /// <param name="property">Property that is needed to obtain</param>
        /// <returns></returns>
        public string GetColumnName(Type t, PropertyInfo property)
        {
            return property.Name;
        }

        /// <summary>
        /// Checks whether type is actually type of entity
        /// </summary>
        /// <param name="t">Type of suspect to entity</param>
        /// <returns>True if this type represents entity in current context</returns>
        public bool IsEntityType(Type t)
        {
            if (t.IsValueType) return false;
            if (t.IsEnum) return false;
            if (t.Namespace == "System" || t.Namespace.StartsWith("System.")) return false;
            return true;
        }

        public IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn)
        {
            yield return new AssociationFields() { From = $"$PK$", To = $"${sourceColumn.Name}_FK$" };
        }
    }
}
