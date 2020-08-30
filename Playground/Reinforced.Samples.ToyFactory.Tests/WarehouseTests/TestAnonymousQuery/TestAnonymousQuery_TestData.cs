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
					Hash = @"OrmQuery_C5A8B5DE46D4A798BDEFB92CE84F25738DFF5C9FEFBC533B1C9A2C24E9C40A",
					Description = @"Get projection of Resource by Id #177"
				};
			}

		}
}