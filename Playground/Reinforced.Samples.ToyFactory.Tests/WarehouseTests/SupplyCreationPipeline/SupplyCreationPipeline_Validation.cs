using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Channels;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Tecture.Aspects.Orm.Commands.Relate;
using Reinforced.Tecture.Aspects.DirectSql.Commands;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.SupplyCreationPipeline
{
		class SupplyCreationPipeline_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<MeasurementUnit>(c.Entity).Id, 0, "Id of property Entity of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
						Assert.Equal(As<MeasurementUnit>(c.Entity).ShortName, @"kG", "ShortName of property Entity of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
						Assert.Equal(As<MeasurementUnit>(c.Entity).Name, @"Kilograms", "Name of property Entity of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
						Assert.Equal(c.EntityType, typeof(MeasurementUnit), "property EntityType of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
						Assert.Equal(c.Annotation, @"create measurement unit 'Kilograms' (kG)", "property Annotation of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "property Annotation of command Save has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<Resource>(c.Entity).Id, 0, "Id of property Entity of command 'new resource resource1' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).Name, @"resource1", "Name of property Entity of command 'new resource resource1' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).StockQuantity, 0, "StockQuantity of property Entity of command 'new resource resource1' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).MeasurementUnitId, 28, "MeasurementUnitId of property Entity of command 'new resource resource1' has invalid value");
						Assert.Null(As<Resource>(c.Entity).MeasurementUnit, "MeasurementUnit of property Entity of command 'new resource resource1' must be null");
						Assert.Equal(c.EntityType, typeof(Resource), "property EntityType of command 'new resource resource1' has invalid value");
						Assert.Equal(c.Annotation, @"new resource resource1", "property Annotation of command 'new resource resource1' has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<Resource>(c.Entity).Id, 0, "Id of property Entity of command 'new resource resource2' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).Name, @"resource2", "Name of property Entity of command 'new resource resource2' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).StockQuantity, 0, "StockQuantity of property Entity of command 'new resource resource2' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).MeasurementUnitId, 28, "MeasurementUnitId of property Entity of command 'new resource resource2' has invalid value");
						Assert.Null(As<Resource>(c.Entity).MeasurementUnit, "MeasurementUnit of property Entity of command 'new resource resource2' must be null");
						Assert.Equal(c.EntityType, typeof(Resource), "property EntityType of command 'new resource resource2' has invalid value");
						Assert.Equal(c.Annotation, @"new resource resource2", "property Annotation of command 'new resource resource2' has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<Resource>(c.Entity).Id, 0, "Id of property Entity of command 'new resource resource3' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).Name, @"resource3", "Name of property Entity of command 'new resource resource3' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).StockQuantity, 0, "StockQuantity of property Entity of command 'new resource resource3' has invalid value");
						Assert.Equal(As<Resource>(c.Entity).MeasurementUnitId, 28, "MeasurementUnitId of property Entity of command 'new resource resource3' has invalid value");
						Assert.Null(As<Resource>(c.Entity).MeasurementUnit, "MeasurementUnit of property Entity of command 'new resource resource3' must be null");
						Assert.Equal(c.EntityType, typeof(Resource), "property EntityType of command 'new resource resource3' has invalid value");
						Assert.Equal(c.Annotation, @"new resource resource3", "property Annotation of command 'new resource resource3' has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "property Annotation of command Save has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<ResourceSupply>(c.Entity).Name, @"Supply1", "Name of property Entity of command 'add resource supply' has invalid value");
						//Assert.Equal(As<ResourceSupply>(c.Entity).CreationDate, new DateTime(2022, 1, 9, 0, 17, 48, 961, DateTimeKind.Utc), "CreationDate of property Entity of command 'add resource supply' has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Entity).Status, ResourceSupplyStatus.Open, "Status of property Entity of command 'add resource supply' has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Entity).ItemsCount, 0, "ItemsCount of property Entity of command 'add resource supply' has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Entity).Id, 0, "Id of property Entity of command 'add resource supply' has invalid value");
						Assert.Equal(c.EntityType, typeof(ResourceSupply), "property EntityType of command 'add resource supply' has invalid value");
						Assert.Equal(c.Annotation, @"add resource supply", "property Annotation of command 'add resource supply' has invalid value");
					}
				);
				flow.Then<Db, Relate>
				(c=>
					{ 
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).ResourceId, 79, "ResourceId of property Primary of command Relate has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Primary).Resource, "Resource of property Primary of command Relate must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).ResourceSupplyId, 0, "ResourceSupplyId of property Primary of command Relate has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Primary).ResourceSupply, "ResourceSupply of property Primary of command Relate must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).Quantity, 10, "Quantity of property Primary of command Relate has invalid value");
						Assert.Equal(c.PrimaryType, typeof(ResourceSupplyItem), "property PrimaryType of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Name, @"Supply1", "Name of property Secondary of command Relate has invalid value");
						//Assert.Equal(As<ResourceSupply>(c.Secondary).CreationDate, new DateTime(2022, 1, 9, 0, 17, 48, 961, DateTimeKind.Utc), "CreationDate of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Status, ResourceSupplyStatus.Open, "Status of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).ItemsCount, 0, "ItemsCount of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Id, 0, "Id of property Secondary of command Relate has invalid value");
						Assert.Equal(c.SecondaryType, typeof(ResourceSupply), "property SecondaryType of command Relate has invalid value");
						Assert.Equal(c.ForeignKeySpecifier, @"ResourceSupply", "property ForeignKeySpecifier of command Relate has invalid value");
						Assert.Equal(c.Annotation, @"", "property Annotation of command Relate has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).ResourceId, 79, "ResourceId of property Entity of command 'add resource supply item' has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Entity).Resource, "Resource of property Entity of command 'add resource supply item' must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).ResourceSupplyId, 0, "ResourceSupplyId of property Entity of command 'add resource supply item' has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Entity).ResourceSupply, "ResourceSupply of property Entity of command 'add resource supply item' must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).Quantity, 10, "Quantity of property Entity of command 'add resource supply item' has invalid value");
						Assert.Equal(c.EntityType, typeof(ResourceSupplyItem), "property EntityType of command 'add resource supply item' has invalid value");
						Assert.Equal(c.Annotation, @"add resource supply item", "property Annotation of command 'add resource supply item' has invalid value");
					}
				);
				flow.Then<Db, Relate>
				(c=>
					{ 
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).ResourceId, 80, "ResourceId of property Primary of command Relate has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Primary).Resource, "Resource of property Primary of command Relate must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).ResourceSupplyId, 0, "ResourceSupplyId of property Primary of command Relate has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Primary).ResourceSupply, "ResourceSupply of property Primary of command Relate must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).Quantity, 10, "Quantity of property Primary of command Relate has invalid value");
						Assert.Equal(c.PrimaryType, typeof(ResourceSupplyItem), "property PrimaryType of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Name, @"Supply1", "Name of property Secondary of command Relate has invalid value");
						//Assert.Equal(As<ResourceSupply>(c.Secondary).CreationDate, new DateTime(2022, 1, 9, 0, 17, 48, 961, DateTimeKind.Utc), "CreationDate of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Status, ResourceSupplyStatus.Open, "Status of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).ItemsCount, 0, "ItemsCount of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Id, 0, "Id of property Secondary of command Relate has invalid value");
						Assert.Equal(c.SecondaryType, typeof(ResourceSupply), "property SecondaryType of command Relate has invalid value");
						Assert.Equal(c.ForeignKeySpecifier, @"ResourceSupply", "property ForeignKeySpecifier of command Relate has invalid value");
						Assert.Equal(c.Annotation, @"", "property Annotation of command Relate has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).ResourceId, 80, "ResourceId of property Entity of command 'add resource supply item' has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Entity).Resource, "Resource of property Entity of command 'add resource supply item' must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).ResourceSupplyId, 0, "ResourceSupplyId of property Entity of command 'add resource supply item' has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Entity).ResourceSupply, "ResourceSupply of property Entity of command 'add resource supply item' must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).Quantity, 10, "Quantity of property Entity of command 'add resource supply item' has invalid value");
						Assert.Equal(c.EntityType, typeof(ResourceSupplyItem), "property EntityType of command 'add resource supply item' has invalid value");
						Assert.Equal(c.Annotation, @"add resource supply item", "property Annotation of command 'add resource supply item' has invalid value");
					}
				);
				flow.Then<Db, Relate>
				(c=>
					{ 
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).ResourceId, 81, "ResourceId of property Primary of command Relate has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Primary).Resource, "Resource of property Primary of command Relate must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).ResourceSupplyId, 0, "ResourceSupplyId of property Primary of command Relate has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Primary).ResourceSupply, "ResourceSupply of property Primary of command Relate must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Primary).Quantity, 10, "Quantity of property Primary of command Relate has invalid value");
						Assert.Equal(c.PrimaryType, typeof(ResourceSupplyItem), "property PrimaryType of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Name, @"Supply1", "Name of property Secondary of command Relate has invalid value");
						//Assert.Equal(As<ResourceSupply>(c.Secondary).CreationDate, new DateTime(2022, 1, 9, 0, 17, 48, 961, DateTimeKind.Utc), "CreationDate of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Status, ResourceSupplyStatus.Open, "Status of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).ItemsCount, 0, "ItemsCount of property Secondary of command Relate has invalid value");
						Assert.Equal(As<ResourceSupply>(c.Secondary).Id, 0, "Id of property Secondary of command Relate has invalid value");
						Assert.Equal(c.SecondaryType, typeof(ResourceSupply), "property SecondaryType of command Relate has invalid value");
						Assert.Equal(c.ForeignKeySpecifier, @"ResourceSupply", "property ForeignKeySpecifier of command Relate has invalid value");
						Assert.Equal(c.Annotation, @"", "property Annotation of command Relate has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).ResourceId, 81, "ResourceId of property Entity of command 'add resource supply item' has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Entity).Resource, "Resource of property Entity of command 'add resource supply item' must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).ResourceSupplyId, 0, "ResourceSupplyId of property Entity of command 'add resource supply item' has invalid value");
						Assert.Null(As<ResourceSupplyItem>(c.Entity).ResourceSupply, "ResourceSupply of property Entity of command 'add resource supply item' must be null");
						Assert.Equal(As<ResourceSupplyItem>(c.Entity).Quantity, 10, "Quantity of property Entity of command 'add resource supply item' has invalid value");
						Assert.Equal(c.EntityType, typeof(ResourceSupplyItem), "property EntityType of command 'add resource supply item' has invalid value");
						Assert.Equal(c.Annotation, @"add resource supply item", "property Annotation of command 'add resource supply item' has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "property Annotation of command Save has invalid value");
					}
				);
				flow.Then<Db, Sql>
				(c=>
					{ 
						Assert.Equal(c.QueryText, @"UPDATE [r] SET [r].[ItemsCount] = (SELECT COUNT(*) FROM [ResourceSupplyItem] [item]
 WHERE [item].[ResourceSupplyId] = {0}) FROM [ResourceSupply] [r]", "query text has invalid value");
						Assert.Collection(c.QueryParameters, "query parameters collection must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 4, "item #0 of query parameters collection has invalid value");
						});
						Assert.Equal(c.Annotation, @"", "property Annotation of command Sql has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "property Annotation of command Save has invalid value");
					}
				);
				flow.Then<Db, Sql>
				(c=>
					{ 
						Assert.Equal(c.QueryText, @"UPDATE [res]
    SET [res].[StockQuantity] = ([res].[StockQuantity] + [item].[Quantity])
    FROM [Resource] [res]

    INNER JOIN [ResourceSupplyItem] [item]
 ON [item].[ResourceId] = [res].[Id]
    WHERE [item].[ResourceSupplyId] = {0}", "query text has invalid value");
						Assert.Collection(c.QueryParameters, "query parameters collection must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 4, "item #0 of query parameters collection has invalid value");
						});
						Assert.Equal(c.Annotation, @"", "property Annotation of command Sql has invalid value");
					}
				);
				flow.Then<Db, Sql>
				(c=>
					{ 
						Assert.Equal(c.QueryText, @"UPDATE [r] SET [r].[Status] = {0} FROM [ResourceSupply] [r]
 WHERE [r].[Id] = {1}", "query text has invalid value");
						Assert.Collection(c.QueryParameters, "query parameters collection must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 4, "item #0 of query parameters collection has invalid value");
						}, r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 4, "item #0 of query parameters collection has invalid value");
						});
						Assert.Equal(c.Annotation, @"", "property Annotation of command Sql has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "property Annotation of command Save has invalid value");
					}
				);
				flow.TheEnd();
			}

		}
}
