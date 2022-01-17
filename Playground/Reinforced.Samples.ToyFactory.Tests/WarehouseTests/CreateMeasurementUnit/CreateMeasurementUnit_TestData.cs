using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit
{
		public partial class CreateMeasurementUnit_TestData : CSharpTestData
		{
			private TestClassA GetEntry_1()
			{ 
				var v1 = New<TestClassA>();
				var v2 = New<TestClassB>();
				Set(v2, x=>x.Name, @"AAA");
				var v3 = New<TestClassB>();
				Set(v3, x=>x.Name, @"BBB");
				var v4 = New<TestClassB>();
				Set(v4, x=>x.Name, @"CCC");
				Set(v1, x=>x.TestField, new Dictionary<Guid, TestClassB>() { 
					{ Guid.Parse("119e2512-d1ec-4bf7-8bb6-67fe0cb30db3"), v2 },
					{ Guid.Parse("b8713a0a-3a04-452a-a701-3430c393eff5"), v3 },
					{ Guid.Parse("25dd1613-825d-4259-ab0f-87fa2e96747b"), v4 }
				});
				return v1;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<TestClassA>(GetEntry_1()) { 
					Hash = @"Test_0",
					Description = null
				};
			}

		}
}