using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Samples.ToyFactory.Logic.Entities;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateTypeWorks
{
		class CreateTypeWorks_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			private ToyType GetEntry_2()
			{ 
				var v1 = New<ToyType>();
				Set(v1, x=>x.Id, 1002);
				Set(v1, x=>x.Name, @"test type");
				return v1;
			}

			private Int32 GetEntry_3()
			{ 
				return 1003;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_C5AA2E972EC6147BB57D28A39C52F64114389AD2A919A31CFA5548AC2731736",
					Description = @"check toy type existence"
				};
				yield return new TestDataRecord<ToyType>(GetEntry_2()) { 
					Hash = @"OrmQuery_24F8CA228FEEEDEF1FFB466BB13B353EC1A3C5D4AB928AE2EDD86C5A14D9",
					Description = null
				};
				yield return new TestDataRecord<Int32>(GetEntry_3()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"Get primary key of added ToyType"
				};
			}

		}
}