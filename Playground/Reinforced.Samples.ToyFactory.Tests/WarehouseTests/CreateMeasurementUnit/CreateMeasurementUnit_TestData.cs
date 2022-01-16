using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Samples.ToyFactory.Logic.Entities;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit
{
		public partial class CreateMeasurementUnit_TestData : CSharpTestData
		{
			private IEnumerable<Dictionary<String, Object>> GetEntry_1()
			{ 
				var v1 = New<Dictionary<String, Object>>();
				v1["Name"] = @"Boo";
				var v2 = New<ToyType>();
				Set(v2, x=>x.Id, 1);
				Set(v2, x=>x.Name, @"Test");
				v1["ToyType"] = v2;
				return new Dictionary<String, Object>[] { v1 } ;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new AnonymousCollectionTestDataRecord(GetEntry_1()) { 
					Hash = @"OrmQuery_891E84797C708923EF89F2156F985474099B28E1C5264F5E1AFB1E79013B8",
					Description = null
				};
			}

		}
}