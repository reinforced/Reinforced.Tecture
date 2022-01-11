using System;
using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.SupplyCreationPipeline
{
		public partial class SupplyCreationPipeline_TestData : CSharpTestData
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
				return 55;
			}

			private Boolean GetEntry_4()
			{ 
				return false;
			}

			private Int32 GetEntry_5()
			{ 
				return 55;
			}

			private Boolean GetEntry_6()
			{ 
				return false;
			}

			private Int32 GetEntry_7()
			{ 
				return 55;
			}

			private Int32 GetEntry_8()
			{ 
				return 158;
			}

			private Resource[] GetEntry_9()
			{ 
				var v1 = New<Resource>();
				Set(v1, x=>x.Id, 158);
				Set(v1, x=>x.Name, @"resource2");
				Set(v1, x=>x.MeasurementUnitId, 55);
				return new Resource[] { v1 } ;
			}

			private List<Resource> GetEntry_10()
			{ 
				var v1 = New<Resource>();
				Set(v1, x=>x.Id, 157);
				Set(v1, x=>x.Name, @"resource1");
				Set(v1, x=>x.MeasurementUnitId, 55);
				var v2 = New<Resource>();
				Set(v2, x=>x.Id, 159);
				Set(v2, x=>x.Name, @"resource3");
				Set(v2, x=>x.MeasurementUnitId, 55);
				return new List<Resource> { v1, v2 } ;
			}

			private Int32 GetEntry_11()
			{ 
				return 22;
			}

			private Int32 GetEntry_12()
			{ 
				return 22;
			}

			public override IEnumerable<ITestDataRecord> GetRecords()
			{ 
				yield return new TestDataRecord<Boolean>(GetEntry_1()) { 
					Hash = @"OrmQuery_64CFEF39AE33876A96A7D1358C1AD1F48A33BA27542F673AE894478DA1F9EBD",
					Description = @"check unit existence"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_2()) { 
					Hash = @"OrmQuery_959DC5E6E283AE49551B247D488BA887F7F5E944D8C25776FDBF0756A83DCA",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_3()) { 
					Hash = @"OrmQuery_23FCA21CF5135EF61DDD712A5EDE57C584AB1531F5850E0E4ADF7B648C61F52",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_4()) { 
					Hash = @"OrmQuery_F40ECA89EC3265AB9568E18CAE06EA9CED5A148D96255F8F21AC1C145A971A",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_5()) { 
					Hash = @"OrmQuery_23FCA21CF5135EF61DDD712A5EDE57C584AB1531F5850E0E4ADF7B648C61F52",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Boolean>(GetEntry_6()) { 
					Hash = @"OrmQuery_18B42C5BA7A7036808A8CE9EC656F238EF090B670C7FCE126C3E81E355FB2C3",
					Description = @"check resource existence"
				};
				yield return new TestDataRecord<Int32>(GetEntry_7()) { 
					Hash = @"OrmQuery_23FCA21CF5135EF61DDD712A5EDE57C584AB1531F5850E0E4ADF7B648C61F52",
					Description = @"lookup measurement unit by name (kG)"
				};
				yield return new TestDataRecord<Int32>(GetEntry_8()) { 
					Hash = @"ORM_AdditionPK_2",
					Description = @"Get primary key of added Resource"
				};
				yield return new TestDataRecord<Resource[]>(GetEntry_9()) { 
					Hash = @"D144B49489B767283F23175268EE381B371CBB54D3E28FEE4C99EBBE6F94",
					Description = @""
				};
				yield return new TestDataRecord<List<Resource>>(GetEntry_10()) { 
					Hash = @"OrmQuery_18C159F5FCCBF781B9F5A187A624382F67A35643C63B5DFC691C0744D6BBD",
					Description = @"lookup resources by 2 names"
				};
				yield return new TestDataRecord<Int32>(GetEntry_11()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"Get primary key of added ResourceSupply"
				};
				yield return new TestDataRecord<Int32>(GetEntry_12()) { 
					Hash = @"ORM_AdditionPK_1",
					Description = @"Get primary key of added ResourceSupply"
				};
			}

		}
}