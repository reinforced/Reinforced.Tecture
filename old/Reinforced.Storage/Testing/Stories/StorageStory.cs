using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.Stories
{
    /// <summary>
    /// Story of applied side effects.
    /// Story can be shown as text or formally validated.
    /// You cannot construct story directly
    /// </summary>
    public class StorageStory
    {
        private readonly SideEffectBase[] _effects;
        internal readonly TestingEnvironment _environment;
        /// <summary>
        /// Effects that story consists of (order matters)
        /// </summary>
        public IEnumerable<SideEffectBase> Effects
        {
            get { return _effects; }
        }

        internal StorageStory(Queue<SideEffectBase> effects, TestingEnvironment environment)
        {
            _environment = environment;
            var nq = new Queue<SideEffectBase>(effects);
            SideEffectBase[] effectsArray = new SideEffectBase[effects.Count];
            int i = 0;
            while (nq.Count>0)
            {
                effectsArray[i++] = nq.Dequeue();
            }
            _effects = effectsArray;
        }

        /// <summary>
        /// Begins story validation
        /// </summary>
        /// <returns>Story validator</returns>
        public StoryValidator Begins()
        {
            return new StoryValidator(this);
        }

        /// <summary>
        /// Turns story into human-readable text
        /// </summary>
        /// <param name="tw">Result writer</param>
        /// <param name="codes">Sets whether it is needed to output side-effect codes</param>
        public void ToText(TextWriter tw, bool codes = true)
        {
            int i = 1;
            foreach (var effect in _effects)
            {
                if (effect is SaveChangesSideEffect)
                {
                    effect.Describe(tw);
                    tw.WriteLine();
                    tw.WriteLine();
                    i = 1;
                }
                else
                {
                    tw.Write($"{i}. ");
                    if (codes)
                    {
                        var ca = effect.GetType().GetCustomAttribute<SideEffectCodeAttribute>();
                        if (ca != null) tw.Write($"[{ca.Code}] ");
                    }
                    effect.Describe(tw);
                    tw.WriteLine();
                    tw.WriteLine();
                    i++;
                }
            }
        }

        /// <summary>
        /// Turns story into human-readable text
        /// </summary>
        /// <param name="codes">Sets whether it is needed to output side-effect codes</param>
        /// <returns>Story textual representation</returns>
        public string ToText(bool codes = true)
        {
            StringBuilder sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                ToText(tw);
            }

            return sb.ToString();
        }
    }
}
