

namespace Reinforced.Tecture.Services {	

	/// <summary>
    /// Interface that makes storage service to have context of 1 instance
    /// </summary> 
	public interface IContext<in T> : IWithContext
	{
		/// <summary>
		/// Imports context into service
		/// </summary>
		void Context(T ctx);
	}

	/// <summary>
	/// Extensions for service location
	/// </summary>
	public static partial class DoExtensions
    {

		/// <summary>
        /// Binds context to service
        /// </summary>
        /// <typeparam name="TService">Type of service</typeparam>
        /// <typeparam name="T1">Type of context variable #1</typeparam>	
		/// <param name="db">Service builder</param>
        /// <param name="arg1">Context variable of type <typeparamref name="T1"></typeparamref> #1</param>
		/// <returns>Service instance</returns>
		public static TService Within<TService, T1>(this LetBuilder<TService> db, T1 arg1) 
			where TService : TectureService, IContext<T1>
        {
            return db.Init(new[] { typeof(T1) }, new object[] { arg1 });
        }    
	 

		/// <summary>
        /// Binds context to service
        /// </summary>
        /// <typeparam name="TService">Type of service</typeparam>
        /// <typeparam name="T1">Type of context variable #1</typeparam>
		/// <typeparam name="T2">Type of context variable #2</typeparam>		
		/// <param name="db">Service builder</param>
        /// <param name="arg1">Context variable of type <typeparamref name="T1"></typeparamref> #1</param>
		/// <param name="arg2">Context variable of type <typeparamref name="T2"></typeparamref> #2</param>		
		/// <returns>Service instance</returns>
		public static TService Within<TService, T1, T2>(this LetBuilder<TService> db, T1 arg1, T2 arg2) 
			where TService : TectureService, IContext<T1, T2>
        {
            return db.Init(
			new[]{ typeof(T1), typeof(T2) }
			,new object[] { arg1, arg2 });
        }       
	 

		/// <summary>
        /// Binds context to service
        /// </summary>
        /// <typeparam name="TService">Type of service</typeparam>
        /// <typeparam name="T1">Type of context variable #1</typeparam>
		/// <typeparam name="T2">Type of context variable #2</typeparam>		
		/// <typeparam name="T3">Type of context variable #3</typeparam>		
		/// <param name="db">Service builder</param>
        /// <param name="arg1">Context variable of type <typeparamref name="T1"></typeparamref> #1</param>
		/// <param name="arg2">Context variable of type <typeparamref name="T2"></typeparamref> #2</param>		
		/// <param name="arg3">Context variable of type <typeparamref name="T3"></typeparamref> #3</param>		
		/// <returns>Service instance</returns>
		public static TService Within<TService, T1, T2, T3>(this LetBuilder<TService> db, T1 arg1, T2 arg2, T3 arg3) 
			where TService : TectureService, IContext<T1, T2, T3>
        {
            return db.Init(
			new[]{ typeof(T1), typeof(T2), typeof(T3) }
			,new object[] { arg1, arg2, arg3 });
        }       
	 

		/// <summary>
        /// Binds context to service
        /// </summary>
        /// <typeparam name="TService">Type of service</typeparam>
        /// <typeparam name="T1">Type of context variable #1</typeparam>
		/// <typeparam name="T2">Type of context variable #2</typeparam>		
		/// <typeparam name="T3">Type of context variable #3</typeparam>		
		/// <typeparam name="T4">Type of context variable #4</typeparam>		
		/// <param name="db">Service builder</param>
        /// <param name="arg1">Context variable of type <typeparamref name="T1"></typeparamref> #1</param>
		/// <param name="arg2">Context variable of type <typeparamref name="T2"></typeparamref> #2</param>		
		/// <param name="arg3">Context variable of type <typeparamref name="T3"></typeparamref> #3</param>		
		/// <param name="arg4">Context variable of type <typeparamref name="T4"></typeparamref> #4</param>		
		/// <returns>Service instance</returns>
		public static TService Within<TService, T1, T2, T3, T4>(this LetBuilder<TService> db, T1 arg1, T2 arg2, T3 arg3, T4 arg4) 
			where TService : TectureService, IContext<T1, T2, T3, T4>
        {
            return db.Init(
			new[]{ typeof(T1), typeof(T2), typeof(T3), typeof(T4) }
			,new object[] { arg1, arg2, arg3, arg4 });
        }       
	 

