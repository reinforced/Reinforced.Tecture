using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Testing.Data
{
    public abstract class CSharpTestDataProvider : Providing
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
        protected CSharpTestDataProvider(bool hashOnlyWarn = false)
        {
            _hashOnlyWarn = hashOnlyWarn;
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

        public T Get<T>(string hash)
        {
            EnsureEnumerator();
            if (!_en.MoveNext())
            {
                throw new TestDataEndsException(_counter);
            }
            var data = _en.Current as TestDataRecord<T>;
            if (data==null)
            {
                throw new TestDataTypeMismatchException(_counter, data.Description, typeof(T), data.Data != null ? data.Data.GetType() : typeof(void));
            }

            if (hash != data.Hash)
            {
                if (!_hashOnlyWarn) throw new TestHashMismatchException(_counter, data.Description, hash, data.Hash);
                else
                {
                    _warnings[_counter] = new HashWarning() { Position = _counter, Actual = hash, Expected = data.Hash };
                }
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
