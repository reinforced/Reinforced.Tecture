using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Testing.Stories.Common
{
    public class CommentCheck : CommandCheck<CommentSideEffect>
    {
        private readonly string _content;

        public CommentCheck(string content)
        {
            _content = content;
        }

        public override string GetMessage(CommentSideEffect command)
        {
            var msg = string.IsNullOrEmpty(_content)
                ? $"comment"
                : $"comment '{_content}'";
            if (command == null)
            {
                return $"{msg} expected here, but story unexpectedly ends";
            }

            if (!string.IsNullOrEmpty(_content) && command.Annotation != _content)
            {
                return $"{msg} expected here, but story unexpectedly ends";
            }

            return null;
        }

        public override bool IsActuallyValid(CommentSideEffect effect)
        {
            if (effect == null) return false;
            if (!string.IsNullOrEmpty(_content)) return effect.Annotation == _content;
            return true;
        }
    }
}
