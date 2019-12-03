using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.Stories
{
    class StorySideEffectDispatcher : SideEffectsDispatcher, ISideEffectSaver
    {
        public StorySideEffectDispatcher()
        {
            RegisterSaver(this);
        }

        private Queue<SideEffectBase> _story;
        private bool _isStoryActive = false;
        internal void BeginStory()
        {
            _isStoryActive = true;
            if (_story!=null) _story.Clear();
            _story = new Queue<SideEffectBase>();
        }

        internal StorageStory EndStory(TestingEnvironment env)
        {
            _isStoryActive = false;
            return new StorageStory(_story,env);
        }

        /// <summary>
        /// Saves data
        /// </summary>
        public void Save()
        {
            _story.Enqueue(new SaveChangesSideEffect());
        }

        /// <summary>
        /// Saves data asynchronously
        /// </summary>
        /// <returns></returns>
        public Task SaveAsync()
        {
            Save();
            return Task.FromResult(0);
        }

        protected override void DispatchInternal(IEnumerable<SideEffectBase> effects)
        {
            if (!_isStoryActive)
            {
                base.DispatchInternal(effects);
                return;
            }
            var e = effects.ToArray();
            foreach (var sideEffectBase in e)
            {
                _story.Enqueue(sideEffectBase);
            }
            base.DispatchInternal(e);
        }

        protected override Task DispatchInternalAsync(IEnumerable<SideEffectBase> effects)
        {
            if (!_isStoryActive) return base.DispatchInternalAsync(effects);
            var e = effects.ToArray();
            foreach (var sideEffectBase in e)
            {
                _story.Enqueue(sideEffectBase);
            }
            return base.DispatchInternalAsync(e);
        }
    }
}
