
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Channels
{
 
	
	#region Setup for 1 entities

	#region Read

	public interface Read<out TChannel, out E1> : Read<TChannel> where TChannel : CanQuery { }

	internal struct SRead<TChannel, E1>
		: IQueryMultiplexer, 
		  Read<TChannel, E1>
		 where TChannel : CanQuery
	{
		private readonly ChannelMultiplexer _cm;
		private readonly TestData _qs;
		public SRead(ChannelMultiplexer cm, TestData qs)
		{
			_cm = cm;
			_qs = qs;
		}

		public TFeature GetFeature<TFeature>(out TestData qs) where TFeature : QueryFeature 
		{
			qs = _qs;
			return _cm.GetQueryFeature<TChannel,TFeature>();
		}

		public TestData GetStore()
		{
			return _qs;
		}
	}

	#endregion

	#region Write
	public interface Write<out TChannel, out E1> : Write<TChannel> where TChannel : CanCommand { }

	internal struct SWrite<TChannel, E1>
		: ICommandMultiplexer, 
		  Write<TChannel, E1>
		 where TChannel : CanCommand
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TFeature GetFeature<TFeature>() where TFeature : CommandFeature 
		{
			return _cm.GetCommandFeature<TChannel,TFeature>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(Channel).FullName;
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

	public interface Read<out TChannel, out E1, out E2> : Read<TChannel> where TChannel : CanQuery { }

	internal struct SRead<TChannel, E1, E2>
		: IQueryMultiplexer, 
		  Read<TChannel, E1, E2>
		 where TChannel : CanQuery
	{
		private readonly ChannelMultiplexer _cm;
		private readonly TestData _qs;
		public SRead(ChannelMultiplexer cm, TestData qs)
		{
			_cm = cm;
			_qs = qs;
		}

		public TFeature GetFeature<TFeature>(out TestData qs) where TFeature : QueryFeature 
		{
			qs = _qs;
			return _cm.GetQueryFeature<TChannel,TFeature>();
		}

		public TestData GetStore()
		{
			return _qs;
		}
	}

	#endregion

	#region Write
	public interface Write<out TChannel, out E1, out E2> : Write<TChannel> where TChannel : CanCommand { }

	internal struct SWrite<TChannel, E1, E2>
		: ICommandMultiplexer, 
		  Write<TChannel, E1, E2>
		 where TChannel : CanCommand
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TFeature GetFeature<TFeature>() where TFeature : CommandFeature 
		{
			return _cm.GetCommandFeature<TChannel,TFeature>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(Channel).FullName;
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

	public interface Read<out TChannel, out E1, out E2, out E3> : Read<TChannel> where TChannel : CanQuery { }

	internal struct SRead<TChannel, E1, E2, E3>
		: IQueryMultiplexer, 
		  Read<TChannel, E1, E2, E3>
		 where TChannel : CanQuery
	{
		private readonly ChannelMultiplexer _cm;
		private readonly TestData _qs;
		public SRead(ChannelMultiplexer cm, TestData qs)
		{
			_cm = cm;
			_qs = qs;
		}

		public TFeature GetFeature<TFeature>(out TestData qs) where TFeature : QueryFeature 
		{
			qs = _qs;
			return _cm.GetQueryFeature<TChannel,TFeature>();
		}

		public TestData GetStore()
		{
			return _qs;
		}
	}

	#endregion

	#region Write
	public interface Write<out TChannel, out E1, out E2, out E3> : Write<TChannel> where TChannel : CanCommand { }

	internal struct SWrite<TChannel, E1, E2, E3>
		: ICommandMultiplexer, 
		  Write<TChannel, E1, E2, E3>
		 where TChannel : CanCommand
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TFeature GetFeature<TFeature>() where TFeature : CommandFeature 
		{
			return _cm.GetCommandFeature<TChannel,TFeature>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(Channel).FullName;
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

	public interface Read<out TChannel, out E1, out E2, out E3, out E4> : Read<TChannel> where TChannel : CanQuery { }

	internal struct SRead<TChannel, E1, E2, E3, E4>
		: IQueryMultiplexer, 
		  Read<TChannel, E1, E2, E3, E4>
		 where TChannel : CanQuery
	{
		private readonly ChannelMultiplexer _cm;
		private readonly TestData _qs;
		public SRead(ChannelMultiplexer cm, TestData qs)
		{
			_cm = cm;
			_qs = qs;
		}

		public TFeature GetFeature<TFeature>(out TestData qs) where TFeature : QueryFeature 
		{
			qs = _qs;
			return _cm.GetQueryFeature<TChannel,TFeature>();
		}

		public TestData GetStore()
		{
			return _qs;
		}
	}

	#endregion

	#region Write
	public interface Write<out TChannel, out E1, out E2, out E3, out E4> : Write<TChannel> where TChannel : CanCommand { }

	internal struct SWrite<TChannel, E1, E2, E3, E4>
		: ICommandMultiplexer, 
		  Write<TChannel, E1, E2, E3, E4>
		 where TChannel : CanCommand
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TFeature GetFeature<TFeature>() where TFeature : CommandFeature 
		{
			return _cm.GetCommandFeature<TChannel,TFeature>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(Channel).FullName;
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

	public interface Read<out TChannel, out E1, out E2, out E3, out E4, out E5> : Read<TChannel> where TChannel : CanQuery { }

	internal struct SRead<TChannel, E1, E2, E3, E4, E5>
		: IQueryMultiplexer, 
		  Read<TChannel, E1, E2, E3, E4, E5>
		 where TChannel : CanQuery
	{
		private readonly ChannelMultiplexer _cm;
		private readonly TestData _qs;
		public SRead(ChannelMultiplexer cm, TestData qs)
		{
			_cm = cm;
			_qs = qs;
		}

		public TFeature GetFeature<TFeature>(out TestData qs) where TFeature : QueryFeature 
		{
			qs = _qs;
			return _cm.GetQueryFeature<TChannel,TFeature>();
		}

		public TestData GetStore()
		{
			return _qs;
		}
	}

	#endregion

	#region Write
	public interface Write<out TChannel, out E1, out E2, out E3, out E4, out E5> : Write<TChannel> where TChannel : CanCommand { }

	internal struct SWrite<TChannel, E1, E2, E3, E4, E5>
		: ICommandMultiplexer, 
		  Write<TChannel, E1, E2, E3, E4, E5>
		 where TChannel : CanCommand
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TFeature GetFeature<TFeature>() where TFeature : CommandFeature 
		{
			return _cm.GetCommandFeature<TChannel,TFeature>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(Channel).FullName;
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

	public interface Read<out TChannel, out E1, out E2, out E3, out E4, out E5, out E6> : Read<TChannel> where TChannel : CanQuery { }

	internal struct SRead<TChannel, E1, E2, E3, E4, E5, E6>
		: IQueryMultiplexer, 
		  Read<TChannel, E1, E2, E3, E4, E5, E6>
		 where TChannel : CanQuery
	{
		private readonly ChannelMultiplexer _cm;
		private readonly TestData _qs;
		public SRead(ChannelMultiplexer cm, TestData qs)
		{
			_cm = cm;
			_qs = qs;
		}

		public TFeature GetFeature<TFeature>(out TestData qs) where TFeature : QueryFeature 
		{
			qs = _qs;
			return _cm.GetQueryFeature<TChannel,TFeature>();
		}

		public TestData GetStore()
		{
			return _qs;
		}
	}

	#endregion

	#region Write
	public interface Write<out TChannel, out E1, out E2, out E3, out E4, out E5, out E6> : Write<TChannel> where TChannel : CanCommand { }

	internal struct SWrite<TChannel, E1, E2, E3, E4, E5, E6>
		: ICommandMultiplexer, 
		  Write<TChannel, E1, E2, E3, E4, E5, E6>
		 where TChannel : CanCommand
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TFeature GetFeature<TFeature>() where TFeature : CommandFeature 
		{
			return _cm.GetCommandFeature<TChannel,TFeature>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(Channel).FullName;
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

	public interface Read<out TChannel, out E1, out E2, out E3, out E4, out E5, out E6, out E7> : Read<TChannel> where TChannel : CanQuery { }

	internal struct SRead<TChannel, E1, E2, E3, E4, E5, E6, E7>
		: IQueryMultiplexer, 
		  Read<TChannel, E1, E2, E3, E4, E5, E6, E7>
		 where TChannel : CanQuery
	{
		private readonly ChannelMultiplexer _cm;
		private readonly TestData _qs;
		public SRead(ChannelMultiplexer cm, TestData qs)
		{
			_cm = cm;
			_qs = qs;
		}

		public TFeature GetFeature<TFeature>(out TestData qs) where TFeature : QueryFeature 
		{
			qs = _qs;
			return _cm.GetQueryFeature<TChannel,TFeature>();
		}

		public TestData GetStore()
		{
			return _qs;
		}
	}

	#endregion

	#region Write
	public interface Write<out TChannel, out E1, out E2, out E3, out E4, out E5, out E6, out E7> : Write<TChannel> where TChannel : CanCommand { }

	internal struct SWrite<TChannel, E1, E2, E3, E4, E5, E6, E7>
		: ICommandMultiplexer, 
		  Write<TChannel, E1, E2, E3, E4, E5, E6, E7>
		 where TChannel : CanCommand
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TFeature GetFeature<TFeature>() where TFeature : CommandFeature 
		{
			return _cm.GetCommandFeature<TChannel,TFeature>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(Channel).FullName;
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

	public interface Read<out TChannel, out E1, out E2, out E3, out E4, out E5, out E6, out E7, out E8> : Read<TChannel> where TChannel : CanQuery { }

	internal struct SRead<TChannel, E1, E2, E3, E4, E5, E6, E7, E8>
		: IQueryMultiplexer, 
		  Read<TChannel, E1, E2, E3, E4, E5, E6, E7, E8>
		 where TChannel : CanQuery
	{
		private readonly ChannelMultiplexer _cm;
		private readonly TestData _qs;
		public SRead(ChannelMultiplexer cm, TestData qs)
		{
			_cm = cm;
			_qs = qs;
		}

		public TFeature GetFeature<TFeature>(out TestData qs) where TFeature : QueryFeature 
		{
			qs = _qs;
			return _cm.GetQueryFeature<TChannel,TFeature>();
		}

		public TestData GetStore()
		{
			return _qs;
		}
	}

	#endregion

	#region Write
	public interface Write<out TChannel, out E1, out E2, out E3, out E4, out E5, out E6, out E7, out E8> : Write<TChannel> where TChannel : CanCommand { }

	internal struct SWrite<TChannel, E1, E2, E3, E4, E5, E6, E7, E8>
		: ICommandMultiplexer, 
		  Write<TChannel, E1, E2, E3, E4, E5, E6, E7, E8>
		 where TChannel : CanCommand
	{
		private readonly ChannelMultiplexer _cm;
		private readonly Pipeline _pipeline;
		public SWrite(ChannelMultiplexer cm, Pipeline p)
		{
			_cm = cm;
			_pipeline = p;
		}

		public TFeature GetFeature<TFeature>() where TFeature : CommandFeature 
		{
			return _cm.GetCommandFeature<TChannel,TFeature>();
		}

		public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
		{
			command.ChannelId = typeof(Channel).FullName;
			_pipeline.Enqueue(command);
			return command;
		}

		 public ActionsQueue Save { get { return _pipeline.PostSaveActions;} }

		 public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
	}
	#endregion 

	#endregion
	}