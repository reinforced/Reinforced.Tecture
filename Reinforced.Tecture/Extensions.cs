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
    }
}
