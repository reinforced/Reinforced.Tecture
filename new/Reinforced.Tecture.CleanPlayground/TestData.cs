using System.Collections;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Tecture.CleanPlayground.Models;
using System;
using System.Collections.Generic;

namespace Test.Data
{
    class TestData : CSharpTestDataProvider
    {
        private User GetEntry_1()
        {
            return new User()
            { Id = 0, FirstName = @"Vasya", LastName = @"Pupkin", BirthDate = new DateTime(2020, 8, 6, 17, 34, 14, 267, DateTimeKind.Utc), Gender = Gender.Male };
        }

        private User GetEntry_2()
        {
            return new User()
            { Id = 0, FirstName = @"Masya", LastName = @"Pupkin", BirthDate = new DateTime(2020, 8, 6, 17, 34, 14, 271, DateTimeKind.Utc), Gender = Gender.Female };
        }

        private Int32 GetEntry_3()
        {
            return 10;
        }

        private String GetEntry_4()
        {
            return @"asdfasdfasdf";
        }

        private Gender GetEntry_5()
        {
            return Gender.Female;
        }

        private User[] GetEntry_6()
        {
            var v1 = new User()
            { Id = 0, FirstName = @"Vasya", LastName = @"Pupkin", BirthDate = new DateTime(2020, 8, 6, 17, 34, 14, 272, DateTimeKind.Utc), Gender = Gender.Male };
            return new User[] { v1 };
        }

        public override IEnumerable<ITestDataRecord> GetRecords()
        {
            yield return new TestDataRecord<User> { Hash = @"abc", Description = @"", Data = GetEntry_1() };
            yield return new TestDataRecord<User> { Hash = @"abc2", Description = @"", Data = GetEntry_2() };
            yield return new TestDataRecord<Int32> { Hash = @"count", Description = @"", Data = GetEntry_3() };
            yield return new TestDataRecord<String> { Hash = @"somestr", Description = @"", Data = GetEntry_4() };
            yield return new TestDataRecord<Gender> { Hash = @"asdfasdfsaf", Description = @"", Data = GetEntry_5() };
            yield return new TestDataRecord<User[]> { Hash = @"adsfasdfasdf", Description = @"", Data = GetEntry_6() };
        }
    }
}