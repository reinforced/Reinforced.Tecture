namespace Reinforced.Tecture.Tracing.Promises
{
    /// <summary>
    /// Promised query that demands data for testing purposes
    /// </summary>
    /// <typeparam name="T">Test data</typeparam>
    public interface NotifyCompleted<T> : Promised<T>
    {
       /// <summary>
       /// Notifies that promised request is successfully completed
       /// </summary>
       /// <param name="description">Query description</param>
       void Fulfill(string description = null);
    }
}