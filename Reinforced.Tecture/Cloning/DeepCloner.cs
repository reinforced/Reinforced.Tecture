using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Reinforced.Tecture.Cloning
{
    /// <summary>
    /// Access point to Tecture's deep cloner
    /// </summary>
    public static class DeepCloner
    {

        private static readonly Dictionary<Type, TypeCloneTooling> _cloneDelegates = new Dictionary<Type, TypeCloneTooling>();

        /// <summary>
        /// Registers personal clone delegate for particular type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cloningDelegate"></param>
        public static void RegisterCloner<T>(Func<T, T> cloningDelegate)
        {
            T Fn(T i, DeepCloneOperator _) => cloningDelegate(i);

            _cloneDelegates[typeof(T)] = new TypeCloneTooling()
            {
                Shallow = (Func<T, DeepCloneOperator, T>)Fn
            };
        }

        /// <summary>
        /// Makes deep clone of object with all the nested objects and collections
        /// </summary>
        /// <param name="original">Original object</param>
        /// <returns>Object clone</returns>
        public static object DeepClone(this object original)
        {
            if (original == null) return null;
            if (original.GetType().IsInlineCloning()) return original;
            if (original is ICloneable clon) return clon.Clone();

            var dco = new DeepCloneOperator(_cloneDelegates);
            return dco.MakeClone(original);
        }

        /// <summary>
        /// Makes deep clone of object with all the nested objects and collections
        /// </summary>
        /// <param name="original">Original object</param>
        /// <returns>Object clone</returns>
        public static T DeepClone<T>(this T original)
        {
            if (original == null) return original;
            if (typeof(T).IsInlineCloning()) return original;
            if (original is ICloneable clon) return (T)clon.Clone();

            var dco = new DeepCloneOperator(_cloneDelegates);
            return (T)dco.MakeClone(original);
        }
    }
}
