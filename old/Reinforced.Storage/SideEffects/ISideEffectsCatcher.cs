using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.SideEffects
{
    /// <summary>
    /// Thing that allows to catch various side effects and aggregate it to another side effect
    /// </summary>
    public interface ISideEffectsCatcher
    {
        /// <summary>
        /// Catches side effect
        /// </summary>
        /// <param name="effect">Side effect that must be caught</param>
        void Catch(SideEffectBase effect);

        /// <summary>
        /// Produces resulting side effect of caught ones
        /// </summary>
        /// <returns>Resulting side effect</returns>
        SideEffectBase Produce();
    }
}
