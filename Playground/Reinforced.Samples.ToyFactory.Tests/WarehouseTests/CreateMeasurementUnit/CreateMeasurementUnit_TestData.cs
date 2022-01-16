using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit
{
		public partial class CreateMeasurementUnit_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			private IEnumerable<Dictionary<String, Object>> GetEntry_2()
			{ 
				var v1 = New<Dictionary<String, Object>>();
				v1["Name"] = @"Kilograms";
				v1["ShortName"] = @"kG";
				return new List<Dictionary<String, Object>> { v1 } ;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_64CFEF39AE33876A96A7D1358C1AD1F48A33BA27542F673AE894478DA1F9EBD",
					Description = @"check unit existence"
				};
				yield return new AnonymousCollectionTestDataRecord(GetEntry_2()) { 
					Hash = @"OrmQuery_F4EDF825F7B548CBB59FE62A7AA717B1DF7F98E10F9FE6FC750E6605346D0CD",
					Description = null
				};
			}

		}
}