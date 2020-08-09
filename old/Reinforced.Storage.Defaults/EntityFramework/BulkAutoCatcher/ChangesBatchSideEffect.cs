using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Defaults.EntityFramework.BulkAutoCatcher
{
    internal static class BatchDictionaryExtensions
    {
        internal static HashSet<object> GetSet(this Dictionary<Type, HashSet<object>> set, object entity, ChangesBatchSideEffect adapter)
        {
            var t = adapter.NormalizeType(entity.GetType());
            return set.GetOrCreate(t);
        }

        internal static HashSet<PropertyInfo> GetSet(this Dictionary<Type, HashSet<PropertyInfo>> set, object entity, ChangesBatchSideEffect adapter)
        {
            var t = adapter.NormalizeType(entity.GetType());
            return set.GetOrCreate(t);
        }
    }

    [SideEffectCode("BATCH")]
    public class ChangesBatchSideEffect : SideEffectBase
    {
        private readonly Dictionary<Type, HashSet<object>> _added = new Dictionary<Type, HashSet<object>>();
        private readonly Dictionary<Type, HashSet<object>> _updated = new Dictionary<Type, HashSet<object>>();
        private readonly Dictionary<Type, HashSet<object>> _removed = new Dictionary<Type, HashSet<object>>();
        private readonly Dictionary<Type, HashSet<PropertyInfo>> _updatedProperties = new Dictionary<Type, HashSet<PropertyInfo>>();
        private readonly HashSet<Type> _knownTypes = new HashSet<Type>();

        public void MergeChanges(ChangesBatchSideEffect @from)
        {
            foreach (var _ in @from.Added)
            {
                foreach (var a in _.Value)
                {
                    Add(a);
                }
            }

            foreach (var _ in @from.Removed)
            {
                foreach (var r in _.Value)
                {
                    Remove(r);
                }
            }

            foreach (var _ in @from.Updated)
            {
                foreach (var u in _.Value)
                {
                    Update(u, @from.UpdatedProperties[u.SafeGetType()]);
                }
            }
        }

        public void Add(object entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (_updated.GetSet(entity, this).Contains(entity)) return;
            _added.GetSet(entity, this).AddIfNotExists(entity);
            _removed.GetSet(entity, this).Remove(entity);
            _knownTypes.AddIfNotExists(this.NormalizeType(entity.GetType()));
        }

        public void Remove(object entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _added.GetSet(entity, this).Remove(entity);
            _updated.GetSet(entity, this).Remove(entity);
            _removed.GetSet(entity, this).AddIfNotExists(entity);
            _knownTypes.AddIfNotExists(this.NormalizeType(entity.GetType()));
        }

        public void Update(object entity, IEnumerable<PropertyInfo> updatedProperties = null)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (_added.GetSet(entity, this).Contains(entity)) return;
            if (this.IsEntityNew(entity))
            {
                Add(entity);
                return;
            }

            if (updatedProperties == null)
                updatedProperties = this.NormalizeType(entity.GetType()).GetCachedProperties();

            var updatedProps = _updatedProperties.GetSet(entity, this);
            foreach (var v in updatedProperties)
            {
                updatedProps.AddIfNotExists(v);
            }
            _updated.GetSet(entity, this).AddIfNotExists(entity);
            _removed.GetSet(entity, this).Remove(entity);
            _knownTypes.AddIfNotExists(this.NormalizeType(entity.GetType()));
        }

        public Dictionary<Type, HashSet<object>> Added
        {
            get { return _added; }
        }

        public Dictionary<Type, HashSet<object>> Updated
        {
            get { return _updated; }
        }

        public Dictionary<Type, HashSet<object>> Removed
        {
            get { return _removed; }
        }

        public Dictionary<Type, HashSet<PropertyInfo>> UpdatedProperties
        {
            get { return _updatedProperties; }
        }

        public HashSet<Type> KnownTypes
        {
            get { return _knownTypes; }
        }

        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            if (string.IsNullOrEmpty(Annotation))
                tw.Write($"Changes batch with {Added.Sum(d=>d.Value.Count)} additions, {Removed.Sum(d => d.Value.Count)} removals and {Updated.Sum(d => d.Value.Count)} updates");
            else
                tw.Write($"Batch {Annotation} (A {Added.Sum(d => d.Value.Count)}/R {Removed.Sum(d => d.Value.Count)}/U {Updated.Sum(d => d.Value.Count)})");
        }

        public Type NormalizeType(Type t)
        {
            return ObjectContext.GetObjectType(t);
        }

        public bool IsEntityNew(object entity)
        {
            var e = entity as IEntity;
            if (e != null)
            {
                if (e.Id == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
