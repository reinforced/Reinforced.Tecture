using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.SupplyCreationPipeline
{
		class SupplyCreationPipeline_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			private Boolean GetEntry_2()
			{ 
				return false;
			}

			private Int32 GetEntry_3()
			{ 
				return 103;
			}

			private Boolean GetEntry_4()
			{ 
				return false;
			}

			private Int32 GetEntry_5()
			{ 
				return 103;
			}

			private Boolean GetEntry_6()
			{ 
				return false;
			}

			private Int32 GetEntry_7()
			{ 
				return 103;
			}

			private Int32 GetEntry_8()
			{ 
				return 182;
			}

			private Resource[] GetEntry_9()
			{ 
				var v1 = New<Resource>();
				Set(v1, x=>x.Id, 182);
				Set(v1, x=>x.Name, @"resource2");
				Set(v1, x=>x.MeasurementUnitId, 103);
				var v2 = New<MeasurementUnit>();
				Set(v2, x=>x.Id, 103);
				Set(v2, x=>x.ShortName, @"kG");
				Set(v2, x=>x.Name, @"Kilograms");
				Set(v1, x=>x.MeasurementUnit, v2);
				return new Resource[] { v1 } ;
			}

			private List<Resource> GetEntry_10()
			{ 
				var v1 = New<Resource>();
				Set(v1, x=>x.Id, 181);
				Set(v1, x=>x.Name, @"resource1");
				Set(v1, x=>x.MeasurementUnitId, 103);
				var v2 = New<MeasurementUnit>();
				Set(v2, x=>x.Id, 103);
				Set(v2, x=>x.ShortName, @"kG");
				Set(v2, x=>x.Name, @"Kilograms");
				var v3 = New<Resource>();
				Set(v3, x=>x.Id, 183);
				Set(v3, x=>x.Name, @"resource3");
				Set(v3, x=>x.MeasurementUnitId, 103);
				var v4 = New<MeasurementUnit>();
				Set(v4, x=>x.Id, 103);
				Set(v4, x=>x.ShortName, @"kG");
				Set(v4, x=>x.Name, @"Kilograms");
				Set(v1, x=>x.MeasurementUnit, v2);
				Set(v3, x=>x.MeasurementUnit, v4);
				return new List<Resource> { v1, v3 } ;
			}

			private Int32 GetEntry_11()
			{ 
				return 46;
			}

			private Int32 GetEntry_12()
			{ 
				return 46;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_AB78B1DAADDAD273C885641045DBA4575D5FF2948319913F64C58E6FC40F215",
					Description = @"check unit existence"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_2()) { 
					Hash = @"OrmQuery_45C7667053E7DAE7F7346A921198CE3C7C78EADF6C1B962D8E62DFE30A1",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_3()) { 
					Hash = @"OrmQuery_9A8DF285F5C79AFAA766CC82625165EB448BABC8E2DA9E16667526773D9BC061",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_4()) { 
					Hash = @"OrmQuery_2E9D46B6980CAA970EC2128E19070F8FA79453BDCA2FDCE3346F6EA54E328",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_5()) { 
					Hash = @"OrmQuery_EEF899C488CDCF57D677A0718DDA4BA2A4E380BE266F8AC276D05456707111",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_6()) { 
					Hash = @"OrmQuery_95D682E873265ACCA3D3CDD6EEBE58E4DC39DF093AD6AC1575D5C1D617924A",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_7()) { 
					Hash = @"OrmQuery_FD3F6ACB04164A82CDDDD4B8EEA086E7D46C29C9E7329A565621A3C24235",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Int32>(GetEntry_8()) { 
					Hash = @"ORM_AdditionPK_2",
					Description = @"ORM Addition PK retrieval"
				};
				yield return new TestDataRecord<Resource[]>(GetEntry_9()) { 
					Hash = @"51FA2A8C1217B05762F53ED7893F636A2F3593C9714D01EC352463C6023AD5D",
					Description = @""
				};
				yield return new TestDataRecord<List<Resource>>(GetEntry_10()) { 
					Hash = @"OrmQuery_F547945F9D123386677AC037DDC725878CA5318DDCA7A5F4ABF512633193D",
					Description = @"lookup resources by 2 names"
				};
				yield return new TestDataRecord<Int32>(GetEntry_11()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"ORM Addition PK retrieval"
				};
				yield return new TestDataRecord<Int32>(GetEntry_12()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"ORM Addition PK retrieval"
				};
			}

		}
}