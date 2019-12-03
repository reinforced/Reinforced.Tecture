using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using Reinforced.Storage.Adapters;

namespace Reinforced.Storage.Defaults.EntityFramework.EntitiesMapper
{
    /// <summary>
    /// Mapper that helps to determine table/column name by type and property using EF's DbContext
    /// </summary>
    internal static class EntitiesMapper
    {
        private static readonly Dictionary<Type, Dictionary<Type, MappingCacheEntry>> _tableNamesCache = new Dictionary<Type, Dictionary<Type, MappingCacheEntry>>();
        private static readonly object _locker = new object();
        private static readonly object _locker2 = new object();
        private static readonly Dictionary<Type, HashSet<Type>> _entityTypesByContext = new Dictionary<Type, HashSet<Type>>();
        private static MappingCacheEntry GetCachedMapping(Type contextType, Type t)
        {
            if (!_tableNamesCache.ContainsKey(contextType))
            {
                lock (_locker)
                {
                    if (!_tableNamesCache.ContainsKey(contextType))
                    {
                        _tableNamesCache[contextType] = new Dictionary<Type, MappingCacheEntry>();
                    }
                }
            }

            var cached = _tableNamesCache[contextType];

            if (!cached.ContainsKey(t))
            {
                lock (_locker2)
                {
                    if (cached.ContainsKey(t)) return cached[t];
                    cached[t] = new MappingCacheEntry();
                }
            }
            return cached[t];
        }

