using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    public class TableReference
    {
        public string Alias { get; set; }
        
        public Type EntityType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal TableReference(Type entityType)
        {
            EntityType = entityType;
            Children = new List<TableReference>();
        }

        public List<TableReference> Children { get; private set; }
    }

    public class NestedTableReference : TableReference
    {
        public TableReference Parent { get; set; }
        
        public PropertyInfo JoinColumn { get; set; }

        public Join? JoinOveride { get; set; }

        public bool IsDerivedJoin { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal NestedTableReference(Type entityType) : base(entityType)
        {
           
        }

    }
}