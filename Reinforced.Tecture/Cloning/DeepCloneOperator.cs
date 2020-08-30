using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Reinforced.Tecture.Cloning
{
    struct LateBoundEntry
    {
        public object Source { get; set; }
        public object Target { get; set; }
        public Delegate LateBound { get; set; }
    }

    class DeepCloneOperator
    {
        private readonly Dictionary<Type, TypeCloneTooling> _cloneDelegates = new Dictionary<Type, TypeCloneTooling>();

        private readonly Queue<object> _instanceQueue = new Queue<object>();
        private readonly Queue<LateBoundEntry> _lateBoundQueue = new Queue<LateBoundEntry>();

        private readonly Dictionary<object, object> _alreadyCloned = new Dictionary<object, object>();

        private static readonly MethodInfo ProduceColletionCloneMethod;
        private static readonly MethodInfo ProduceDictionaryCloneMethod;

        internal static readonly MethodInfo DeferCloneMethod;
        internal static readonly MethodInfo DeferBindMethod;
        internal static readonly MethodInfo ResolveMethod;
        internal static readonly MethodInfo CloneAndDeferMethod;
        static DeepCloneOperator()
        {
            ProduceColletionCloneMethod = typeof(DeepCloneOperator).GetMethod(nameof(ProduceCollectionClone),
                BindingFlags.Instance | BindingFlags.NonPublic);
            ProduceDictionaryCloneMethod = typeof(DeepCloneOperator).GetMethod(nameof(ProduceDictionaryClone),
                BindingFlags.Instance | BindingFlags.NonPublic);

            DeferCloneMethod =
                typeof(DeepCloneOperator).GetMethod(nameof(DeferClone), BindingFlags.NonPublic | BindingFlags.Instance);

            DeferBindMethod =
                typeof(DeepCloneOperator).GetMethod(nameof(DeferBind), BindingFlags.NonPublic | BindingFlags.Instance);
            ResolveMethod =
                typeof(DeepCloneOperator).GetMethod(nameof(Resolve), BindingFlags.NonPublic | BindingFlags.Instance);
            CloneAndDeferMethod =
                typeof(DeepCloneOperator).GetMethod(nameof(CloneAndDeferMethod), BindingFlags.NonPublic | BindingFlags.Instance);

        }

        internal T Resolve<T>(T original)
        {
            if (!_alreadyCloned.ContainsKey(original))
                throw new Exception("Cloning queue is broken");
            return (T)_alreadyCloned[original];
        }

        internal void DeferClone(object o)
        {
            if (o == null) throw new ArgumentNullException(nameof(o));
            _instanceQueue.Enqueue(o);
        }

        internal void DeferBind(Type valType, object source, object target)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (valType.IsDictionary() || valType.IsCollection()) return;
            if (!_cloneDelegates.ContainsKey(valType))
            {
                _cloneDelegates[valType] = CloningDelegateEmitter.EmitCloneDelegate(valType);
            }

            var tooling = _cloneDelegates[valType];
            if (tooling.Bind != null)
            {
                _lateBoundQueue.Enqueue(new LateBoundEntry()
                {
                    LateBound = tooling.Bind,
                    Source = source,
                    Target = target
                });
            }
        }

        private void Proceed()
        {
            while (_instanceQueue.Count > 0)
            {
                var obj = _instanceQueue.Dequeue();
                if (obj != null)
                {
                    var result = Clone(obj);
                    if (!_alreadyCloned.ContainsKey(obj))
                    {
                        _alreadyCloned[obj] = result;
                    }
                }
            }

            while (_lateBoundQueue.Count > 0)
            {
                var item = _lateBoundQueue.Dequeue();
                item.LateBound.DynamicInvoke(item.Source, item.Target, this);
            }
        }

        public DeepCloneOperator(Dictionary<Type, TypeCloneTooling> cloneDelegates)
        {
            _cloneDelegates = cloneDelegates;
        }

        public object MakeClone(object original)
        {
            if (original == null) return null;
            _instanceQueue.Enqueue(original);
            Proceed();
            return _alreadyCloned[original];
        }

        private object Clone(object original)
        {
            if (original == null) return null;
            return Clone(original.GetType(), original);
        }

        internal object CloneAndDefer(object original)
        {
            if (original == null) return null;
            var def = Clone(original.GetType(), original);
            var t = original.GetType();
            if (!t.IsInlineCloning()) DeferBind(t,original,def);
            return def;
        }

        private object Clone(Type t, object original)
        {
            if (original == null) return null;
            if (_alreadyCloned.ContainsKey(original)) return _alreadyCloned[original];

            if (original is IDictionary dic) return CloneDictionary(dic);
            if (original is IEnumerable coll) return CloneCollection(coll);
            if (original is ICloneable clon) return clon.Clone();

            if (!_cloneDelegates.ContainsKey(t))
            {
                _cloneDelegates[t] = CloningDelegateEmitter.EmitCloneDelegate(t);
            }

            var del = _cloneDelegates[t];
            return del.Shallow.DynamicInvoke(original, this);
        }

        private IEnumerable<T> ProduceCollectionClone<T>(IEnumerable<T> source)
        {
            List<T> result = new List<T>();
            foreach (var src in source)
            {
                if (src == null) result.Add(src);
                else
                {
                    result.Add((T)Clone(src));
                }
            }

            return result;
        }

        public object CloneCollectionData(IEnumerable coll)
        {
            var elementType = coll.GetType().GetCollectionElementType();
            var g = ProduceColletionCloneMethod.MakeGenericMethod(elementType);
            var enumerable = g.Invoke(this, new object[] { coll });
            return enumerable;
        }

        public object CloneCollection(IEnumerable coll)
        {
            var collType = coll.GetType();
            if (!_cloneDelegates.ContainsKey(collType))
            {
                _cloneDelegates[collType] = CloningDelegateEmitter.EmitCollectionCloningDelegate(collType);
            }

            var del = _cloneDelegates[collType];
            var clones = CloneCollectionData(coll);
            return del.Shallow.DynamicInvoke(clones);
        }

        private IDictionary<K, V> ProduceDictionaryClone<K, V>(IDictionary<K, V> dct)
        {
            Dictionary<K, V> result = new Dictionary<K, V>();
            foreach (var v in dct)
            {
                var keyClone = Clone(typeof(K), v.Key);
                var valueClone = v.Value == null ? null : Clone(typeof(V), v.Value);
                result.Add((K)keyClone, (V)valueClone);
            }

            return result;
        }

        public object CloneDictionaryData(IDictionary dic)
        {
            var (keyType, valueType) = dic.GetType().GetDictionaryParameters();
            var g = ProduceDictionaryCloneMethod.MakeGenericMethod(keyType, valueType);
            var dictionary = g.Invoke(this, new object[] { dic });
            return dictionary;
        }

        public object CloneDictionary(IDictionary dic)
        {
            var dicType = dic.GetType();
            if (!_cloneDelegates.ContainsKey(dicType))
            {
                _cloneDelegates[dicType] = CloningDelegateEmitter.EmitDictionaryCloningDelegate(dicType);
            }

            var del = _cloneDelegates[dicType];
            var clones = CloneDictionaryData(dic);
            return del.Shallow.DynamicInvoke(clones);
        }


    }
}