        private static EntityTypeMapping GetMapping(DbContext context, Type t)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            var metadata = objectContext.MetadataWorkspace;
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));
           
            var entityType = metadata.GetItems<EntityType>(DataSpace.OSpace)
                .FirstOrDefault(x => objectItemCollection.GetClrType(x) == t);
            if (entityType == null) return null;
            string name = entityType.Name;
            bool isDerived = false;
            if (entityType.BaseType != null) // alert
            {
                name = entityType.BaseType.Name;
                isDerived = true;
            }

            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .FirstOrDefault(s => s.ElementType.Name == name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                .Single()
                .EntitySetMappings
                .Single(s => s.EntitySet == entitySet);

            if (isDerived || mapping.EntityTypeMappings.Count > 1)
            {
                var m = mapping.EntityTypeMappings.FirstOrDefault(x => x.EntityType != null && x.EntityType.Name == entityType.Name);
                if (m == null) return mapping.EntityTypeMappings.FirstOrDefault(x => x.EntityType == null);
                else return m;
            }
            return mapping.EntityTypeMappings.Single();
        }

        /// <summary>
        /// Retrieves table name by type of particular context
        /// </summary>
        /// <param name="context">EF DbContext</param>
        /// <param name="t">Mapped type</param>
        /// <returns>Table name</returns>
        public static string GetTableName<T>(T context, Type t) where T : DbContext
        {
            var cm = GetCachedMapping(typeof(T), t);
            var tName = cm.GetTableName(t);
            if (string.IsNullOrEmpty(tName))
            {
                tName = cm.GetTableName(t, GetMapping(context, t));
            }
            return tName;
        }

        /// <summary>
        /// Retrieves DB column name for Type's property
        /// </summary>
        /// <param name="context">EF DbContext</param>
        /// <param name="t">Mapped type</param>
        /// <param name="propertyName"></param>
        /// <returns>Column name</returns>
        public static string GetColumnName<T>(T context, Type t, string propertyName) where T : DbContext
        {
            var cm = GetCachedMapping(typeof(T), t);
            var fName = cm.GetFieldName(t, propertyName);
            if (string.IsNullOrEmpty(fName))
            {
                fName = cm.GetFieldName(t, propertyName, GetMapping(context, t));
            }
            return fName;
        }

        private static void BuildEntityTypebyContext<T>(T context) where T : DbContext
        {
            if (!_entityTypesByContext.ContainsKey(typeof(T)))
            {
                lock (_entityTypesByContext)
                {
                    if (!_entityTypesByContext.ContainsKey(typeof(T)))
                    {
                        var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                        var metadata = objectContext.MetadataWorkspace;
                        var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));
                        var entityTypes = new HashSet<Type>(metadata.GetItems<EntityType>(DataSpace.OSpace)
                            .Where(d => d.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                            .Select(x => objectItemCollection.GetClrType(x)));

                        _entityTypesByContext[typeof(T)] = entityTypes;
                    }
                }
            }
        }

        public static bool IsEntityType<T>(T context, Type t) where T : DbContext
        {
            BuildEntityTypebyContext(context);
            return _entityTypesByContext[typeof(T)].Contains(t);
        }


        private static readonly Dictionary<AssociationCacheKey, IEnumerable<AssociationFields>> _joinsCache = new Dictionary<AssociationCacheKey, IEnumerable<AssociationFields>>();

        struct AssociationCacheKey
        {
            public Type Type;
            public PropertyInfo Pi;

            public bool Equals(AssociationCacheKey other)
            {
                return Equals(Type, other.Type) && Equals(Pi, other.Pi);
            }

            /// <summary>Indicates whether this instance and a specified object are equal.</summary>
            /// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
            /// <param name="obj">The object to compare with the current instance. </param>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is AssociationCacheKey && Equals((AssociationCacheKey)obj);
            }

            /// <summary>Returns the hash code for this instance.</summary>
            /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Type != null ? Type.GetHashCode() : 0) * 397) ^ (Pi != null ? Pi.GetHashCode() : 0);
                }
            }

            /// <summary>Returns a value that indicates whether the values of two <see cref="T:Reinforced.Storage.Defaults.EntityFramework.EntitiesMapper.EntitiesMapper.AssociationCacheKey" /> objects are equal.</summary>
            /// <param name="left">The first value to compare.</param>
            /// <param name="right">The second value to compare.</param>
            /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
            public static bool operator ==(AssociationCacheKey left, AssociationCacheKey right)
            {
                return left.Equals(right);
            }

            /// <summary>Returns a value that indicates whether two <see cref="T:Reinforced.Storage.Defaults.EntityFramework.EntitiesMapper.EntitiesMapper.AssociationCacheKey" /> objects have different values.</summary>
            /// <param name="left">The first value to compare.</param>
            /// <param name="right">The second value to compare.</param>
            /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
            public static bool operator !=(AssociationCacheKey left, AssociationCacheKey right)
            {
                return !left.Equals(right);
            }
        }

        public static IEnumerable<AssociationFields> GetJoinKeys<T>(T context, Type sourceEntity, PropertyInfo sourceColumn) where T : DbContext
        {
            var key = new AssociationCacheKey()
            {
                Pi = sourceColumn,
                Type = sourceEntity
            };

            if (!_joinsCache.ContainsKey(key))
            {
                lock (_joinsCache)
                {
                    if (!_joinsCache.ContainsKey(key))
                    {
                        var val = GetJoinKeysInner(context, sourceEntity, sourceColumn);
                        _joinsCache[key] = val;
                    }
                }
            }

            return _joinsCache[key];
        }

        private static IEnumerable<AssociationFields> GetJoinKeysInner<T>(T context, Type sourceEntity, PropertyInfo sourceColumn) where T : DbContext
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            var metadata = objectContext.MetadataWorkspace;

            var result = new List<AssociationFields>();
            var sourceMap = GetMapping(context, sourceEntity);
            if (sourceMap == null) return result;
            var navProp = sourceMap.EntityType.NavigationProperties.FirstOrDefault(d => d.Name == sourceColumn.Name);
            
            if (navProp != null)
            {
                var frm = navProp.FromEndMember;
                var to = navProp.ToEndMember;
                var assoc = metadata
                    .GetItems<AssociationType>(DataSpace.CSpace)
                    .SingleOrDefault(d => d.AssociationEndMembers.Contains(frm) && d.AssociationEndMembers.Contains(to));

                var entitySet = metadata
                    .GetItems<EntityContainer>(DataSpace.CSpace)
                    .Single()
                    .AssociationSets
                    .FirstOrDefault(s => s.ElementType == assoc);

                var sAssoc = metadata
                    .GetItems<AssociationType>(DataSpace.SSpace)
                    .FirstOrDefault(d => d.Name == entitySet.Name);

                if (sAssoc != null)
                {
                    var cnstrnt = sAssoc.Constraint;

                    for (int i = 0; i < cnstrnt.FromProperties.Count; i++)
                    {
                        var fromProp = cnstrnt.FromProperties[i];
                        var toProp = cnstrnt.ToProperties[i];
                        result.Add(new AssociationFields()
                        {
                            From = fromProp.Name,
                            To = toProp.Name,
                        });
                    }
                }

            }

            return result;
        }
    }
}
