
using System;
using System.Collections.Generic;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Savers
{
 

#region Producer/Saver pair #1
	
	/// <summary>
	/// Marking interface for feature being able to produce 1 various commands
	/// </summary>
	public interface Produces<TCommand1> : CommandFeature
		where TCommand1 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 1 commands
	/// </summary>
	public abstract class Saver<TCommand1> : SaverBase
		where TCommand1 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	}

#endregion

 

#region Producer/Saver pair #2
	
	/// <summary>
	/// Marking interface for feature being able to produce 2 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 2 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	}

#endregion

 

#region Producer/Saver pair #3
	
	/// <summary>
	/// Marking interface for feature being able to produce 3 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 3 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	}

#endregion

 

#region Producer/Saver pair #4
	
	/// <summary>
	/// Marking interface for feature being able to produce 4 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 4 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	}

#endregion

 

#region Producer/Saver pair #5
	
	/// <summary>
	/// Marking interface for feature being able to produce 5 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 5 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	}

#endregion

 

#region Producer/Saver pair #6
	
	/// <summary>
	/// Marking interface for feature being able to produce 6 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 6 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	}

#endregion

 

#region Producer/Saver pair #7
	
	/// <summary>
	/// Marking interface for feature being able to produce 7 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 7 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	}

#endregion

 

#region Producer/Saver pair #8
	
	/// <summary>
	/// Marking interface for feature being able to produce 8 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 8 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	}

#endregion

 

#region Producer/Saver pair #9
	
	/// <summary>
	/// Marking interface for feature being able to produce 9 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 9 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8);  
				yield return typeof(TCommand9); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8); 
			if (command is TCommand9 cm9) return GetRunner9(cm9);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand9"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand9"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand9> GetRunner9(TCommand9 command);
	}

#endregion

 

#region Producer/Saver pair #10
	
	/// <summary>
	/// Marking interface for feature being able to produce 10 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 10 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8);  
				yield return typeof(TCommand9);  
				yield return typeof(TCommand10); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8); 
			if (command is TCommand9 cm9) return GetRunner9(cm9); 
			if (command is TCommand10 cm10) return GetRunner10(cm10);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand9"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand9"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand9> GetRunner9(TCommand9 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand10"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand10"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand10> GetRunner10(TCommand10 command);
	}

#endregion

 

