using System;
using System.Collections.Generic;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Testing.Data;

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
				Set(v1, x=>x.Id, 79);
				Set(v1, x=>x.Name, @"test type");
				return v1;
			}

			private Int32 GetEntry_3()
			{ 
				return 79;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_D89D2325CAB36C99B50CC6CB1D6F8A81DDA18073C8CC7A0FD7820A65E89",
					Description = @"check toy type existence"
				};
				yield return new TestDataRecord<ToyType>(GetEntry_2()) { 
					Hash = @"OrmQuery_24F8CA228FEEEDEF1FFB466BB13B353EC1A3C5D4AB928AE2EDD86C5A14D9",
					Description = @"Obtaining Reinforced.Samples.ToyFactory.Logic.Entities.ToyType via O/RM"
				};
				yield return new TestDataRecord<Int32>(GetEntry_3()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"ORM Addition PK retrieval"
				};
			}

		}
}