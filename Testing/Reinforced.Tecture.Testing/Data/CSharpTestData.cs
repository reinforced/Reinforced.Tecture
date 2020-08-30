using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Testing.Data
{
    public abstract class CSharpTestData : ITestDataSource
    {
        private class HashWarning
        {
            public int Position { get; set; }
            public string Description { get; set; }
            public string Expected { get; set; }
            public string Actual { get; set; }

        }

        private readonly bool _hashOnlyWarn = false;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        protected CSharpTestData(bool hashOnlyWarn = false)
        {
            _hashOnlyWarn = hashOnlyWarn;
        }

        public T New<T>()
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (Exception ex)
            {
                return (T) typeof(T).InstanceNonpublic();
            }
        }

        public void Set<T, V>(T target, Expression<Func<T, V>> prop, V value)
        {
            if (prop.Body is MemberExpression mex)
            {
                if (mex.Member is PropertyInfo pi)
                {
                    if (pi.CanWrite)
                    {
                        pi.SetValue(target, value);
                    }
                }
            }
        }

        public abstract IEnumerable<ITestDataRecord> GetRecords();

        private IEnumerator<ITestDataRecord> _en = null;

        private void EnsureEnumerator()
        {
            if (_en == null)
            {
                _en = GetRecords().GetEnumerator();
            }
        }

        private int _counter = 0;

        private readonly Dictionary<int, HashWarning> _warnings = new Dictionary<int, HashWarning>();

        public T Get<T>(string hash, string description = null)
        {
            EnsureEnumerator();
            if (!_en.MoveNext())
            {
                throw new TestDataEndsException(_counter);
            }

            var k = _en.Current;
            if (hash != _en.Current.Hash)
            {
                if (!_hashOnlyWarn)
                {
                    if (!string.IsNullOrEmpty(k.Description) && !string.IsNullOrEmpty(description))
                    {
                        throw new QueryOrderMismatchException(_counter, k.Description, description);
                    }
                    throw new TestHashMismatchException(_counter, k.Description, hash, k.Hash);
                }
                else
                {
                    _warnings[_counter] = new HashWarning() { Position = _counter, Actual = hash, Expected = k.Hash };
                }
            }

            if (k is AnonymousTestDataRecord tr)
            {
                if (!typeof(T).IsAnonymousType())
                {
                    throw new TestDataTypeMismatchException(_counter, k.Description, typeof(T), typeof(void));
                }

                return tr.OfType<T>();
            }
            var data = k as ITestDataRecord<T>;
            if (data == null)
            {
                throw new TestDataTypeMismatchException(_counter, k.Description, typeof(T), k.Payload != null ? k.Payload.GetType() : typeof(void));
            }



            return data.Data;
        }

        public string WarnigsText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                var allWarnings = _warnings.Values.OrderBy(x => x.Position);
                foreach (var hashWarning in allWarnings)
                {
                    if (string.IsNullOrEmpty(hashWarning.Description))
                    {
                        sb.AppendLine(
                            $"At [{hashWarning.Position}] hash mismatch: '{hashWarning.Expected}' / '{hashWarning.Actual}'");
                    }
                    else
                    {
                        sb.AppendLine(
                            $"At [{hashWarning.Position}][{hashWarning.Description}] hash mismatch: '{hashWarning.Expected}' / '{hashWarning.Actual}'");
                    }
                }

                return sb.ToString();
            }
        }
    }
}
