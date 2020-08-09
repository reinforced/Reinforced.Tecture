namespace Reinforced.Storage.Services
{
    /// <summary>
    /// Base class for storage service
    /// </summary>
    public partial class StorageService
    {
        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        public T Do<T>() where T : StorageService, INoContext
        {
            return ServiceManager.Do<T>();
        }

        /// <summary>
        /// Obtains context service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Context service <typeparamref name="T"/></returns>
        public LetBuilder<T> Let<T>() where T : StorageService, IWithContext
        {
            return ServiceManager.Let<T>();
        }
    }
}
