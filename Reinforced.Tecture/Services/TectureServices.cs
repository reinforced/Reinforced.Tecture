using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;



namespace Reinforced.Tecture.Services {	
	
 
	

	/// <summary>
    /// Storage services that touches 2 entities
    /// </summary>    
	public class TectureService<T1, T2>
			   : TectureServiceBase
			   where T1: class
			   where T2 : class
			   
    {

        protected Read<T, T1, T2> From<T>() where T : CanQuery
        {
            return new SRead<T, T1, T2>(ChannelMultiplexer, TestData.Instance);
        }		

		protected Write<T, T1, T2> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1, T2>(ChannelMultiplexer, Pipeline);
        }	
    }

 
	

	/// <summary>
    /// Storage services that touches 3 entities
    /// </summary>    
	public class TectureService<T1, T2, T3>
			   : TectureServiceBase
			   where T1: class
			   where T2 : class
			   where T3 : class
			   
    {

        protected Read<T, T1, T2, T3> From<T>() where T : CanQuery
        {
            return new SRead<T, T1, T2, T3>(ChannelMultiplexer, TestData.Instance);
        }		

		protected Write<T, T1, T2, T3> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1, T2, T3>(ChannelMultiplexer, Pipeline);
        }	
    }

 
	

	/// <summary>
    /// Storage services that touches 4 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4>
			   : TectureServiceBase
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   
    {

        protected Read<T, T1, T2, T3, T4> From<T>() where T : CanQuery
        {
            return new SRead<T, T1, T2, T3, T4>(ChannelMultiplexer, TestData.Instance);
        }		

		protected Write<T, T1, T2, T3, T4> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1, T2, T3, T4>(ChannelMultiplexer, Pipeline);
        }	
    }

 
	

	/// <summary>
    /// Storage services that touches 5 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4, T5>
			   : TectureServiceBase
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   where T5 : class
			   
    {

        protected Read<T, T1, T2, T3, T4, T5> From<T>() where T : CanQuery
        {
            return new SRead<T, T1, T2, T3, T4, T5>(ChannelMultiplexer, TestData.Instance);
        }		

		protected Write<T, T1, T2, T3, T4, T5> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1, T2, T3, T4, T5>(ChannelMultiplexer, Pipeline);
        }	
    }

 
	

	/// <summary>
    /// Storage services that touches 6 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4, T5, T6>
			   : TectureServiceBase
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   where T5 : class
			   where T6 : class
			   
    {

        protected Read<T, T1, T2, T3, T4, T5, T6> From<T>() where T : CanQuery
        {
            return new SRead<T, T1, T2, T3, T4, T5, T6>(ChannelMultiplexer, TestData.Instance);
        }		

		protected Write<T, T1, T2, T3, T4, T5, T6> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1, T2, T3, T4, T5, T6>(ChannelMultiplexer, Pipeline);
        }	
    }

 
	

	/// <summary>
    /// Storage services that touches 7 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4, T5, T6, T7>
			   : TectureServiceBase
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   where T5 : class
			   where T6 : class
			   where T7 : class
			   
    {

        protected Read<T, T1, T2, T3, T4, T5, T6, T7> From<T>() where T : CanQuery
        {
            return new SRead<T, T1, T2, T3, T4, T5, T6, T7>(ChannelMultiplexer, TestData.Instance);
        }		

		protected Write<T, T1, T2, T3, T4, T5, T6, T7> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1, T2, T3, T4, T5, T6, T7>(ChannelMultiplexer, Pipeline);
        }	
    }

 
	

	/// <summary>
    /// Storage services that touches 8 entities
    /// </summary>    
	public class TectureService<T1, T2, T3, T4, T5, T6, T7, T8>
			   : TectureServiceBase
			   where T1: class
			   where T2 : class
			   where T3 : class
			   where T4 : class
			   where T5 : class
			   where T6 : class
			   where T7 : class
			   where T8 : class
			   
    {

        protected Read<T, T1, T2, T3, T4, T5, T6, T7, T8> From<T>() where T : CanQuery
        {
            return new SRead<T, T1, T2, T3, T4, T5, T6, T7, T8>(ChannelMultiplexer, TestData.Instance);
        }		

		protected Write<T, T1, T2, T3, T4, T5, T6, T7, T8> To<T>() where T : CanCommand
        {
            return new SWrite<T, T1, T2, T3, T4, T5, T6, T7, T8>(ChannelMultiplexer, Pipeline);
        }	
    }

}

