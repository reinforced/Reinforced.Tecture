
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Testing.Builders
{
	public static partial class Extensions
    {
 
		public static void ForCommand<TFeature, TCommand1>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1> saver)
            where TFeature : Produces<TCommand1>
            where TCommand1 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 
		public static void ForCommand<TFeature, TCommand1, TCommand2>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2> saver)
            where TFeature : Produces<TCommand1, TCommand2>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4>
            where TCommand1 : CommandBase
where TCommand2 : CommandBase
where TCommand3 : CommandBase
where TCommand4 : CommandBase

        {
            var holder = cf as MultiplexerRegistrationDecorator;
            holder.RegisterCommandFeature(typeof(TFeature), feature);
            holder.RegisterSaver(saver);
        }
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15>
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
 
		public static void ForCommand<TFeature, TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15, TCommand16>(this TestingChannelConfiguration<CommandChannel<TFeature>> cf, TFeature feature, Saver<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15, TCommand16> saver)
            where TFeature : Produces<TCommand1, TCommand2, TCommand3, TCommand4, TCommand5, TCommand6, TCommand7, TCommand8, TCommand9, TCommand10, TCommand11, TCommand12, TCommand13, TCommand14, TCommand15, TCommand16>
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

