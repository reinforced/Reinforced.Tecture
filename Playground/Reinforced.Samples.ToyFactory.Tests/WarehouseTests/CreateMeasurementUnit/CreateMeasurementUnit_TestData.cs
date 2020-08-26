using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit
{
		class CreateMeasurementUnit_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_D08B6C21DA6DE34CA95A128C82B27357B9BD49E49D958E517895AC6948227",
					Description = @"check unit existence"
				};
			}

		}
}