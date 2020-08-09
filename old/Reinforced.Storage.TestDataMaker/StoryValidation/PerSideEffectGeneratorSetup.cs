using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public interface ISideEffectGeneratorSetup
    {
        HashSet<IValidationGenerator> Generators { get; }
    }
    public class PerSideEffectGeneratorSetup<T> : ISideEffectGeneratorSetup where T : SideEffectBase
    {
        
        public PerSideEffectGeneratorSetup()
        {
            Generators = new HashSet<IValidationGenerator>();
        }

        public PerSideEffectGeneratorSetup<T> Register(ValidationGenerator<T> gen)
        {
            if (!Generators.Contains(gen)) Generators.Add(gen);
            return this;
        }

        public HashSet<IValidationGenerator> Generators { get; }
    }
}
