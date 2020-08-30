using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Tecture.Testing.Data
{
    public class AnonymousTestDataRecord : ITestDataRecord<Dictionary<string, object>>
    {
        public string Description { get; set; }
        public string Hash { get; set; }
        public Type RecordType
        {
            get { return typeof(Dictionary<string, object>); }
        }
        public object Payload
        {
            get { return Data; }
        }
        public Dictionary<string, object> Data { get; }

        private const BindingFlags CtorFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public T OfType<T>()
        {
            var anonymousType = typeof(T);
            var ctor = anonymousType.GetConstructors(CtorFlags).FirstOrDefault();
            var parameters = ctor.GetParameters().OrderBy(x => x.Position).Select(x => Data[x.Name]).ToArray();
            var result = ctor.Invoke(parameters);

            return (T)result;
        }

        public static AnonymousTestDataRecord FromAnonymousObject(object o, Type objectType)
        {
            if (o==null) return new AnonymousTestDataRecord(new Dictionary<string, object>());
            
            var props = objectType.GetProperties(BindingFlags.Public|BindingFlags.Instance)
                .ToDictionary(x=>x.Name,x=>x.GetValue(o));
            return new AnonymousTestDataRecord(props);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public AnonymousTestDataRecord(Dictionary<string, object> data)
        {
            Data = data;
        }
    }
}