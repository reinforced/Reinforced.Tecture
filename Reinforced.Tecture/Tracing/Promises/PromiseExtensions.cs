using System;

namespace Reinforced.Tecture.Tracing.Promises
{
    public static class PromiseExtensions
    {
        /// <summary>
        /// Resolves value-type using promise
        /// </summary>
        /// <param name="promise">Promise</param>
        /// <param name="valueFactory">Function that obtains original value</param>
        /// <param name="hashFactory">Function that obtains hash of query</param>
        /// <param name="description">Function that obtains description of query</param>
        /// <typeparam name="T">Resolved value-type</typeparam>
        /// <returns>Value-type either resolved from test data or real one</returns>
        public static T ResolveValue<T>(this Promised<T> promise, Func<T> valueFactory, Func<string> hashFactory,
            Func<string> description = null)
            where T : struct
        {
            T result;
            if (promise is Containing<T> c)
            {
                var hashString = hashFactory();
                result = c.Get(hashString, description?.Invoke());
            }
            else
            {
                result = valueFactory();
            }

            if (promise is Demanding<T> d)
            {
                var hashString = hashFactory();
                d.Fullfill(result, result, hashString, description?.Invoke());
            }

            return result;
        }

        /// <summary>
        /// Resolves reference type using promise
        /// </summary>
        /// <param name="promise">Promise</param>
        /// <param name="valueFactory">Function that obtains original value</param>
        /// <param name="hashFactory">Function that obtains hash of query</param>
        /// <param name="description">Function that obtains description of query</param>
        /// <typeparam name="T">Resolved reference-type</typeparam>
        /// <returns>Reference-type either resolved from test data or real one</returns>
        public static T ResolveReference<T>(this Promised<T> promise, Func<T> valueFactory, Func<string> hashFactory,
            Func<string> description = null, Func<T, T> clone = null)
            where T : class
        {
            T result;
            if (promise is Containing<T> c)
            {
                var hashString = hashFactory();
                result = c.Get(hashString, description?.Invoke());
            }
            else
            {
                result = valueFactory();
            }

            if (promise is Demanding<T> d)
            {
                var hashString = hashFactory();
                if (clone != null)
                {
                    d.Fullfill(result, clone(result), hashString, description?.Invoke());
                }
                else
                {
                    d.Fullfill(result, hashString, description?.Invoke());
                }
            }

            return result;
        }
    }
}