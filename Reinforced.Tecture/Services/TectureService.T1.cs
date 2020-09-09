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
    /// Storage services with tooling
    /// </summary> 
    public class TectureService<Tool> : TectureServiceBase
        where Tool : Tooling
    {
        /// <summary>
        /// Gets reading end of channel <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Channel to obtain read end of</typeparam>
        /// <returns>Channel's read end</returns>
        protected Read<T, Tool> From<T>() where T : CanQuery
        {
            return new SRead<T, Tool>(ChannelMultiplexer);
        }

        /// <summary>
        /// Gets writing end of channel <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Channel to obtain write end of</typeparam>
        /// <returns>Channel's write end</returns>
        protected Write<T, Tool> To<T>() where T : CanCommand
        {
            return new SWrite<T, Tool>(ChannelMultiplexer, Pipeline);
        }

    }

}
