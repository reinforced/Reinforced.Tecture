using System;
using System.Collections.Generic;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateBlueprintWorks
{
		class CreateBlueprintWorks_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return true;
			}

			private Boolean GetEntry_2()
			{ 
				return false;
			}

			private ToyType GetEntry_3()
			{ 
				var v1 = New<ToyType>();
				Set(v1, x=>x.Id, 79);
				Set(v1, x=>x.Name, @"test type");
				return v1;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_D73E75FCFE14DB8BBC2B13F988D625E8ED437DEC5A20B39FE2B11BD1310C8",
					Description = @"Exists ToyType with Id #79"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_2()) { 
					Hash = @"OrmQuery_7345F4CE1387FAC0F9C1A4C0AAE43E64A7D47CEED731A6E706383B618C28",
					Description = @"check blueprint existence"
				};
				yield return new TestDataRecord<ToyType>(GetEntry_3()) { 
					Hash = @"OrmQuery_F5C719D43D5C52BAA3C583FFABDAE44E3292BD639D78133919CBF47AC177",
					Description = @"Get ToyType by Id #79"
				};
			}

		}
}