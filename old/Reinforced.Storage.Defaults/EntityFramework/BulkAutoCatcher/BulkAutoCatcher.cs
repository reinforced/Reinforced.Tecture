using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Defaults.EntityFramework.BulkAutoCatcher
{
    public static class BulkAutoExtensions
    {
        public static CatchingEffects<BulkAutoCatcher> BulkAuto(this Effects e, string annotation = null)
        {
            return e.Catch(new BulkAutoCatcher(), annotation);
        }

        public static CatchingEffects<BulkAutoCatcher> BulkAuto(this IModifyableStorage e, string annotation = null)
        {
            return e.Effects.BulkAuto(annotation);
        }
    }

    public class BulkAutoCatcher : ISideEffectsCatcher
    {
        private readonly ChangesBatchSideEffect _sideEffect = new ChangesBatchSideEffect();

        /// <summary>
        /// Catches side effect
        /// </summary>
        /// <param name="effect">Side effect that must be caught</param>
        public void Catch(SideEffectBase effect)
        {
            if (effect is AddSideEffect add)
            {
                _sideEffect.Add(add.Entity);
                return;
            }

            if (effect is RemoveSideEffect rem)
            {
                _sideEffect.Remove(rem.Entity);
                return;
            }

            if (effect is UpdateSideEffect upd)
            {
                _sideEffect.Update(upd.Entity, upd.PropertiesToUpdate);
                return;
            }

            if (effect is ChangesBatchSideEffect chb)
            {
                _sideEffect.MergeChanges(chb);
                return;
            }

            throw new Exception($"Unbatchable side-effect {effect}");
        }

        /// <summary>
        /// Produces resulting side effect of caught ones
        /// </summary>
        /// <returns>Resulting side effect</returns>
        public SideEffectBase Produce()
        {
            return _sideEffect;
        }
    }
}
