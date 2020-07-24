using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public interface IMapper
    {
        string GetTableName(Type t);

        /// <summary>
        /// Obtains column name mapped to DB
        /// </summary>
        /// <param name="t">Entity type</param>
        /// <param name="property">Property that is needed to obtain</param>
        /// <returns></returns>
        string GetColumnName(Type t, PropertyInfo property);

        /// <summary>
        /// Checks whether type is actually type of entity
        /// </summary>
        /// <param name="t">Type of suspect to entity</param>
        /// <returns>True if this type represents entity in current context</returns>
        bool IsEntityType(Type t);

        IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn);
    }

    public class AssociationFields
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}
