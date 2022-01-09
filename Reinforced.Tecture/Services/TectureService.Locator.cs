namespace Reinforced.Tecture.Services
{
    /// <summary>
    /// Base class for storage service
    /// </summary>
    public partial class TectureServiceBase
    {
        /// <summary>
        /// Obtains instance of service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        protected T Do<T>() where T : TectureServiceBase
        {
            return ServiceManager.Do<T>();
        }

        /// <summary>
        /// Obtains instance of service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        protected T Let<T>() where T : TectureServiceBase
        {
            return Do<T>();
        }
    }
}
