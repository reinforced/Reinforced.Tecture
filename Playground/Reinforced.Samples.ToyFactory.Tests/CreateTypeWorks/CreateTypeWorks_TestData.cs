using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Samples.ToyFactory.Tests.CreateTypeWorks
{
		class CreateTypeWorks_TestData:CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			private Int32 GetEntry_2()
			{ 
				return 43;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_D89D2325CAB36C99B50CC6CB1D6F8A81DDA18073C8CC7A0FD7820A65E89",
					Description = @"check toy type existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_2()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"ORM Addition PK retrieval"
				};
			}

		}
}