#region Producer/Saver pair #11
	
	/// <summary>
	/// Marking interface for feature being able to produce 11 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 11 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8);  
				yield return typeof(TCommand9);  
				yield return typeof(TCommand10);  
				yield return typeof(TCommand11); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8); 
			if (command is TCommand9 cm9) return GetRunner9(cm9); 
			if (command is TCommand10 cm10) return GetRunner10(cm10); 
			if (command is TCommand11 cm11) return GetRunner11(cm11);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand9"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand9"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand9> GetRunner9(TCommand9 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand10"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand10"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand10> GetRunner10(TCommand10 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand11"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand11"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand11> GetRunner11(TCommand11 command);
	}

#endregion

 

#region Producer/Saver pair #12
	
	/// <summary>
	/// Marking interface for feature being able to produce 12 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 12 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8);  
				yield return typeof(TCommand9);  
				yield return typeof(TCommand10);  
				yield return typeof(TCommand11);  
				yield return typeof(TCommand12); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8); 
			if (command is TCommand9 cm9) return GetRunner9(cm9); 
			if (command is TCommand10 cm10) return GetRunner10(cm10); 
			if (command is TCommand11 cm11) return GetRunner11(cm11); 
			if (command is TCommand12 cm12) return GetRunner12(cm12);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand9"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand9"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand9> GetRunner9(TCommand9 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand10"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand10"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand10> GetRunner10(TCommand10 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand11"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand11"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand11> GetRunner11(TCommand11 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand12"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand12"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand12> GetRunner12(TCommand12 command);
	}

#endregion

 

#region Producer/Saver pair #13
	
	/// <summary>
	/// Marking interface for feature being able to produce 13 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase
where TCommand13 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 13 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase
where TCommand13 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8);  
				yield return typeof(TCommand9);  
				yield return typeof(TCommand10);  
				yield return typeof(TCommand11);  
				yield return typeof(TCommand12);  
				yield return typeof(TCommand13); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8); 
			if (command is TCommand9 cm9) return GetRunner9(cm9); 
			if (command is TCommand10 cm10) return GetRunner10(cm10); 
			if (command is TCommand11 cm11) return GetRunner11(cm11); 
			if (command is TCommand12 cm12) return GetRunner12(cm12); 
			if (command is TCommand13 cm13) return GetRunner13(cm13);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand9"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand9"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand9> GetRunner9(TCommand9 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand10"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand10"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand10> GetRunner10(TCommand10 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand11"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand11"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand11> GetRunner11(TCommand11 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand12"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand12"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand12> GetRunner12(TCommand12 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand13"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand13"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand13> GetRunner13(TCommand13 command);
	}

#endregion

 

#region Producer/Saver pair #14
	
	/// <summary>
	/// Marking interface for feature being able to produce 14 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase
where TCommand13 : CommandBase
where TCommand14 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 14 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase
where TCommand13 : CommandBase
where TCommand14 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8);  
				yield return typeof(TCommand9);  
				yield return typeof(TCommand10);  
				yield return typeof(TCommand11);  
				yield return typeof(TCommand12);  
				yield return typeof(TCommand13);  
				yield return typeof(TCommand14); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8); 
			if (command is TCommand9 cm9) return GetRunner9(cm9); 
			if (command is TCommand10 cm10) return GetRunner10(cm10); 
			if (command is TCommand11 cm11) return GetRunner11(cm11); 
			if (command is TCommand12 cm12) return GetRunner12(cm12); 
			if (command is TCommand13 cm13) return GetRunner13(cm13); 
			if (command is TCommand14 cm14) return GetRunner14(cm14);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand9"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand9"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand9> GetRunner9(TCommand9 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand10"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand10"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand10> GetRunner10(TCommand10 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand11"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand11"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand11> GetRunner11(TCommand11 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand12"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand12"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand12> GetRunner12(TCommand12 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand13"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand13"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand13> GetRunner13(TCommand13 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand14"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand14"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand14> GetRunner14(TCommand14 command);
	}

#endregion

 

#region Producer/Saver pair #15
	
	/// <summary>
	/// Marking interface for feature being able to produce 15 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase
where TCommand13 : CommandBase
where TCommand14 : CommandBase
where TCommand15 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 15 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase
where TCommand13 : CommandBase
where TCommand14 : CommandBase
where TCommand15 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8);  
				yield return typeof(TCommand9);  
				yield return typeof(TCommand10);  
				yield return typeof(TCommand11);  
				yield return typeof(TCommand12);  
				yield return typeof(TCommand13);  
				yield return typeof(TCommand14);  
				yield return typeof(TCommand15); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8); 
			if (command is TCommand9 cm9) return GetRunner9(cm9); 
			if (command is TCommand10 cm10) return GetRunner10(cm10); 
			if (command is TCommand11 cm11) return GetRunner11(cm11); 
			if (command is TCommand12 cm12) return GetRunner12(cm12); 
			if (command is TCommand13 cm13) return GetRunner13(cm13); 
			if (command is TCommand14 cm14) return GetRunner14(cm14); 
			if (command is TCommand15 cm15) return GetRunner15(cm15);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand9"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand9"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand9> GetRunner9(TCommand9 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand10"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand10"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand10> GetRunner10(TCommand10 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand11"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand11"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand11> GetRunner11(TCommand11 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand12"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand12"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand12> GetRunner12(TCommand12 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand13"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand13"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand13> GetRunner13(TCommand13 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand14"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand14"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand14> GetRunner14(TCommand14 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand15"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand15"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand15> GetRunner15(TCommand15 command);
	}

#endregion

 

#region Producer/Saver pair #16
	
	/// <summary>
	/// Marking interface for feature being able to produce 16 various commands
	/// </summary>
	public interface Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15, TCommand16> : CommandFeature
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase
where TCommand13 : CommandBase
where TCommand14 : CommandBase
where TCommand15 : CommandBase
where TCommand16 : CommandBase

	{ }

	/// <summary>
	/// Saver serving 16 commands
	/// </summary>
	public abstract class Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15, TCommand16> : SaverBase
		where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase
where TCommand9 : CommandBase
where TCommand10 : CommandBase
where TCommand11 : CommandBase
where TCommand12 : CommandBase
where TCommand13 : CommandBase
where TCommand14 : CommandBase
where TCommand15 : CommandBase
where TCommand16 : CommandBase

	{ 

		internal override IEnumerable<Type> ServingCommandTypes
		{
			get 
			{  
				yield return typeof(TCommand1);  
				yield return typeof(TCommand2);  
				yield return typeof(TCommand3);  
				yield return typeof(TCommand4);  
				yield return typeof(TCommand5);  
				yield return typeof(TCommand6);  
				yield return typeof(TCommand7);  
				yield return typeof(TCommand8);  
				yield return typeof(TCommand9);  
				yield return typeof(TCommand10);  
				yield return typeof(TCommand11);  
				yield return typeof(TCommand12);  
				yield return typeof(TCommand13);  
				yield return typeof(TCommand14);  
				yield return typeof(TCommand15);  
				yield return typeof(TCommand16); 		
			}
		}

		internal override CommandRunner GetRunner(CommandBase command)
		{
	 
			if (command is TCommand1 cm1) return GetRunner1(cm1); 
			if (command is TCommand2 cm2) return GetRunner2(cm2); 
			if (command is TCommand3 cm3) return GetRunner3(cm3); 
			if (command is TCommand4 cm4) return GetRunner4(cm4); 
			if (command is TCommand5 cm5) return GetRunner5(cm5); 
			if (command is TCommand6 cm6) return GetRunner6(cm6); 
			if (command is TCommand7 cm7) return GetRunner7(cm7); 
			if (command is TCommand8 cm8) return GetRunner8(cm8); 
			if (command is TCommand9 cm9) return GetRunner9(cm9); 
			if (command is TCommand10 cm10) return GetRunner10(cm10); 
			if (command is TCommand11 cm11) return GetRunner11(cm11); 
			if (command is TCommand12 cm12) return GetRunner12(cm12); 
			if (command is TCommand13 cm13) return GetRunner13(cm13); 
			if (command is TCommand14 cm14) return GetRunner14(cm14); 
			if (command is TCommand15 cm15) return GetRunner15(cm15); 
			if (command is TCommand16 cm16) return GetRunner16(cm16);
			throw new TectureException($"No runner for command {command.GetType().Name} found in {this.GetType().Name}");
		}

	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand1"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand1"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand1> GetRunner1(TCommand1 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand2"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand2"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand2> GetRunner2(TCommand2 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand3"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand3"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand3> GetRunner3(TCommand3 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand4"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand4"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand4> GetRunner4(TCommand4 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand5"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand5"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand5> GetRunner5(TCommand5 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand6"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand6"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand6> GetRunner6(TCommand6 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand7"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand7"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand7> GetRunner7(TCommand7 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand8"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand8"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand8> GetRunner8(TCommand8 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand9"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand9"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand9> GetRunner9(TCommand9 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand10"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand10"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand10> GetRunner10(TCommand10 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand11"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand11"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand11> GetRunner11(TCommand11 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand12"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand12"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand12> GetRunner12(TCommand12 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand13"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand13"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand13> GetRunner13(TCommand13 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand14"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand14"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand14> GetRunner14(TCommand14 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand15"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand15"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand15> GetRunner15(TCommand15 command);
	 
		/// <summary>
		/// Returns instance of command runner for command <typeparamref name="TCommand16"/>. 
		/// </summary>
		/// <param name="command">Command of type <typeparamref name="TCommand16"/> </param>
		/// <returns>Command runner</returns>
		protected abstract CommandRunner<TCommand16> GetRunner16(TCommand16 command);
	}

#endregion

}

