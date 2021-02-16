using Reinforced.Tecture.Channels;

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
        /// Gets reading end of channel <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Channel to obtain read end of</typeparam>
        /// <returns>Channel's read end</returns>
        protected Read<T> In<T>() where T : CanQuery
        {
            return From<T>();
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
    /// Storage services that does not change a thing, but only communicating reads from several channels
    /// </summary> 
    public class ReadonlyTectureService : TectureServiceBase
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
        /// Gets reading end of channel <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Channel to obtain read end of</typeparam>
        /// <returns>Channel's read end</returns>
        protected Read<T> In<T>() where T : CanQuery
        {
            return From<T>();
        }
    }
}
