using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reinforced.Tecture.Features.SqlStroke.Infrastructure
{
    public interface IMapper
    {
        /// <summary>
        /// Returns table name by entity type
        /// </summary>
        /// <param name="t">Entity type</param>
        /// <returns>Table name</returns>
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

        /// <summary>
        /// Retrieves set of source column - target column pairs for joining tables
        ///
        /// Example: sourceEntity = User, sourceColumn = Order
        /// Expected result: {From = OrderId,           To = Id}
        ///                     ^- from "users" table    ^- from "orders" table
        /// </summary>
        /// <param name="sourceEntity">Type of source entity</param>
        /// <param name="sourceColumn">PropertyInfo pointing to column with nested aggregate</param>
        /// <returns>Set of associations</returns>
        IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn);
    }

    public class AssociationFields
    {
        /// <summary>
        /// Primary key. E.g. User.Id
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Child key. E.g. Order.UserId
        /// </summary>
        public string To { get; set; }
    }
}
