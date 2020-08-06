using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection.Strategies;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection
{
    public class CollectionStrategies
    {
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
