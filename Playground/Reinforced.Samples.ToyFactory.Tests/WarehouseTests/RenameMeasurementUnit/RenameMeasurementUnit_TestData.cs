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
				return 1109;
			}

			private MeasurementUnit GetEntry_3()
			{ 
				var v1 = New<MeasurementUnit>();
				Set(v1, x=>x.Id, 1109);
				Set(v1, x=>x.ShortName, @"kg");
				Set(v1, x=>x.Name, @"Kilo");
				return v1;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_AB78B1DAADDAD273C885641045DBA4575D5FF2948319913F64C58E6FC40F215",
					Description = @"check unit existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_2()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"Get primary key of added MeasurementUnit"
				};
				yield return new TestDataRecord<MeasurementUnit>(GetEntry_3()) { 
					Hash = @"OrmQuery_E8D88FC78DB6B7C532F5B2FFF6D4DEBA741DE11B0878CC9313BF2A0A5F5CC5",
					Description = @"Get MeasurementUnit by Id #1109 (required)"
				};
			}

		}
}