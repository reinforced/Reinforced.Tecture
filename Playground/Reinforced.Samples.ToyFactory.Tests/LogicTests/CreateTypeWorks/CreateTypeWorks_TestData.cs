using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateTypeWorks
{
		class CreateTypeWorks_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_95A6A23F5636A6A7953BD7F86A1E7B6FB9A89894E745ED5DC8DAAA2F6D3E3E",
					Description = @"check toy type existence"
				};
			}

		}
}