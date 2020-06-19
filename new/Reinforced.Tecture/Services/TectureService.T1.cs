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
    public class TectureService<T1> : TectureService
        where T1 : class
    {
        protected Read<T, T1> From<T>() where T : CanQuery
        {
            return new SRead<T, T1>(ChannelMultiplexer);
        }

        protected Write<T, T1> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1>(ChannelMultiplexer,Pipeline);
        }

    }

}
