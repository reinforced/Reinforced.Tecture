using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection.Strategies;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection
{
    /// <summary>
    /// Collection generation strategies provider
    /// </summary>
    public class CollectionStrategies
    {
        /// <summary>
        /// Gets whether particular type has particular method
        /// </summary>
        /// <param name="t">Type</param>
        /// <param name="name">Method name</param>
        /// <returns>True when type <paramref name="t"/> has method <paramref name="name"/>, false otherwise</returns>
        protected virtual bool HasMethod(Type t, string name)
        {
            try
            {
                var m = t.GetMethod(name);
                return m != null;
            }
            catch (AmbiguousMatchException)
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Obtains generation strategy for tuple
        /// </summary>
        /// <param name="tupleTypes">Set of types that tuple consists of</param>
        /// <returns>Generation strategy for tuple</returns>
        public virtual ICollectionCreationStrategy GetTupleStrategy(IEnumerable<Type> tupleTypes)
        {
            return new TupleCreationStrategy();
        }
             
        /// <summary>
        /// Obtains generation strategy for collection
        /// </summary>
        /// <param name="collectionType">Collection type</param>
        /// <returns>Generation strategy of collection <paramref name="collectionType"/></returns>
        public virtual ICollectionCreationStrategy GetStrategy(Type collectionType)
        {
            if (collectionType.IsArray)
            {
                var ar = collectionType.GetElementType();
                return new ArrayCreationStrategy(ar);
            }

            if (collectionType.IsEnumerable() && HasMethod(collectionType, "Add"))
            {
                return new ObjectInitializerStrategy(collectionType);
            }
            return new ConstructorOfArrayStrategy(collectionType, collectionType.GetGenericArguments()[0]);
        }
    }
}
