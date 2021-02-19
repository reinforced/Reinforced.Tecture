using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.Test
{
		class Test_TestData : CSharpTestData
		{
			private Boolean GetEntry_1()
			{ 
				return false;
			}

			private Boolean GetEntry_2()
			{ 
				return false;
			}

			private Int32 GetEntry_3()
			{ 
				return 6;
			}

			private Boolean GetEntry_4()
			{ 
				return false;
			}

			private Int32 GetEntry_5()
			{ 
				return 6;
			}

			private Boolean GetEntry_6()
			{ 
				return false;
			}

			private Int32 GetEntry_7()
			{ 
				return 6;
			}

			private Boolean GetEntry_8()
			{ 
				return false;
			}

			private Int32 GetEntry_9()
			{ 
				return 6;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_453C31866DD6BD48D2E31F78EC2AD153828D26DAB1C5EC2AF7C2829A94A424",
					Description = @"check unit existence"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_2()) { 
					Hash = @"OrmQuery_46177FB019BB168CA5823D5C92DCBA70ED790402C49D48E8CD1BC6AB085B937",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_3()) { 
					Hash = @"OrmQuery_DE21551542C1C371DBAFBDF219316878426BDF3A9AAA0CAC31EEB86ED9E",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_4()) { 
					Hash = @"OrmQuery_1F1CBDA1894FA031CF99E99ACC4B9A5558E175A48F98C19EA2C1E5DFE4689",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_5()) { 
					Hash = @"OrmQuery_7A53114FEA6930716E934B26C28C3A30517A3BDFFEA644CC1424963EBC2BD2",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_6()) { 
					Hash = @"OrmQuery_7633FCAB7EA1F89D690B9B4C4434979AFD274FEFD7813561872D2E8D7408",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_7()) { 
					Hash = @"OrmQuery_AB93E7D8AC8B154721FFF84A3A80F2722CE0F5D1810C353EBE31964FEE08712",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_8()) { 
					Hash = @"OrmQuery_21A4D9499A18441A49E589C36A0FE4C7A232E4313EA5EDB6E79EFB1DBA4CDE",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_9()) { 
					Hash = @"OrmQuery_C52FAEA1EBAF2EF8F2952E5713603233612381873AB3C73A95EB6932BA5C",
					Description = @"lookup measurement unit by name (kG)"
				};
			}

		}
}