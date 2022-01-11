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
            try
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
                    if (promise is NotifyCompleted<T> nc) nc.Fulfill(description?.Invoke());
                }

                if (promise is Demanding<T> d)
                {
                    var hashString = hashFactory();
                    d.Fulfill(result, result, hashString, description?.Invoke());
                }

                return result;
            }
            catch (Exception ex)
            {
                if (promise is Catching<T> d)
                {
                    d.Fulfill(ex, description?.Invoke());
                }

                throw;
            }
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
            try
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
                    if (promise is NotifyCompleted<T> nc) nc.Fulfill(description?.Invoke());
                }

                if (promise is Demanding<T> d)
                {
                    var hashString = hashFactory();
                    if (clone != null)
                    {
                        d.Fulfill(result, clone(result), hashString, description?.Invoke());
                    }
                    else
                    {
                        d.Fulfill(result, hashString, description?.Invoke());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                if (promise is Catching<T> d)
                {
                    d.Fulfill(ex, description?.Invoke());
                }

                throw;
            }
        }
    }
}