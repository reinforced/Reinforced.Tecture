using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Services
{

    /// <summary>
    /// Storage services that touches 1 entity
    /// </summary> 
    public class TectureService<T1> : TectureService
        where T1 : class
    {
        /// <summary>
        /// Service pipeline access
        /// </summary>
        protected ServicePipeline<T1> Q { get; private set; }

        internal override void CallInit(Pipeline pipeline)
        {
            Q = new ServicePipeline<T1>(pipeline);
            Init();
        }

        internal override ServicePipeline Pipeline
        {
            get { return Q; }
        }
    }

}
