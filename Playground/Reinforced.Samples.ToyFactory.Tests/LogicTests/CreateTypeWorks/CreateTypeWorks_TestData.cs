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
				Set(v1, x=>x.Id, 32);
				Set(v1, x=>x.Name, @"test type2");
				return v1;
			}

			private Int32 GetEntry_3()
			{ 
				return 32;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_3241C66C12C82F8E81E7DC4A39E3B8C316ACC7CCD0F6D37C6E519C43BB3E531",
					Description = @"check toy type existence"
				};
				yield return new TestDataRecord<ToyType>(GetEntry_2()) { 
					Hash = @"OrmQuery_F7D8A6ED24C427B3D81EE2B65FB779C63D6FCF5FC8AC32EC56756A1EA4A25",
					Description = null
				};
				yield return new TestDataRecord<Int32>(GetEntry_3()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"Get primary key of added ToyType"
				};
			}

		}
}