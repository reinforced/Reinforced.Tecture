using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Services
{
    /// <summary>
    /// Storage services that touches 1 entity
    /// </summary> 
    public class TectureService : TectureServiceBase
    {
        /// <summary>
        /// Gets reading end of channel <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Channel to obtain read end of</typeparam>
        /// <returns>Channel's read end</returns>
        protected Read<T> From<T>() where T : CanQuery
        {
            return new SRead<T>(ChannelMultiplexer);
        }

        /// <summary>
        /// Gets writing end of channel <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Channel to obtain write end of</typeparam>
        /// <returns>Channel's write end</returns>
        protected Write<T> To<T>() where T : CanCommand
        {
            return new SWrite<T>(ChannelMultiplexer, Pipeline);
        }

    }

    /// <summary>
    /// Storage services that touches 1 entity
    /// </summary> 
    public class TectureService<T1> : TectureServiceBase
        where T1 : class
    {
        /// <summary>
        /// Gets reading end of channel <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Channel to obtain read end of</typeparam>
        /// <returns>Channel's read end</returns>
        protected Read<T, T1> From<T>() where T : CanQuery
        {
            return new SRead<T, T1>(ChannelMultiplexer);
        }

        /// <summary>
        /// Gets writing end of channel <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Channel to obtain write end of</typeparam>
        /// <returns>Channel's write end</returns>
        protected Write<T, T1> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1>(ChannelMultiplexer, Pipeline);
        }

    }

}
