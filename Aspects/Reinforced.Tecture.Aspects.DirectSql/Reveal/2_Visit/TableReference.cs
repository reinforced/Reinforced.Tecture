using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Tecture.Aspects.DirectSql;
using Reinforced.Tecture.Aspects.DirectSql;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit
{
    /// <summary>
    /// Table reference from SQL Expression
    /// </summary>
    public class TableReference
    {
        /// <summary>
        /// Table alias (from expression)
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// Entity type
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal TableReference(Type entityType, string alias)
        {
            EntityType = entityType;
            Children = new List<NestedTableReference>();
            Alias = alias;
        }

        /// <summary>
        /// Child tables also deduced from this table reference
        /// </summary>
        public List<NestedTableReference> Children { get; private set; }
    }

    /// <summary>
    /// Nested (automatic) table reference
    /// </summary>
    public class NestedTableReference : TableReference
    {
        /// <summary>
        /// Reference to parent table
        /// </summary>
        public TableReference Parent { get; }

        /// <summary>
        /// Column that join happens by
        /// </summary>
        public PropertyInfo JoinColumn { get; }

        /// <summary>
        /// Join type override (optional)
        /// </summary>
        public Join? JoinOveride { get; set; }


        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal NestedTableReference(Type entityType, string alias, PropertyInfo joinColumn, TableReference parent) : base(entityType, alias)
        {
            JoinColumn = joinColumn;
            Parent = parent;
            parent.Children.Add(this);
        }
    }
}