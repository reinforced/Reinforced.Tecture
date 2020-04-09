using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor
{
    internal class TableParameterReference
    {
        public string TableName { get; set; }

        public string Alias { get; set; }

        public bool IsDeclared { get; set; }

        public Type EntityType { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TableParameterReference(Type entityType)
        {
            EntityType = entityType;
            Children = new List<TableParameterReference>();
        }

        public TableParameterReference Parent { get; set; }

        public List<TableParameterReference> Children { get; private set; }

        public PropertyInfo JoinColumn { get; set; }

        public Join? JoinOveride { get; set; }

        public bool IsDerivedJoin { get; set; }
    }
}