using System.Linq;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing.Validation
{
    /// <summary>
    /// Base class for validation the state of the command queue
    /// </summary>
    public abstract class ValidationBase
    {
        protected readonly IAssert Assert = new AssertObj();

        /// <summary>
        /// Safe type cast with correct assertion
        /// </summary>
        /// <param name="obj">Object to cast</param>
        /// <typeparam name="T">Target type</typeparam>
        /// <returns>Casted object</returns>
        /// <exception cref="TectureValidationException"></exception>
        protected T As<T>(object obj)
        {
            if (obj == null) return default(T);
            if (obj is T to) return to;
            throw new TectureValidationException(
                $"Object {obj} expected to be of type {typeof(T)}, but actually it is {obj.GetType()}");
        }

        /// <summary>
        /// Obtains property of the anonymous type
        /// </summary>
        /// <param name="obj">Anonymous type object</param>
        /// <param name="propertyName">Property name</param>
        /// <typeparam name="T">Property type</typeparam>
        /// <returns>Casted property</returns>
        protected T Prop<T>(object obj, string propertyName)
        {
            if (obj == null)
                throw new TectureValidationException($"Null is not expected here");

            var property = obj
                .GetType()
                .GetProperties()
                .FirstOrDefault(x => x.Name == propertyName && typeof(T).IsAssignableFrom(x.PropertyType));

            var value = property.GetValue(obj);

            if (value is T v) return v;
            throw new TectureValidationException($"Property {propertyName} expected to be of type {typeof(T)}, but actually is {(value==null?"null":value.GetType().FullName)}");
        }
        
        /// <summary>
        /// Validates instance of Tecture root. 
        /// Requires started tract on this instance
        /// </summary>
        /// <param name="tecture">Tecture instance</param>
        public void Validate(ITecture tecture)
        {
            var trace = tecture.EndTrace();
            Validate(trace);
        }

        /// <summary>
        /// Validates collected trace
        /// </summary>
        /// <param name="trace">Trace object to validate</param>
        public void Validate(Trace trace)
        {
            var validator = trace.Begins();
            Validate(validator);
        }

        /// <summary>
        /// Performs exact validation calls
        /// </summary>
        /// <param name="flow">Validation flow</param>
        protected abstract void Validate(TraceValidator flow);
    }
}