		/// <summary>
        /// Binds context to service
        /// </summary>
        /// <typeparam name="TService">Type of service</typeparam>
        /// <typeparam name="T1">Type of context variable #1</typeparam>
		/// <typeparam name="T2">Type of context variable #2</typeparam>		
		/// <typeparam name="T3">Type of context variable #3</typeparam>		
		/// <typeparam name="T4">Type of context variable #4</typeparam>		
		/// <typeparam name="T5">Type of context variable #5</typeparam>		
		/// <param name="db">Service builder</param>
        /// <param name="arg1">Context variable of type <typeparamref name="T1"></typeparamref> #1</param>
		/// <param name="arg2">Context variable of type <typeparamref name="T2"></typeparamref> #2</param>		
		/// <param name="arg3">Context variable of type <typeparamref name="T3"></typeparamref> #3</param>		
		/// <param name="arg4">Context variable of type <typeparamref name="T4"></typeparamref> #4</param>		
		/// <param name="arg5">Context variable of type <typeparamref name="T5"></typeparamref> #5</param>		
		/// <returns>Service instance</returns>
		public static TService Within<TService, T1, T2, T3, T4, T5>(this LetBuilder<TService> db, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) 
			where TService : TectureService, IContext<T1, T2, T3, T4, T5>
        {
            return db.Init(
			new[]{ typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) }
			,new object[] { arg1, arg2, arg3, arg4, arg5 });
        }       
	 

		/// <summary>
        /// Binds context to service
        /// </summary>
        /// <typeparam name="TService">Type of service</typeparam>
        /// <typeparam name="T1">Type of context variable #1</typeparam>
		/// <typeparam name="T2">Type of context variable #2</typeparam>		
		/// <typeparam name="T3">Type of context variable #3</typeparam>		
		/// <typeparam name="T4">Type of context variable #4</typeparam>		
		/// <typeparam name="T5">Type of context variable #5</typeparam>		
		/// <typeparam name="T6">Type of context variable #6</typeparam>		
		/// <param name="db">Service builder</param>
        /// <param name="arg1">Context variable of type <typeparamref name="T1"></typeparamref> #1</param>
		/// <param name="arg2">Context variable of type <typeparamref name="T2"></typeparamref> #2</param>		
		/// <param name="arg3">Context variable of type <typeparamref name="T3"></typeparamref> #3</param>		
		/// <param name="arg4">Context variable of type <typeparamref name="T4"></typeparamref> #4</param>		
		/// <param name="arg5">Context variable of type <typeparamref name="T5"></typeparamref> #5</param>		
		/// <param name="arg6">Context variable of type <typeparamref name="T6"></typeparamref> #6</param>		
		/// <returns>Service instance</returns>
		public static TService Within<TService, T1, T2, T3, T4, T5, T6>(this LetBuilder<TService> db, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) 
			where TService : TectureService, IContext<T1, T2, T3, T4, T5, T6>
        {
            return db.Init(
			new[]{ typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) }
			,new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }       
	 

		/// <summary>
        /// Binds context to service
        /// </summary>
        /// <typeparam name="TService">Type of service</typeparam>
        /// <typeparam name="T1">Type of context variable #1</typeparam>
		/// <typeparam name="T2">Type of context variable #2</typeparam>		
		/// <typeparam name="T3">Type of context variable #3</typeparam>		
		/// <typeparam name="T4">Type of context variable #4</typeparam>		
		/// <typeparam name="T5">Type of context variable #5</typeparam>		
		/// <typeparam name="T6">Type of context variable #6</typeparam>		
		/// <typeparam name="T7">Type of context variable #7</typeparam>		
		/// <param name="db">Service builder</param>
        /// <param name="arg1">Context variable of type <typeparamref name="T1"></typeparamref> #1</param>
		/// <param name="arg2">Context variable of type <typeparamref name="T2"></typeparamref> #2</param>		
		/// <param name="arg3">Context variable of type <typeparamref name="T3"></typeparamref> #3</param>		
		/// <param name="arg4">Context variable of type <typeparamref name="T4"></typeparamref> #4</param>		
		/// <param name="arg5">Context variable of type <typeparamref name="T5"></typeparamref> #5</param>		
		/// <param name="arg6">Context variable of type <typeparamref name="T6"></typeparamref> #6</param>		
		/// <param name="arg7">Context variable of type <typeparamref name="T7"></typeparamref> #7</param>		
		/// <returns>Service instance</returns>
		public static TService Within<TService, T1, T2, T3, T4, T5, T6, T7>(this LetBuilder<TService> db, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) 
			where TService : TectureService, IContext<T1, T2, T3, T4, T5, T6, T7>
        {
            return db.Init(
			new[]{ typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) }
			,new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }       
	 

