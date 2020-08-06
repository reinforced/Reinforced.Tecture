using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Tecture.CleanPlayground.Models;

namespace Reinforced.Tecture.CleanPlayground
{
    class SampleTestData : CSharpTestDataProvider
    {
        private User[] GetEntry_1()
        {
            var v1 = new User()
                { Id = 0, FirstName = @"Vasya", LastName = @"Pupkin", BirthDate = new DateTime(2020, 8, 6, 21, 37, 38, 127, DateTimeKind.Utc), Gender = Gender.Male };
            var v2 = new Order()
                { Id = 10, Title = @"aaa", UserId = 10 };
            v2.User = v1;
            v1.Orders = new List<Order> { v2 };
            return new User[] { v1 };
        }

        public override IEnumerable<ITestDataRecord> GetRecords()
        {
            yield return new TestDataRecord<User[]> { Hash = @"adsfasdfasdf", Description = @"", Data = GetEntry_1() };
        }
    }
}