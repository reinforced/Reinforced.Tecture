using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture
{
    /// <summary>
    /// Tecture facade
    /// </summary>
    public interface ITecture
    {
        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        T Do<T>() where T : TectureService, INoContext;

        /// <summary>
        /// Obtains context service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Context service <typeparamref name="T"/></returns>
        LetBuilder<T> Let<T>() where T : TectureService, IWithContext;
    }
}
