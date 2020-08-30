using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Samples.ToyFactory.Logic.Entities;

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
				Set(v1, x=>x.Id, 1002);
				Set(v1, x=>x.Name, @"test type");
				return v1;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_FEDF12D01C5A92BE793C8C9A1E2BBA6B2EF49F38D6C787B61195909AB7814C52",
					Description = @"Exists ToyType with Id #1002"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_2()) { 
					Hash = @"OrmQuery_60D4B01265E1C2985AC496B35BE9E5B6AB10C2635F3048A4F12544C99B57CFC6",
					Description = @"check blueprint existence"
				};
				yield return new TestDataRecord<ToyType>(GetEntry_3()) { 
					Hash = @"OrmQuery_73EB1F8673A3B81BBBED1E0929FA23B0E8E289BB3F4DA2F6D09669815AB162",
					Description = @"Get ToyType by Id #1002"
				};
			}

		}
}