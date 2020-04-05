using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands.Exact;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Checks
{
    public class CommentCheck : CommandCheck<CommentCommand>
    {
        private readonly string _content;

        internal CommentCheck(string content)
        {
            _content = content;
        }

        protected override string GetMessage(CommentCommand command)
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

        protected override bool IsActuallyValid(CommentCommand effect)
        {
            if (effect == null) return false;
            if (!string.IsNullOrEmpty(_content)) return effect.Annotation == _content;
            return true;
        }
    }
}