		/// <summary>
        /// Binds context to service
        /// </summary>
        /// <typeparam name="TService">Type of service</typeparam>
        /// <typeparam name="T1">Type of context variable #1</typeparam>
		/// <typeparam name="T2">Type of context variable #2</typeparam>		
		/// <typeparam name="T3">Type of context variable #3</typeparam>		
		/// <typeparam name="T4">Type of context variable #4</typeparam>		
		/// <typeparam name="T5">Type of context variable #5</typeparam>		
		/// <typeparam name="T6">Type of context variable #6</typeparam>		
		/// <typeparam name="T7">Type of context variable #7</typeparam>		
		/// <typeparam name="T8">Type of context variable #8</typeparam>		
		/// <param name="db">Service builder</param>
        /// <param name="arg1">Context variable of type <typeparamref name="T1"></typeparamref> #1</param>
		/// <param name="arg2">Context variable of type <typeparamref name="T2"></typeparamref> #2</param>		
		/// <param name="arg3">Context variable of type <typeparamref name="T3"></typeparamref> #3</param>		
		/// <param name="arg4">Context variable of type <typeparamref name="T4"></typeparamref> #4</param>		
		/// <param name="arg5">Context variable of type <typeparamref name="T5"></typeparamref> #5</param>		
		/// <param name="arg6">Context variable of type <typeparamref name="T6"></typeparamref> #6</param>		
		/// <param name="arg7">Context variable of type <typeparamref name="T7"></typeparamref> #7</param>		
		/// <param name="arg8">Context variable of type <typeparamref name="T8"></typeparamref> #8</param>		
		/// <returns>Service instance</returns>
		public static TService Within<TService, T1, T2, T3, T4, T5, T6, T7, T8>(this LetBuilder<TService> db, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8) 
			where TService : TectureService, IContext<T1, T2, T3, T4, T5, T6, T7, T8>
        {
            return db.Init(
			new[]{ typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) }
			,new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }       
	    }
 
	
	/// <summary>
    /// Interface that makes storage service to have context of 2 instances
    /// </summary>
	public interface IContext<in T1, in T2> : IWithContext
	{
		/// <summary>
		/// Imports context into service
		/// </summary>
		void Context(T1 ctx1, T2 ctx2 );
	}	

 
	
	/// <summary>
    /// Interface that makes storage service to have context of 3 instances
    /// </summary>
	public interface IContext<in T1, in T2, in T3> : IWithContext
	{
		/// <summary>
		/// Imports context into service
		/// </summary>
		void Context(T1 ctx1, T2 ctx2 , T3 ctx3 );
	}	

 
	
	/// <summary>
    /// Interface that makes storage service to have context of 4 instances
    /// </summary>
	public interface IContext<in T1, in T2, in T3, in T4> : IWithContext
	{
		/// <summary>
		/// Imports context into service
		/// </summary>
		void Context(T1 ctx1, T2 ctx2 , T3 ctx3 , T4 ctx4 );
	}	

 
	
	/// <summary>
    /// Interface that makes storage service to have context of 5 instances
    /// </summary>
	public interface IContext<in T1, in T2, in T3, in T4, in T5> : IWithContext
	{
		/// <summary>
		/// Imports context into service
		/// </summary>
		void Context(T1 ctx1, T2 ctx2 , T3 ctx3 , T4 ctx4 , T5 ctx5 );
	}	

 
	
	/// <summary>
    /// Interface that makes storage service to have context of 6 instances
    /// </summary>
	public interface IContext<in T1, in T2, in T3, in T4, in T5, in T6> : IWithContext
	{
		/// <summary>
		/// Imports context into service
		/// </summary>
		void Context(T1 ctx1, T2 ctx2 , T3 ctx3 , T4 ctx4 , T5 ctx5 , T6 ctx6 );
	}	

 
	
	/// <summary>
    /// Interface that makes storage service to have context of 7 instances
    /// </summary>
	public interface IContext<in T1, in T2, in T3, in T4, in T5, in T6, in T7> : IWithContext
	{
		/// <summary>
		/// Imports context into service
		/// </summary>
		void Context(T1 ctx1, T2 ctx2 , T3 ctx3 , T4 ctx4 , T5 ctx5 , T6 ctx6 , T7 ctx7 );
	}	

 
	
	/// <summary>
    /// Interface that makes storage service to have context of 8 instances
    /// </summary>
	public interface IContext<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8> : IWithContext
	{
		/// <summary>
		/// Imports context into service
		/// </summary>
		void Context(T1 ctx1, T2 ctx2 , T3 ctx3 , T4 ctx4 , T5 ctx5 , T6 ctx6 , T7 ctx7 , T8 ctx8 );
	}	

}