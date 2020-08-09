using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.Stories
{
    public class StorageAssertionExpection : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        public StorageAssertionExpection(string message) : base("Storage assertion failed: " + message)
        {
        }
    }

    public abstract class SideEffectAssertion
    {
        public abstract bool IsValidEffect(SideEffectBase effect);
        public abstract void Assert(SideEffectBase effect);

        public abstract Type SideEffectType { get; }

        public TestingEnvironment Environment { get; internal set; }
    }

    
    public abstract class CommandCheck<TEffect> : SideEffectAssertion where TEffect : SideEffectBase
    {
        public abstract string GetMessage(TEffect command);

        public abstract bool IsActuallyValid(TEffect effect);

        public void Assert(TEffect effect)
        {
            if (!IsActuallyValid(effect)) throw new StorageAssertionExpection(GetMessage(effect));
        }

        public override void Assert(SideEffectBase effect)
        {
            if (effect is TEffect) Assert((TEffect)effect);
            
        }

        public override Type SideEffectType
        {
            get { return typeof(TEffect); }
        }

        public override bool IsValidEffect(SideEffectBase effect)
        {
            if (effect is TEffect) return IsActuallyValid((TEffect) effect);
            return false;
        }
    }
}
