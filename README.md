# What is that?

This is experimental architecture framework for .NET applications. It is built on aspect-based principles involving some CQRS and functional programming approaches. Tecture intensively utilizes C# features, strong typing, lambda expressions, extension methods and generics. Usage experience is very similar to LINQ. The closest project that does somewhat similar is [MediatR](https://github.com/jbogard/MediatR), but Tecture is wider and covers more problems. 

Reinforced.Tecture [is available on NuGet](https://www.nuget.org/packages/Reinforced.Tecture/) along with its dependent packages.

```bash
PM> Install-Package Reinforced.Tecture
PM> Install-Package Reinforced.Tecture.Aspects.Orm
PM> Install-Package Reinforced.Tecture.Aspects.SqlStroke
PM> Install-Package Reinforced.Tecture.Runtimes.EfCore
PM> Install-Package Reinforced.Tecture.Testing
```

Get in touch with [documentation](https://github.com/reinforced/Reinforced.Tecture/wiki)

# What can I do with it?

Lots of useful stuff that you usually do while maintaining a business application, but much easier.

## Define 
[channels](https://github.com/reinforced/Reinforced.Tecture/wiki/Channels) and use [aspects](https://github.com/reinforced/Reinforced.Tecture/wiki/Aspects):

```csharp
/// <summary>
/// Hi, I'm database communication channel
/// </summary>
public interface Db :
        CommandQueryChannel<Reinforced.Tecture.Aspects.Orm.Command, 
		Reinforced.Tecture.Aspects.Orm.Query>
    { }
```

## Create 
[services](https://github.com/reinforced/Reinforced.Tecture/wiki/Services) for business logic and produce [commands](https://github.com/reinforced/Reinforced.Tecture/wiki/Commands)

```csharp
/// <summary>
/// I'm orders service. And these are my type parameters (tooling). 
///	                  By using them I say that I can update orders and add order lines
/// </summary>                       |                               |
public class Orders : TectureService< Updates<Order>, Adds<OrderLine> >
{
	private Orders() { }

	/// <summary>
	/// And I'm business logic method
	/// </summary>
	/// <param name="orderId">I consume order id</param>
	/// <param name="poductId">and product id</param>
	/// <param name="quantity">and also product quantity</param>
	public void CreateLine(int orderId, int poductId, int quantity)
	{
		// I perform queries to the database
		var order = From<Db>.Get<Order>().ById(orderId);
		
		// My aspect allows me to add order lines
		To<Db>().Add(new OrderLine
				{
						OrderId = orderId,
						ProductId = productId,
						Quantity = quantity
				}
		);

		// And only update orders
		To<Db>.Update(order)
		      .Set(x=>x.TotalQuantity, order.TotalQuantity + quantity);

		// Also I can invoke other services
		Do<Products>().AttachToOrder(order);
	}
}
```

## Use 
[queries](https://github.com/reinforced/Reinforced.Tecture/wiki/Queries) inside your channels

```csharp
///<summary>
/// I'm entity interface...
///</summary>
public interface IEntity { int Id { get; } }

public static class Extensions
{
	///<summary>
	/// ...and you don't need repositories anymore to get me by Id
	///</summary>
	public static T ById<T>(this IQueryFor<T> q, int id) where T : IEntity
	{
		return q.All.FirstOrDefault(x => x.Id == id);
	}
	
	///<summary>
	/// Even if you have SQL
	///</summary>
	public static IEnumerable<Order> GetRecentOrders(this Read<Db> db)
	{
		return db.SqlQuery<Order>(o => 
			$"SELECT * FROM {o} WHERE DATEDIFF(day, {o.UpdatedAt}, GETDATE()) < 30"
		).As<Order>();
	}
}

// ... 
var o = From<Db>().GetRecentOrders();
// ...
```

## Integrate 
[with IoC containers](https://github.com/reinforced/Reinforced.Tecture/wiki/Ioc) and use it from application

```csharp
public class OrdersController : ApiController
{
	// You can inject
	private readonly ITecture _tecture;

	public OrdersController(ITecture tecture)
	{
		_tecture = tecture;
	}

	public ActionResult PerformActionWithOrder(int id)
	{
		// and use it  
		_tecture.Do<Orders>().PerformAction(id);
		_tecture.Save();

		return Ok();
	}
}
```

## [Trace](https://github.com/reinforced/Reinforced.Tecture/wiki/Tracing) 
your business logic and get clear explanation what exactly happens in terms of business

```csharp
tecture.BeginTrace();

var a = tecture.Do<Orders>().CreateOne("new order");
ctx.Save();

var trace = tecture.EndTrace();
Output.Write(trace.Explain());

/**
 * 1. [ ->] 	Check existing order presence: 'False' obtained
 * 2. [ADD] 	Adding new order to the database
 * 3. [<- ] 	<SAVE>
 * 4. [SQL] 	Re-calculating denormalized items count
 * 5. [<- ] 	<SAVE>
 * 6. [ ! ] 	<END>
 */
```

## Test
You can capture [test data](https://github.com/reinforced/Reinforced.Tecture/wiki/Test-Data) and save it in file.   Next step is to get [validation from traces](https://github.com/reinforced/Reinforced.Tecture/wiki/Generate-Validation). In the end, turn captured data with validation trace into data-driven infrastructure-free unit tests!

```csharp
[Fact]
public void Order_Creation_Works_As_Expected()
{
	using var c = Case<Order_Creation_Works_As_Expected_TestData>(out ITecture ctx);

	var a = ctx.Do<Orders>().CreateOne("test order");
	ctx.Save();
	
	c.Validate<Order_Creation_Works_As_Expected_Validation>();
}
```
