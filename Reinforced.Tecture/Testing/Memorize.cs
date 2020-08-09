using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing
{
    public interface IMemorizing
    {
        void Memorize(CommandBase seb);
    }

    /// <summary>
    /// Helper class that will help you to memorize some command instance
    /// and use it later.
    ///
    /// E.g. you can pass memorize to some checkers and receive instance of command that
    /// they are checking. And check its contents later.
    ///
    /// Memorize type is not limited to Command because basically it can memorize everything
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public class Memorize<T>
    {
        public Memorize(T value)
        {
            Value = value;
        }

        public void SetValue(object value)
        {
            if (value is T)
            {
                SetValue((T)value);
                return;
            }
            throw new Exception($"Invalid memorizable value:{value}. Expected {typeof(T).Name}");
        }

        public void SetValue(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }

        public static implicit operator T(Memorize<T> mem)
        {
            return mem.Value;
        }

        public static explicit operator Memorize<T>(T value)
        {
            return new Memorize<T>(value);
        }
    }
}
