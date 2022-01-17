using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reinforced.Tecture.Testing.Data
{
    /// <summary>
    /// Test data record for anonymous type
    /// </summary>
    public class AnonymousTestDataRecord : ITestDataRecord<Dictionary<string, object>>
    {
        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public string Hash { get; set; }

        /// <inheritdoc />
        public Type RecordType => typeof(Dictionary<string, object>);

        /// <inheritdoc />
        public object Payload => Data;

        /// <inheritdoc />
        public Dictionary<string, object> Data { get; }

        private const BindingFlags CtorFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        
        /// <summary>
        /// Make instance of anonymous type based on dictionary of saved properties
        /// </summary>
        /// <typeparam name="T">Anonymous type</typeparam>
        /// <returns>Anonymous type instance</returns>
        public T OfType<T>()
        {
            return OfType<T>(Data);
        }
        
        /// <summary>
        /// Make instance of anonymous type based on dictionary of saved properties
        /// </summary>
        /// <typeparam name="T">Anonymous type</typeparam>
        /// <returns>Anonymous type instance</returns>
        public static T OfType<T>(Dictionary<string, object> data)
        {
            if (data == null) return default(T);
            
            var anonymousType = typeof(T);
            var ctor = anonymousType.GetConstructors(CtorFlags).FirstOrDefault();
            if (ctor == null)
                throw new Exception("Anonymous type implemenatation in .NET has been unexpectidly changed");

            var parameters = ctor.GetParameters().OrderBy(x => x.Position).Select(x => data[x.Name]).ToArray();
            var result = ctor.Invoke(parameters);

            return (T)result;
        }
        
        /// <summary>
        /// Make instance of anonymous type based on dictionary of saved properties
        /// </summary>
        /// <returns>Anonymous type instance</returns>
        public static object OfType(Dictionary<string, object> data, Type anonymousType)
        {
            if (data == null) return null;
            var ctor = anonymousType.GetConstructors(CtorFlags).FirstOrDefault();
            if (ctor == null)
                throw new Exception("Anonymous type implemenatation in .NET has been unexpectidly changed");

            var parameters = ctor.GetParameters().OrderBy(x => x.Position).Select(x => data[x.Name]).ToArray();
            var result = ctor.Invoke(parameters);

            return result;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public AnonymousTestDataRecord(Dictionary<string, object> data)
        {
            Data = data;
        }
    }
}