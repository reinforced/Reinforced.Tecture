using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.RenameMeasurementUnit
{
		class RenameMeasurementUnit_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			private Int32 GetEntry_2()
			{ 
				return 29;
			}

			private MeasurementUnit GetEntry_3()
			{ 
				var v1 = New<MeasurementUnit>();
				Set(v1, x=>x.Id, 29);
				Set(v1, x=>x.ShortName, @"kg");
				Set(v1, x=>x.Name, @"Kilo");
				return v1;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_64CFEF39AE33876A96A7D1358C1AD1F48A33BA27542F673AE894478DA1F9EBD",
					Description = @"check unit existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_2()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"Get primary key of added MeasurementUnit"
				};
				yield return new TestDataRecord<MeasurementUnit>(GetEntry_3()) { 
					Hash = @"OrmQuery_DEF7C21F11B39E93E826A4FA68BA35FBB0A5AB2855BFD74A47E7426DDE7D3EC",
					Description = @"Get MeasurementUnit by Id #29"
				};
			}

		}
}