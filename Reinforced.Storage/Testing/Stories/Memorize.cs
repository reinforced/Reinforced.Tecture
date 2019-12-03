using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Testing.Stories
{
    public interface IMemorizing
    {
        void Memorize(SideEffectBase seb);
    }
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
