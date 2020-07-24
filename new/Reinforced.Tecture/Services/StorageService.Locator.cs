namespace Reinforced.Tecture.Services
{
    /// <summary>
    /// Base class for storage service
    /// </summary>
    public partial class TectureServiceBase
    {
        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        protected T Do<T>() where T : TectureServiceBase, INoContext
        {
            return ServiceManager.Do<T>();
        }

        /// <summary>
        /// Obtains context service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Context service <typeparamref name="T"/></returns>
        protected LetBuilder<T> Let<T>() where T : TectureServiceBase, IWithContext
        {
            return ServiceManager.Let<T>();
        }
    }
}
