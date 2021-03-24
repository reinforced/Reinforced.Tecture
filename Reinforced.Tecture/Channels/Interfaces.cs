// ReSharper disable UnusedTypeParameter
namespace Reinforced.Tecture.Channels
{

    /// <summary>
    /// Common channel interface
    /// </summary>
    public interface Channel { }

    /// <summary>
    /// Flag type for data channels that can be queried 
    /// </summary>
    public interface CanQuery : Channel { }

    /// <summary>
    /// Flag type for data channels that can be addressed with commands
    /// </summary>
    public interface CanCommand : Channel { }

    /// <summary>
    /// Flag type for data channels that supports particular methodology
    /// </summary>
    /// <typeparam name="T">Type of supported aspect</typeparam>
    public interface QueryChannel<out T> : CanQuery where T : QueryAspect { }

    /// <summary>
    /// Flag type for data channels that supports particular methodology
    /// </summary>
    /// <typeparam name="T">Type of supported aspect</typeparam>
    public interface CommandChannel<out T> : CanCommand where T : CommandAspect { }

    /// <summary>
    /// Flag type for Command/Query channels
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    public interface CommandQueryChannel<out TCommand,out TQuery> : QueryChannel<TQuery>, CommandChannel<TCommand> where TCommand : CommandAspect where TQuery : QueryAspect { }

    /// <summary>
    /// Nil (empty) channel
    /// </summary>
    public interface Channelless : Channel { }
}
