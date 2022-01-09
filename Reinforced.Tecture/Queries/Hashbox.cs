using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantJumpStatement

namespace Reinforced.Tecture.Queries
{
    /// <summary>
    /// Utility class for SHA-256 query hashes computing
    /// </summary>
    public class Hashbox : IDisposable
    {
        private const string NIL = "nil";
        private const string REF = "ref";

        /// <summary>
        /// Hash set of all numeric types
        /// </summary>
        public static readonly HashSet<Type> NumericTypes = new HashSet<Type>(new[]
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(byte?),
            typeof(sbyte?),
            typeof(short?),
            typeof(ushort?),
            typeof(int?),
            typeof(uint?),
            typeof(long?),
            typeof(ulong?),
            typeof(float?),
            typeof(double?),
            typeof(decimal?)
        });

        private readonly MemoryStream _ms;
        private readonly BinaryWriter _bw;

        /// <summary>
        /// Binary writer containing sensible data to hash
        /// </summary>
        public BinaryWriter Writer => _bw;

        private readonly bool _debugMode;
        public List<object> ObjectsLog { get; } = null;

        private void Log(object entry)
        {
            if (_debugMode)
            {
                ObjectsLog.Add(entry);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Hashbox(bool debugMode = false)
        {
            _debugMode = debugMode;
            if (_debugMode)
            {
                ObjectsLog = new List<object>();
            }
            _ms = new MemoryStream();
            _bw = new BinaryWriter(_ms);
        }

        private readonly Dictionary<object, long> _refs = new Dictionary<object, long>();

        private void Remember(object o)
        {
            _refs[o] = _ms.Position;
        }


        private void PutObjet(object o)
        {
            var t = o.GetType();
            if (t.IsAnonymousType())
            {
                var fields = t.GetRuntimeFields();
                foreach (var pi in fields)
                {
                    Put(pi.GetValue(o));
                }

                return;
            }

            var props = t.GetRuntimeProperties().ToArray();
            if (props.Length > 0)
            {
                foreach (var pi in props)
                {
                    Put(pi.GetValue(o));
                }
            }
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(object value)
        {
            if (value == null)
            {
                PutNull();
                return;
            }


            if (value is string s)
            {
                Put(s);
                return;
            }

            if (value is bool b)
            {
                Put(b);
                return;
            }

            if (value is Guid g)
            {
                Put(g);
                return;
            }

            var to = value.GetType();
            if (NumericTypes.Contains(to))
            {
                PutNumber(value);
                return;
            }

            if (to.GetTypeInfo().IsEnum)
            {
                Put(Convert.ToInt64(value));
                return;
            }

            if (value is DateTime dt)
            {
                Put(dt.GetHashCode());
                return;
            }

            if (value is DateTimeOffset dto)
            {
                Put(dto.GetHashCode());
                return;
            }

            if (value is TimeSpan ts)
            {
                Put(ts.GetHashCode());
                return;
            }

            if (_refs.ContainsKey(value))
            {
                Put(REF);
                Put(_refs[value]);
                return;
            }

            if (value is IEnumerable ie)
            {
                foreach (var item in ie)
                {
                    Put(item);
                }
                
            }
            else
            {

                if (value is IHashable h)
                {
                    h.WriteData(this);
                }
                else
                {
                    PutObjet(value);
                }
            }

            Remember(value);
        }

        /// <summary>
        /// Computes hash of collected values and return string result
        /// </summary>
        /// <returns>Hash of collected data</returns>
        public string Compute()
        {
            _bw.Flush();
            _ms.Flush();
            var bytes = _ms.ToArray();


            byte[] hash;
            using (var sha = SHA256.Create())
            {
                hash = sha.ComputeHash(bytes);
            }

            StringBuilder sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.AppendFormat("{0:X1}", b);
            }

            return sb.ToString();
        }

        private void PutNumber(object value)
        {
            if (value is byte t_byte)
            {
                Put(t_byte);
                return;
            }

            if (value is sbyte t_sbyte)
            {
                Put(t_sbyte);
                return;
            }

            if (value is short t_short)
            {
                Put(t_short);
                return;
            }

            if (value is ushort t_ushort)
            {
                Put(t_ushort);
                return;
            }

            if (value is int t_int)
            {
                Put(t_int);
                return;
            }

            if (value is uint t_uint)
            {
                Put(t_uint);
                return;
            }

            if (value is long t_long)
            {
                Put(t_long);
                return;
            }

            if (value is ulong t_ulong)
            {
                Put(t_ulong);
                return;
            }

            if (value is float t_float)
            {
                Put(t_float);
                return;
            }

            if (value is double t_double)
            {
                Put(t_double);
                return;
            }

            if (value is decimal t_decimal)
            {
                Put(t_decimal);
                return;
            }
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(Guid value)
        {
            Log(value);
            _bw.Write(value.ToByteArray());
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(Guid? value)
        {
            if (value.HasValue) _bw.Write(value.Value.ToByteArray());
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(byte value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(sbyte value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(short value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(ushort value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(int value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(uint value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(long value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(ulong value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(float value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(double value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(decimal value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(bool value)
        {
            Log(value);
            _bw.Write(value);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(byte? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(sbyte? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(short? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(ushort? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(int? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(uint? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(long? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(ulong? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(float? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(double? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(decimal? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(bool? value)
        {
            if (value.HasValue)
            {
                Log(value.Value);
                _bw.Write(value.Value);
            }
            else PutNull();
        }

        /// <summary>
        /// Puts null value into hashbox
        /// </summary>
        public void PutNull()
        {
            Put(NIL);
        }

        /// <summary>
        /// Puts value into hashbox
        /// </summary>
        /// <param name="value">Value to be reflected in the hash</param>
        public void Put(string value)
        {
            if (value == null) PutNull();
            else
            {
                Log(value);
                _bw.Write(value);
            }
        }


        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _ms?.Dispose();
            _bw?.Dispose();
        }
    }
}