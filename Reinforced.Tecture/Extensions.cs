using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture
{
    public static class Extensions
    {
        /// <summary>
        /// Obtains instance of service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        public static T Let<T>(this ITecture t) where T : TectureServiceBase
        {
            return t.Do<T>();
        }

        /// <summary>
        /// Obtains data source to query data from
        /// </summary>
        /// <typeparam name="T">Type of data source</typeparam>
        /// <returns>Data source instance</returns>
        public static Read<T> In<T>(this ITecture t) where T : CanQuery
        {
            return t.From<T>();
        }
        
        /// <summary>
        /// Turns command time taken into human-readeable string
        /// </summary>
        /// <param name="timeTaken"></param>
        /// <returns></returns>
        public static string FormatCommandTimeTaken(this TimeSpan timeTaken)
        {
            if (timeTaken.TotalMinutes > 1)
            {
                return $"{timeTaken:mm\\:ss\\:fff}";
            }

            if (timeTaken.TotalSeconds > 5)
            {
                return $"{timeTaken:ss\\:fff} s";
            }

            if (timeTaken.TotalMilliseconds < 1)
            {
                var val = (int) (timeTaken.TotalMilliseconds * 1000);
                return $"{val:000}μs";
            }

            return $"{timeTaken:fff}ms";
        }
    }
}
