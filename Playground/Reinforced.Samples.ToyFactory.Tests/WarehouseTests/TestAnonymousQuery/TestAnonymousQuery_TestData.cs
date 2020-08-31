using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.TestAnonymousQuery
{
		class TestAnonymousQuery_TestData : CSharpTestData
		{
			private Dictionary<String, Object> GetEntry_1()
			{ 
				var v1 = New<Dictionary<String, Object>>();
				v1["Name"] = @"resource3";
				v1["StockQuantity"] = 10;
				return v1;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new AnonymousTestDataRecord(GetEntry_1()) { 
					Hash = @"OrmQuery_2EFF68D3FE5250B9C962CF6CC94EE22324F2C74AA348D393888C868BD9294",
					Description = @"Get projection of Resource by Id #183"
				};
			}

		}
}