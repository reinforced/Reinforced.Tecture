
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Services;

// ReSharper disable UnusedTypeParameter

namespace Reinforced.Tecture.Channels
{
 
	
	#region Setup for 1 entities

	#region Read

	/// <summary>
	/// Channel's read end for 1 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	public interface Read<out TChannel , out T1> : Read<TChannel> 
		where TChannel : CanQuery 
		where T1 : Tooling
	{ }

	internal struct SRead<TChannel , T1>
		: IQueryMultiplexer, Read<TChannel , T1>
		where TChannel : CanQuery
		where T1 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		
		public SRead(ChannelMultiplexer cm)
		{
			_cm = cm;		
		}

		public TAspect GetAspect<TAspect>() where TAspect : QueryAspect 
		{
			return _cm.GetQueryAspect<TChannel,TAspect>();
		}
	}

	#endregion

	#region Write
	/// <summary>
	/// Channel's write end for 1 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	public interface Write<out TChannel, out T1> : Write<TChannel> 
		where TChannel : CanCommand 
		where T1 : Tooling
	{ }

	internal struct SWrite<TChannel, T1>
		: ICommandMultiplexer, Write<TChannel, T1>
		where TChannel : CanCommand
		where T1 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TAspect GetAspect<TAspect>() where TAspect : CommandAspect 
		{
			return _cm.GetCommandAspect<TChannel,TAspect>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(TChannel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	 
	
	#region Setup for 2 entities

	#region Read

	/// <summary>
	/// Channel's read end for 2 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	public interface Read<out TChannel , out T1, out T2> : Read<TChannel, T1> 
		where TChannel : CanQuery 
		where T1 : Tooling
		where T2 : Tooling
	{ }

	internal struct SRead<TChannel , T1, T2>
		: IQueryMultiplexer, Read<TChannel , T1, T2>
		where TChannel : CanQuery
		where T1 : Tooling
		where T2 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		
		public SRead(ChannelMultiplexer cm)
		{
			_cm = cm;		
		}

		public TAspect GetAspect<TAspect>() where TAspect : QueryAspect 
		{
			return _cm.GetQueryAspect<TChannel,TAspect>();
		}
	}

	#endregion

	#region Write
	/// <summary>
	/// Channel's write end for 2 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	public interface Write<out TChannel, out T1, out T2> : Write<TChannel, T1> 
		where TChannel : CanCommand 
		where T1 : Tooling
		where T2 : Tooling
	{ }

	internal struct SWrite<TChannel, T1, T2>
		: ICommandMultiplexer, Write<TChannel, T1, T2>
		where TChannel : CanCommand
		where T1 : Tooling
		where T2 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TAspect GetAspect<TAspect>() where TAspect : CommandAspect 
		{
			return _cm.GetCommandAspect<TChannel,TAspect>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(TChannel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	 
	
	#region Setup for 3 entities

	#region Read

	/// <summary>
	/// Channel's read end for 3 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	public interface Read<out TChannel , out T1, out T2, out T3> : Read<TChannel, T1, T2> 
		where TChannel : CanQuery 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
	{ }

	internal struct SRead<TChannel , T1, T2, T3>
		: IQueryMultiplexer, Read<TChannel , T1, T2, T3>
		where TChannel : CanQuery
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		
		public SRead(ChannelMultiplexer cm)
		{
			_cm = cm;		
		}

		public TAspect GetAspect<TAspect>() where TAspect : QueryAspect 
		{
			return _cm.GetQueryAspect<TChannel,TAspect>();
		}
	}

	#endregion

	#region Write
	/// <summary>
	/// Channel's write end for 3 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	public interface Write<out TChannel, out T1, out T2, out T3> : Write<TChannel, T1, T2> 
		where TChannel : CanCommand 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
	{ }

	internal struct SWrite<TChannel, T1, T2, T3>
		: ICommandMultiplexer, Write<TChannel, T1, T2, T3>
		where TChannel : CanCommand
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TAspect GetAspect<TAspect>() where TAspect : CommandAspect 
		{
			return _cm.GetCommandAspect<TChannel,TAspect>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(TChannel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	 
	
	#region Setup for 4 entities

	#region Read

	/// <summary>
	/// Channel's read end for 4 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	public interface Read<out TChannel , out T1, out T2, out T3, out T4> : Read<TChannel, T1, T2, T3> 
		where TChannel : CanQuery 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
	{ }

	internal struct SRead<TChannel , T1, T2, T3, T4>
		: IQueryMultiplexer, Read<TChannel , T1, T2, T3, T4>
		where TChannel : CanQuery
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		
		public SRead(ChannelMultiplexer cm)
		{
			_cm = cm;		
		}

		public TAspect GetAspect<TAspect>() where TAspect : QueryAspect 
		{
			return _cm.GetQueryAspect<TChannel,TAspect>();
		}
	}

	#endregion

	#region Write
	/// <summary>
	/// Channel's write end for 4 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	public interface Write<out TChannel, out T1, out T2, out T3, out T4> : Write<TChannel, T1, T2, T3> 
		where TChannel : CanCommand 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
	{ }

	internal struct SWrite<TChannel, T1, T2, T3, T4>
		: ICommandMultiplexer, Write<TChannel, T1, T2, T3, T4>
		where TChannel : CanCommand
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TAspect GetAspect<TAspect>() where TAspect : CommandAspect 
		{
			return _cm.GetCommandAspect<TChannel,TAspect>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(TChannel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	 
	
	#region Setup for 5 entities

	#region Read

	/// <summary>
	/// Channel's read end for 5 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	/// <typeparam name="T5">Tooling of type # 5</typeparam>
	public interface Read<out TChannel , out T1, out T2, out T3, out T4, out T5> : Read<TChannel, T1, T2, T3, T4> 
		where TChannel : CanQuery 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
	{ }

	internal struct SRead<TChannel , T1, T2, T3, T4, T5>
		: IQueryMultiplexer, Read<TChannel , T1, T2, T3, T4, T5>
		where TChannel : CanQuery
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		
		public SRead(ChannelMultiplexer cm)
		{
			_cm = cm;		
		}

		public TAspect GetAspect<TAspect>() where TAspect : QueryAspect 
		{
			return _cm.GetQueryAspect<TChannel,TAspect>();
		}
	}

	#endregion

	#region Write
	/// <summary>
	/// Channel's write end for 5 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	/// <typeparam name="T5">Tooling of type # 5</typeparam>
	public interface Write<out TChannel, out T1, out T2, out T3, out T4, out T5> : Write<TChannel, T1, T2, T3, T4> 
		where TChannel : CanCommand 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
	{ }

	internal struct SWrite<TChannel, T1, T2, T3, T4, T5>
		: ICommandMultiplexer, Write<TChannel, T1, T2, T3, T4, T5>
		where TChannel : CanCommand
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TAspect GetAspect<TAspect>() where TAspect : CommandAspect 
		{
			return _cm.GetCommandAspect<TChannel,TAspect>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(TChannel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	 
	
	#region Setup for 6 entities

	#region Read

	/// <summary>
	/// Channel's read end for 6 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	/// <typeparam name="T5">Tooling of type # 5</typeparam>
	/// <typeparam name="T6">Tooling of type # 6</typeparam>
	public interface Read<out TChannel , out T1, out T2, out T3, out T4, out T5, out T6> : Read<TChannel, T1, T2, T3, T4, T5> 
		where TChannel : CanQuery 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
	{ }

	internal struct SRead<TChannel , T1, T2, T3, T4, T5, T6>
		: IQueryMultiplexer, Read<TChannel , T1, T2, T3, T4, T5, T6>
		where TChannel : CanQuery
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		
		public SRead(ChannelMultiplexer cm)
		{
			_cm = cm;		
		}

		public TAspect GetAspect<TAspect>() where TAspect : QueryAspect 
		{
			return _cm.GetQueryAspect<TChannel,TAspect>();
		}
	}

	#endregion

	#region Write
	/// <summary>
	/// Channel's write end for 6 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	/// <typeparam name="T5">Tooling of type # 5</typeparam>
	/// <typeparam name="T6">Tooling of type # 6</typeparam>
	public interface Write<out TChannel, out T1, out T2, out T3, out T4, out T5, out T6> : Write<TChannel, T1, T2, T3, T4, T5> 
		where TChannel : CanCommand 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
	{ }

	internal struct SWrite<TChannel, T1, T2, T3, T4, T5, T6>
		: ICommandMultiplexer, Write<TChannel, T1, T2, T3, T4, T5, T6>
		where TChannel : CanCommand
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TAspect GetAspect<TAspect>() where TAspect : CommandAspect 
		{
			return _cm.GetCommandAspect<TChannel,TAspect>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(TChannel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	 
	
	#region Setup for 7 entities

	#region Read

	/// <summary>
	/// Channel's read end for 7 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	/// <typeparam name="T5">Tooling of type # 5</typeparam>
	/// <typeparam name="T6">Tooling of type # 6</typeparam>
	/// <typeparam name="T7">Tooling of type # 7</typeparam>
	public interface Read<out TChannel , out T1, out T2, out T3, out T4, out T5, out T6, out T7> : Read<TChannel, T1, T2, T3, T4, T5, T6> 
		where TChannel : CanQuery 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
		where T7 : Tooling
	{ }

	internal struct SRead<TChannel , T1, T2, T3, T4, T5, T6, T7>
		: IQueryMultiplexer, Read<TChannel , T1, T2, T3, T4, T5, T6, T7>
		where TChannel : CanQuery
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
		where T7 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		
		public SRead(ChannelMultiplexer cm)
		{
			_cm = cm;		
		}

		public TAspect GetAspect<TAspect>() where TAspect : QueryAspect 
		{
			return _cm.GetQueryAspect<TChannel,TAspect>();
		}
	}

	#endregion

	#region Write
	/// <summary>
	/// Channel's write end for 7 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	/// <typeparam name="T5">Tooling of type # 5</typeparam>
	/// <typeparam name="T6">Tooling of type # 6</typeparam>
	/// <typeparam name="T7">Tooling of type # 7</typeparam>
	public interface Write<out TChannel, out T1, out T2, out T3, out T4, out T5, out T6, out T7> : Write<TChannel, T1, T2, T3, T4, T5, T6> 
		where TChannel : CanCommand 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
		where T7 : Tooling
	{ }

	internal struct SWrite<TChannel, T1, T2, T3, T4, T5, T6, T7>
		: ICommandMultiplexer, Write<TChannel, T1, T2, T3, T4, T5, T6, T7>
		where TChannel : CanCommand
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
		where T7 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TAspect GetAspect<TAspect>() where TAspect : CommandAspect 
		{
			return _cm.GetCommandAspect<TChannel,TAspect>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(TChannel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	 
	
	#region Setup for 8 entities

	#region Read

	/// <summary>
	/// Channel's read end for 8 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	/// <typeparam name="T5">Tooling of type # 5</typeparam>
	/// <typeparam name="T6">Tooling of type # 6</typeparam>
	/// <typeparam name="T7">Tooling of type # 7</typeparam>
	/// <typeparam name="T8">Tooling of type # 8</typeparam>
	public interface Read<out TChannel , out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8> : Read<TChannel, T1, T2, T3, T4, T5, T6, T7> 
		where TChannel : CanQuery 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
		where T7 : Tooling
		where T8 : Tooling
	{ }

	internal struct SRead<TChannel , T1, T2, T3, T4, T5, T6, T7, T8>
		: IQueryMultiplexer, Read<TChannel , T1, T2, T3, T4, T5, T6, T7, T8>
		where TChannel : CanQuery
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
		where T7 : Tooling
		where T8 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		
		public SRead(ChannelMultiplexer cm)
		{
			_cm = cm;		
		}

		public TAspect GetAspect<TAspect>() where TAspect : QueryAspect 
		{
			return _cm.GetQueryAspect<TChannel,TAspect>();
		}
	}

	#endregion

	#region Write
	/// <summary>
	/// Channel's write end for 8 entities
	/// </summary>
	/// <typeparam name="TChannel">Type of channel</typeparam>
	/// <typeparam name="T1">Tooling of type # 1</typeparam>
	/// <typeparam name="T2">Tooling of type # 2</typeparam>
	/// <typeparam name="T3">Tooling of type # 3</typeparam>
	/// <typeparam name="T4">Tooling of type # 4</typeparam>
	/// <typeparam name="T5">Tooling of type # 5</typeparam>
	/// <typeparam name="T6">Tooling of type # 6</typeparam>
	/// <typeparam name="T7">Tooling of type # 7</typeparam>
	/// <typeparam name="T8">Tooling of type # 8</typeparam>
	public interface Write<out TChannel, out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8> : Write<TChannel, T1, T2, T3, T4, T5, T6, T7> 
		where TChannel : CanCommand 
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
		where T7 : Tooling
		where T8 : Tooling
	{ }

	internal struct SWrite<TChannel, T1, T2, T3, T4, T5, T6, T7, T8>
		: ICommandMultiplexer, Write<TChannel, T1, T2, T3, T4, T5, T6, T7, T8>
		where TChannel : CanCommand
		where T1 : Tooling
		where T2 : Tooling
		where T3 : Tooling
		where T4 : Tooling
		where T5 : Tooling
		where T6 : Tooling
		where T7 : Tooling
		where T8 : Tooling
	
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TAspect GetAspect<TAspect>() where TAspect : CommandAspect 
		{
			return _cm.GetCommandAspect<TChannel,TAspect>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(TChannel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	}

