using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Samples.ToyFactory.Logic.Entities;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateAndRenameBlueprintWorks
{
		class CreateAndRenameBlueprintWorks_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			private Boolean GetEntry_2()
			{ 
				return false;
			}

			private ToyType GetEntry_3()
			{ 
				var v1 = New<ToyType>();
				Set(v1, x=>x.Id, 32);
				Set(v1, x=>x.Name, @"test type2");
				return v1;
			}

			private ToyType GetEntry_4()
			{ 
				var v1 = New<ToyType>();
				Set(v1, x=>x.Id, 32);
				Set(v1, x=>x.Name, @"test type2");
				return v1;
			}

			private Int32 GetEntry_5()
			{ 
				return 43;
			}

			private Int32 GetEntry_6()
			{ 
				return 44;
			}

			private Int32 GetEntry_7()
			{ 
				return 7;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_B93AD48467D78E5C47FF7B931C38FB255F67E21145A95CAF987E168E9E48",
					Description = @"check toy type existence"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_2()) { 
					Hash = @"OrmQuery_2CEF197181A74CFDD4949CEAC53CEFB45D94671105F5F4B35148213B5A2A9",
					Description = @"check toy type existence"
				};
				yield return new TestDataRecord<ToyType>(GetEntry_3()) { 
					Hash = @"OrmQuery_F7D8A6ED24C427B3D81EE2B65FB779C63D6FCF5FC8AC32EC56756A1EA4A25",
					Description = null
				};
				yield return new TestDataRecord<ToyType>(GetEntry_4()) { 
					Hash = @"OrmQuery_F7D8A6ED24C427B3D81EE2B65FB779C63D6FCF5FC8AC32EC56756A1EA4A25",
					Description = null
				};
				yield return new TestDataRecord<Int32>(GetEntry_5()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"Get primary key of added ToyType"
				};
				yield return new TestDataRecord<Int32>(GetEntry_6()) { 
					Hash = @"ORM_AdditionPK_2",
					Description = @"Get primary key of added ToyType"
				};
				yield return new TestDataRecord<Int32>(GetEntry_7()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"Get primary key of added Blueprint"
				};
			}

		}
}