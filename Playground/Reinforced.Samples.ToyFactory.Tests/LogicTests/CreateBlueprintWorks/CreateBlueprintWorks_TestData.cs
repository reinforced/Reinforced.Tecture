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
				Set(v1, x=>x.Id, 32);
				Set(v1, x=>x.Name, @"test type2");
				return v1;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_889BAD76EA1CBA5ADF2BA777D9CA1C8BCD12D2C20D8BD81982FED99AE66241",
					Description = @"Exists ToyType with Id #32"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_2()) { 
					Hash = @"OrmQuery_F555F139FCA269BF8D85195307757B4B0467EFA6620A4665E8A924D504F80",
					Description = @"check blueprint existence"
				};
				yield return new TestDataRecord<ToyType>(GetEntry_3()) { 
					Hash = @"OrmQuery_72E2BAE12A177BA43B52C8D7578526DCB76D5EB850F3C8E3C78FAAAF53144",
					Description = @"Get ToyType by Id #32"
				};
			}

		}
}