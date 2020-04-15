using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Services {	
	
 
	

	/// <summary>
    /// Storage services that touches 2 entities
    /// </summary>    
	public class TectureService<T1, T2>
			   : TectureService<T1>
			   where T1: class
			   where T2 : class
			   
    {
		/// <summary>
        /// Service pipeline access
        /// </summary>
        protected ServicePipeline<T1, T2> Q { get; private set; }

        internal override void CallInit(Pipeline pipeline)
        {
            Q = new ServicePipeline<T1, T2>(pipeline);
            Init();
        }

        internal override ServicePipeline Pipeline
        {
            get { return Q; }
        }
    }

 
	

	/// <summary>
    /// Storage services that touches 3 entities
    /// </summary>    
	public class TectureService<T1, T2, T3>
			   : TectureService<T1, T2>
			   where T1: class
			   where T2 : class
			   where T3 : class
			   
    {
		/// <summary>
        /// Service pipeline access
        /// </summary>
        protected ServicePipeline<T1, T2, T3> Q { get; private set; }

        internal override void CallInit(Pipeline pipeline)
        {
            Q = new ServicePipeline<T1, T2, T3>(pipeline);
            Init();
        }

        internal override ServicePipeline Pipeline
        {
            get { return Q; }
        }
    }

 
	

	/// <summary>
    /// Storage services that touches 4 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4>
			   : TectureService<T1, T2, T3>
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   
    {
		/// <summary>
        /// Service pipeline access
        /// </summary>
        protected ServicePipeline<T1, T2, T3, T4> Q { get; private set; }

        internal override void CallInit(Pipeline pipeline)
        {
            Q = new ServicePipeline<T1, T2, T3, T4>(pipeline);
            Init();
        }

        internal override ServicePipeline Pipeline
        {
            get { return Q; }
        }
    }

 
	

	/// <summary>
    /// Storage services that touches 5 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4, T5>
			   : TectureService<T1, T2, T3, T4>
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   where T5 : class
			   
    {
		/// <summary>
        /// Service pipeline access
        /// </summary>
        protected ServicePipeline<T1, T2, T3, T4, T5> Q { get; private set; }

        internal override void CallInit(Pipeline pipeline)
        {
            Q = new ServicePipeline<T1, T2, T3, T4, T5>(pipeline);
            Init();
        }

        internal override ServicePipeline Pipeline
        {
            get { return Q; }
        }
    }

 
	

	/// <summary>
    /// Storage services that touches 6 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4, T5, T6>
			   : TectureService<T1, T2, T3, T4, T5>
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   where T5 : class
			   where T6 : class
			   
    {
		/// <summary>
        /// Service pipeline access
        /// </summary>
        protected ServicePipeline<T1, T2, T3, T4, T5, T6> Q { get; private set; }

        internal override void CallInit(Pipeline pipeline)
        {
            Q = new ServicePipeline<T1, T2, T3, T4, T5, T6>(pipeline);
            Init();
        }

        internal override ServicePipeline Pipeline
        {
            get { return Q; }
        }
    }

 
	

	/// <summary>
    /// Storage services that touches 7 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4, T5, T6, T7>
			   : TectureService<T1, T2, T3, T4, T5, T6>
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   where T5 : class
			   where T6 : class
			   where T7 : class
			   
    {
		/// <summary>
        /// Service pipeline access
        /// </summary>
        protected ServicePipeline<T1, T2, T3, T4, T5, T6, T7> Q { get; private set; }

        internal override void CallInit(Pipeline pipeline)
        {
            Q = new ServicePipeline<T1, T2, T3, T4, T5, T6, T7>(pipeline);
            Init();
        }

        internal override ServicePipeline Pipeline
        {
            get { return Q; }
        }
    }

 
	

	/// <summary>
    /// Storage services that touches 8 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4, T5, T6, T7, T8>
			   : TectureService<T1, T2, T3, T4, T5, T6, T7>
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   where T5 : class
			   where T6 : class
			   where T7 : class
			   where T8 : class
			   
    {
		/// <summary>
        /// Service pipeline access
        /// </summary>
        protected ServicePipeline<T1, T2, T3, T4, T5, T6, T7, T8> Q { get; private set; }

        internal override void CallInit(Pipeline pipeline)
        {
            Q = new ServicePipeline<T1, T2, T3, T4, T5, T6, T7, T8>(pipeline);
            Init();
        }

        internal override ServicePipeline Pipeline
        {
            get { return Q; }
        }
    }

}