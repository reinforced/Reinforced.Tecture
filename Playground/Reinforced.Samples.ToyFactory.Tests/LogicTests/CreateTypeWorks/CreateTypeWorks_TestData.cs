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
				return 1002;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_F1DC789FD292E34441F920118A716D7253EB66868BB4B1944758E4022EDE5",
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