using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Stories
{
    class StoryCommandDispatcher : CommandsDispatcher, ISaver
    {
        public StoryCommandDispatcher()
        {
            RegisterSaver(this);
        }

        private Queue<CommandBase> _story;
        private bool _isStoryActive = false;
        internal void BeginStory()
        {
            _isStoryActive = true;
            if (_story != null) _story.Clear();
            _story = new Queue<CommandBase>();
        }

        internal StorageStory EndStory(TestingEnvironment env)
        {
            _isStoryActive = false;
            return new StorageStory(_story, env);
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

        protected override void DispatchInternal(IEnumerable<CommandBase> effects)
        {
            if (!_isStoryActive)
            {
                base.DispatchInternal(effects);
                return;
            }
            var e = effects.ToArray();
            foreach (var CommandBase in e)
            {
                _story.Enqueue(CommandBase);
            }
            base.DispatchInternal(e);
        }

        protected override Task DispatchInternalAsync(IEnumerable<CommandBase> effects)
        {
            if (!_isStoryActive) return base.DispatchInternalAsync(effects);
            var e = effects.ToArray();
            foreach (var CommandBase in e)
            {
                _story.Enqueue(CommandBase);
            }
            return base.DispatchInternalAsync(e);
        }
    }
}
