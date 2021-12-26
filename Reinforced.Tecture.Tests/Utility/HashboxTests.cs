using System;
using System.Collections.Concurrent;
using Reinforced.Tecture.Queries;
using Xunit;

namespace Reinforced.Tecture.Tests.Utility
{
    public class HashboxTests
    {
        [Fact]
        public void HashBox_Works_As_Expected_On_Simple_Types()
        {
            var hb = new Hashbox();
            
            hb.Put(1);
            hb.Put((byte)1);
            hb.Put((sbyte)1);
            hb.Put((char)1);
            hb.Put((short)1);
            hb.Put((ushort)1);
            hb.Put((long)1);
            hb.Put((ulong)1);
            hb.Put((uint)1);
            hb.Put((float)1);
            hb.Put((double)1);
            hb.Put((decimal)1);
            hb.Put((int?)1);
            hb.Put((int?)null);
            hb.Put((byte?)null);
            hb.Put((byte?)1);
            hb.Put((sbyte?)null);
            hb.Put((sbyte?)1);
            hb.Put((char?)null);
            hb.Put((char?)1);
            hb.Put((short?)null);
            hb.Put((short?)1);
            hb.Put((ushort?)null);
            hb.Put((ushort?)1);
            hb.Put((long?)null);
            hb.Put((long?)1);
            hb.Put((ulong?)null);
            hb.Put((ulong?)1);
            hb.Put((uint?)null);
            hb.Put((uint?)1);
            hb.Put((float?)null);
            hb.Put((float?)1);
            hb.Put((double?)null);
            hb.Put((double?)1);
            hb.Put((decimal?)1);
            hb.Put((decimal?)null);
            hb.Put(Guid.Empty);
            hb.Put((Guid?)Guid.Empty);
            hb.Put((Guid?)null);

            hb.Put(false);
            hb.Put((bool?)false);
            hb.Put((bool?)null);
            
            hb.Put("aaaa");
            hb.Put((string)null);
            
            var w = hb.Writer;
            const string expected = "9AAD24025A0E2BD8AAFE4A8EF2DFADF8EA84DF28038E8921E2F1081E59410";
            var result = hb.Compute();
            
            Assert.Equal(expected,result);

        }

        enum TestEnum { One,Two,Three }
        
        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Address Address { get; set; }
            public DateTime RegDate { get; set; }
        }

        class Address
        {
            public TestEnum EnumValue { get; set; }
            public string City { get; set; }
        }
        
        [Fact]
        public void HashBox_Works_As_Expected_On_Complex_Types()
        {
            var user = new User()
            {
                Name = "Test",
                Age = 40,
                Address = new Address()
                {
                    EnumValue = TestEnum.Two,
                    City = "Moscow"
                },
                RegDate = new DateTime(2001,10,10)
            };

            var hb = new Hashbox();
            hb.Put(user);

            const string expected = "D04DEBDF206BFAB6D61EE7CAB883DFD61AAF7C46BD73045FABDFCCDD701E32";

            var result = hb.Compute();
            
            Assert.Equal(expected,result);
        }
    }
}