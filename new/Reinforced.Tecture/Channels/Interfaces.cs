using System;

namespace Reinforced.Tecture.Channels
{

    /// <summary>
    /// Common channel interfact
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
    /// <typeparam name="T">Type of supported feature</typeparam>
    public interface QueryChannel<out T> : CanQuery where T : QueryFeature { }

    /// <summary>
    /// Flag type for data channels that supports particular methodology
    /// </summary>
    /// <typeparam name="T">Type of supported feature</typeparam>
    public interface CommandChannel<out T> : CanCommand where T : CommandFeature { }

    /// <summary>
    /// Flag type for Command/Query channels
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    public interface CommandQueryChannel<out TQuery, out TCommand> : QueryChannel<TQuery>, CommandChannel<TCommand> where TCommand : CommandFeature where TQuery : QueryFeature { }
}
