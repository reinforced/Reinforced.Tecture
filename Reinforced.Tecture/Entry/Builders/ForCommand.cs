
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Entry.Builders
{
	public static partial class Extensions
    {
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1> saver)
            where TFeature : CommandFeature, Produces<TCommand1>
            where TCommand1 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase
where TCommand5 : CommandBase
where TCommand6 : CommandBase
where TCommand7 : CommandBase
where TCommand8 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand9">Type of command #9 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9>
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
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand9">Type of command #9 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand10">Type of command #10 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10>
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
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand9">Type of command #9 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand10">Type of command #10 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand11">Type of command #11 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11>
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
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand9">Type of command #9 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand10">Type of command #10 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand11">Type of command #11 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand12">Type of command #12 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12>
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
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand9">Type of command #9 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand10">Type of command #10 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand11">Type of command #11 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand12">Type of command #12 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand13">Type of command #13 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13>
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
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand9">Type of command #9 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand10">Type of command #10 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand11">Type of command #11 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand12">Type of command #12 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand13">Type of command #13 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand14">Type of command #14 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14>
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
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand9">Type of command #9 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand10">Type of command #10 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand11">Type of command #11 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand12">Type of command #12 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand13">Type of command #13 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand14">Type of command #14 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand15">Type of command #15 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15>
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
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 

		/// <summary>
        /// Configures saver for channel
        /// </summary>
        /// <typeparam name="TFeature">Feature type</typeparam>		 
		/// <typeparam name="TCommand1">Type of command #1 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand2">Type of command #2 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand3">Type of command #3 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand4">Type of command #4 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand5">Type of command #5 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand6">Type of command #6 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand7">Type of command #7 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand8">Type of command #8 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand9">Type of command #9 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand10">Type of command #10 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand11">Type of command #11 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand12">Type of command #12 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand13">Type of command #13 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand14">Type of command #14 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand15">Type of command #15 that is being supported by feature</typeparam> 
		/// <typeparam name="TCommand16">Type of command #16 that is being supported by feature</typeparam> 
        /// <param name="cf">Channel configuration</param>
        /// <param name="feature">Command feature instance</param>
        /// <param name="saver">Corresponding saver instance</param>
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15, TCommand16>(this ChannelBinding<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15, TCommand16> saver)
            where TFeature : CommandFeature, Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15, TCommand16>
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
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
	}
}

