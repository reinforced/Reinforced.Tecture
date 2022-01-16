using System;
using System.Collections.Generic;
using System.Linq;

namespace Reinforced.Tecture.Testing.Data
{
    public class AnonymousCollectionTestDataRecord : ITestDataRecord<IEnumerable<Dictionary<string,object>>>
    {
        public AnonymousCollectionTestDataRecord(IEnumerable<Dictionary<string, object>> data)
        {
            Data = data;
        }

        public string Description { get; set; }
        public string Hash { get; set; }
        public Type RecordType => typeof(IEnumerable<Dictionary<string, object>>);
        public object Payload => Data;
        public IEnumerable<Dictionary<string, object>> Data { get; }

        public T OfType<T>()
        {
            var colType = typeof(T);
            var elementType = typeof(T).ElementType();
            var objs = Data.Select(x => AnonymousTestDataRecord.OfType(x, elementType))
                .ToArray();
            var arr = Array.CreateInstance(elementType, objs.Length);
            for (int i = 0; i < objs.Length; i++)
            {
                arr.SetValue(objs[i],i);
            }
            if (colType.IsArray)
                return (T)(object)arr;

            if (colType.IsInterface && colType.Name == "IEnumerable`1")
            {
                return (T)(object)arr;
            }

            var enumerableOfElement = typeof(IEnumerable<>).MakeGenericType(elementType);

            // in case if collection has constructor of enumerable
            if (typeof(T).GetConstructors()
                .Any(x => x.GetParameters().Length == 1 && x.GetParameters()[0].ParameterType == enumerableOfElement))
                return (T)Activator.CreateInstance(typeof(T), new object[] { arr });
            
            // if no one, then we can add elements one by one
            var addMethod = typeof(T).GetMethods().FirstOrDefault(x =>
                x.Name == "Add" && x.GetParameters().Length == 1 && x.GetParameters()[0].ParameterType == elementType);
            if (addMethod!=null)
            {
                var result = Activator.CreateInstance<T>();
                for (int i = 0; i < arr.Length; i++)
                {
                    addMethod.Invoke(result, new[] { arr.GetValue(i) });
                }

                return result;
            }

            throw new TectureException($"Cannot turn collection of anonymous types into {typeof(T).FullName}");
        }
    }
}