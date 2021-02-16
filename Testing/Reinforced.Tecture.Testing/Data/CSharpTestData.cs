using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Reinforced.Tecture.Testing.Data
{
    /// <summary>
    /// Test data based on C# code
    /// </summary>
    public abstract class CSharpTestData : ITestDataSource
    {
        private class HashWarning
        {
            public int Position { get; set; }
            public string Description { get; set; }
            public string Expected { get; set; }
            public string Actual { get; set; }

        }

        private readonly bool _hashOnlyWarn;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        protected CSharpTestData(bool hashOnlyWarn = false)
        {
            _hashOnlyWarn = hashOnlyWarn;
        }

        /// <summary>
        /// Creates instance of type <typeparamref name="T"/> via reflection
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>New instance of <typeparamref name="T"/></returns>
        public T New<T>()
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (Exception)
            {
                return (T)typeof(T).InstanceNonpublic();
            }
        }

        /// <summary>
        /// Sets property of <typeparamref name="T"/> via reflection
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="V">Property value type</typeparam>
        /// <param name="target">Target instance of <typeparamref name="T"/></param>
        /// <param name="prop">Property being set</param>
        /// <param name="value">Desired property value</param>
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

        /// <summary>
        /// Consequent set of responses to the queries
        /// </summary>
        /// <returns>Set of test data records</returns>
        public abstract IEnumerable<ITestDataRecord> GetRecords();

        private IEnumerator<ITestDataRecord> _en;

        private void EnsureEnumerator()
        {
            if (_en == null)
            {
                _en = GetRecords().GetEnumerator();
            }
        }

        private int _counter = 0;

        private readonly Dictionary<int, HashWarning> _warnings = new Dictionary<int, HashWarning>();

        /// <inheritdoc />
        public T Get<T>(string hash, string description = null)
        {
            EnsureEnumerator();
            if (!_en.MoveNext())
            {
                throw new TestDataEndsException(_counter);
            }

            var k = _en.Current;
            if (k == null)
            {
                throw new TestDataEndsException(_counter);
            }
            if (hash != k.Hash)
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

        /// <summary>
        /// Collected warnings regarding test data hash mismatch
        /// </summary>
        public string WarningsText
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
