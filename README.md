# What is that?

This is experimental architecture framework for business applications made on .NET platform.

Reinforced.Tecture [is available on NuGet](https://www.nuget.org/packages/Reinforced.Tecture/) along with its dependent packages.

```bash
PM> Install-Package Reinforced.Tecture
PM> Install-Package Reinforced.Tecture.Aspects.Orm
PM> Install-Package Reinforced.Tecture.Aspects.DirectSql
PM> Install-Package Reinforced.Tecture.Runtimes.EfCore
PM> Install-Package Reinforced.Tecture.Testing
```

Get in touch with [documentation](https://github.com/reinforced/Reinforced.Tecture/wiki)

# What are benefits?

Tecture gives you benefits depending on your role in the team. So in case if you are...

- **System Architect**: easy and explicit type-safe integration of your .NET application with external systems (databases, queues, etc) by the mechanism of [channels](https://github.com/reinforced/Reinforced.Tecture/wiki/Channels). More formal control over the codebase from single place by [aspects](https://github.com/reinforced/Reinforced.Tecture/wiki/Aspects). Shorter [IoC](https://github.com/reinforced/Reinforced.Tecture/wiki/IoC) configuration;
- **System Analyst**: [self-explanatory](https://github.com/reinforced/Reinforced.Tecture/wiki/Tracing) business logic with [informational annotations](https://github.com/reinforced/Reinforced.Tecture/wiki/Describe);
- **DevOps**: easier way to go .NET Core;
- **QA**: [automatically generated validation](https://github.com/reinforced/Reinforced.Tecture/wiki/Generate-Validation) for honest, meaningful [infrastructure-free data-driven regression tests](https://github.com/reinforced/Reinforced.Tecture/wiki/Unit-Test) with [automated test data capture](https://github.com/reinforced/Reinforced.Tecture/wiki/Test-Data);
- **Regular Software Engineer**: clean and safe way to implement features without necessarity to write mocks, stubs and fakes. [No more redundant interfaces](https://github.com/reinforced/Reinforced.Tecture/wiki/Services), no more [repository pattern](https://github.com/reinforced/Reinforced.Tecture/wiki/Queries);
- **Functional programming maniac**: giant IO monad on [commands](https://github.com/reinforced/Reinforced.Tecture/wiki/Commands), queries made with [pure functions](https://github.com/reinforced/Reinforced.Tecture/wiki/Queries);
- **Team Lead**: cheap way to get honest 100% code coverage with tests;
- **Product Owner**: faster feature delivery because of less code, easier feature logic discussions because of self-explanatory logic traces.

Below are several pieces of code that uses Tecture:

## Abstractions of external systems 
Define [channels](https://github.com/reinforced/Reinforced.Tecture/wiki/Channels) and use [aspects](https://github.com/reinforced/Reinforced.Tecture/wiki/Aspects):

```csharp
/// <summary>
/// Hi, I'm database communication channel
/// </summary>
public interface Db :
        CommandQueryChannel<
	   Reinforced.Tecture.Aspects.Orm.Command, 
	   Reinforced.Tecture.Aspects.Orm.Query
	  >
    { }
```

## Organize your business logic 
Create [services](https://github.com/reinforced/Reinforced.Tecture/wiki/Services) for business logic and produce [commands](https://github.com/reinforced/Reinforced.Tecture/wiki/Commands)

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
		Let<Products>().AttachToOrder(order);
	}
}
```

## Manage read operations 
Define [queries](https://github.com/reinforced/Reinforced.Tecture/wiki/Queries) for your channels

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

## Connect Tecture to your application 
Tecture can be easily registered in any [IoC container](https://github.com/reinforced/Reinforced.Tecture/wiki/Ioc) and used from application

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
		_tecture.Let<Orders>().PerformAction(id);
		_tecture.Save();

		return Ok();
	}
}
```

## Get explanation of your business logic
[Trace](https://github.com/reinforced/Reinforced.Tecture/wiki/Tracing) your business logic and get clear explanation what exactly it does with external systems:

```csharp
tecture.BeginTrace();

var a = tecture.Let<Orders>().CreateOne("new order");
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

## Create unit tests without pain
Extract [test data](https://github.com/reinforced/Reinforced.Tecture/wiki/Test-Data) from the trace and dump it into C# code. Convert tract into [validation code](https://github.com/reinforced/Reinforced.Tecture/wiki/Generate-Validation). Put them together to get data-driven infrastructure-free unit test

```csharp
[Fact]
public void Order_Creation_Works_As_Expected()
{
	using var c = Case<Order_Creation_Works_As_Expected_TestData>(out ITecture ctx);

	var a = ctx.Let<Orders>().CreateOne("test order");
	ctx.Save();
	
	c.Validate<Order_Creation_Works_As_Expected_Validation>();
}
```
