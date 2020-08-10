using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Reinforced.Tecture.Query
{
    public class Hashbox : IDisposable
    {
        private const string NIL = "nil";
        private const string REF = "ref";

        /// <summary>
        /// Hash set of all numeric types
        /// </summary>
        public static readonly HashSet<Type> NumericTypes = new HashSet<Type>(new[]
        {
            typeof (byte),
            typeof (sbyte),
            typeof (short),
            typeof (ushort),
            typeof (int),
            typeof (uint),
            typeof (long),
            typeof (ulong),
            typeof (float),
            typeof (double),
            typeof (decimal),
            typeof (byte?),
            typeof (sbyte?),
            typeof (short?),
            typeof (ushort?),
            typeof (int?),
            typeof (uint?),
            typeof (long?),
            typeof (ulong?),
            typeof (float?),
            typeof (double?),
            typeof (decimal?)
        });

        private readonly MemoryStream _ms;
        private readonly BinaryWriter _bw;

        /// <summary>
        /// Binary writer containing sensible data to hash
        /// </summary>
        public BinaryWriter Writer
        {
            get { return _bw; }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Hashbox()
        {
            _ms = new MemoryStream();
            _bw = new BinaryWriter(_ms);
        }
        private readonly Dictionary<object,long> _refs = new Dictionary<object, long>();

        private void Remember(object o)
        {
            _refs[o] = _ms.Position;
        }

        private bool IsSimple(Type t)
        {
            if (t == typeof(string)) return true;
            if (t == typeof(bool) || t==typeof(bool?)) return true;
            if (t == typeof(DateTime) || t == typeof(DateTime?)) return true;
            if (t == typeof(Guid) || t == typeof(Guid?)) return true;
            if (NumericTypes.Contains(t)) return true;
            if (t.GetTypeInfo().IsEnum) return true;
            return false;
        }

        private void PutObjet(object o)
        {
            var t = o.GetType();
            var props = t.GetRuntimeProperties();
            foreach (var pi in props)
            {
                if (IsSimple(pi.PropertyType))
                {
                    Put(pi.GetValue(o));
                }
            }
        }

        public void Put(object o)
        {
            if (o == null) { PutNull(); return; }
            
            if (o is DateTime dt) { Put(dt.GetHashCode()); return; }
            if (o is string s) { Put(s); return; }
            if (o is bool b) { Put(b); return; }
            if (o is Guid g) { Put(g); return; }

            var to = o.GetType();
            if (NumericTypes.Contains(to)) { PutNumber(o); return; }
            if (to.GetTypeInfo().IsEnum) { Put((long)o); return; }

            if (_refs.ContainsKey(o))
            {
                Put(REF);
                Put(_refs[o]);
                return;
            }

            if (o is IEnumerable ie)
            {
                foreach (var item in ie)
                {
                    Put(item);
                }
            }

            if (o is IHasheable h)
            {
                h.WriteData(this);
            }
            else
            {
                PutObjet(o);
            }

            Remember(o);
        }

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
                sb.AppendFormat("{0:0X}", b);
            }

            return sb.ToString();

        }

        private void PutNumber(object value)
        {
            if (value is byte t_byte) { Put(t_byte); return; }
            if (value is sbyte t_sbyte) { Put(t_sbyte); return; }
            if (value is short t_short) { Put(t_short); return; }
            if (value is ushort t_ushort) { Put(t_ushort); return; }
            if (value is int t_int) { Put(t_int); return; }
            if (value is uint t_uint) { Put(t_uint); return; }
            if (value is long t_long) { Put(t_long); return; }
            if (value is ulong t_ulong) { Put(t_ulong); return; }
            if (value is float t_float) { Put(t_float); return; }
            if (value is double t_double) { Put(t_double); return; }
            if (value is decimal t_decimal) { Put(t_decimal); return; }
        }
        public void Put(Guid value) { _bw.Write(value.ToByteArray()); }
        public void Put(Guid? value) { if (value.HasValue) _bw.Write(value.Value.ToByteArray()); else PutNull(); }
        public void Put(byte value) { _bw.Write(value); }
        public void Put(sbyte value) { _bw.Write(value); }
        public void Put(short value) { _bw.Write(value); }
        public void Put(ushort value) { _bw.Write(value); }
        public void Put(int value) { _bw.Write(value); }
        public void Put(uint value) { _bw.Write(value); }
        public void Put(long value) { _bw.Write(value); }
        public void Put(ulong value) { _bw.Write(value); }
        public void Put(float value) { _bw.Write(value); }
        public void Put(double value) { _bw.Write(value); }
        public void Put(decimal value) { _bw.Write(value); }
        public void Put(bool value) { _bw.Write(value); }
        public void Put(byte? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(sbyte? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(short? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(ushort? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(int? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(uint? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(long? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(ulong? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(float? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(double? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(decimal? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }
        public void Put(bool? value) { if (value.HasValue) _bw.Write(value.Value); else PutNull(); }

        public void PutNull()
        {
            Put(NIL);
        }

        public void Put(string s)
        {
            _bw.Write(s);
        }


        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _ms?.Dispose();
            _bw?.Dispose();
        }
    }
}
