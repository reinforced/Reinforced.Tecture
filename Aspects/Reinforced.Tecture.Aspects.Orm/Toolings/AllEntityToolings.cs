
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Aspects.Orm.Toolings
{
 

    /// <summary>
    /// Service tooling that adds entities
    /// </summary>
    public interface Adds : Tooling { }

    /// <summary>
    /// Service tooling that adds 1 entity
    /// </summary>
    public interface Adds<out T1> : Adds { }

 

    /// <summary>
    /// Service tooling that adds 2 entities
    /// </summary>
    public interface Adds<out T1, out T2> : Adds<T1> { }
 

    /// <summary>
    /// Service tooling that adds 3 entities
    /// </summary>
    public interface Adds<out T1, out T2, out T3> : Adds<T1, T2> { }
 

    /// <summary>
    /// Service tooling that adds 4 entities
    /// </summary>
    public interface Adds<out T1, out T2, out T3, out T4> : Adds<T1, T2, T3> { }
 

    /// <summary>
    /// Service tooling that adds 5 entities
    /// </summary>
    public interface Adds<out T1, out T2, out T3, out T4, out T5> : Adds<T1, T2, T3, T4> { }
 

    /// <summary>
    /// Service tooling that adds 6 entities
    /// </summary>
    public interface Adds<out T1, out T2, out T3, out T4, out T5, out T6> : Adds<T1, T2, T3, T4, T5> { }
 

    /// <summary>
    /// Service tooling that adds 7 entities
    /// </summary>
    public interface Adds<out T1, out T2, out T3, out T4, out T5, out T6, out T7> : Adds<T1, T2, T3, T4, T5, T6> { }
 

    /// <summary>
    /// Service tooling that adds 8 entities
    /// </summary>
    public interface Adds<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8> : Adds<T1, T2, T3, T4, T5, T6, T7> { }
 

    /// <summary>
    /// Service tooling that deletes entities
    /// </summary>
    public interface Deletes : Tooling { }

    /// <summary>
    /// Service tooling that deletes 1 entity
    /// </summary>
    public interface Deletes<out T1> : Deletes { }

 

    /// <summary>
    /// Service tooling that deletes 2 entities
    /// </summary>
    public interface Deletes<out T1, out T2> : Deletes<T1> { }
 

    /// <summary>
    /// Service tooling that deletes 3 entities
    /// </summary>
    public interface Deletes<out T1, out T2, out T3> : Deletes<T1, T2> { }
 

    /// <summary>
    /// Service tooling that deletes 4 entities
    /// </summary>
    public interface Deletes<out T1, out T2, out T3, out T4> : Deletes<T1, T2, T3> { }
 

    /// <summary>
    /// Service tooling that deletes 5 entities
    /// </summary>
    public interface Deletes<out T1, out T2, out T3, out T4, out T5> : Deletes<T1, T2, T3, T4> { }
 

    /// <summary>
    /// Service tooling that deletes 6 entities
    /// </summary>
    public interface Deletes<out T1, out T2, out T3, out T4, out T5, out T6> : Deletes<T1, T2, T3, T4, T5> { }
 

    /// <summary>
    /// Service tooling that deletes 7 entities
    /// </summary>
    public interface Deletes<out T1, out T2, out T3, out T4, out T5, out T6, out T7> : Deletes<T1, T2, T3, T4, T5, T6> { }
 

    /// <summary>
    /// Service tooling that deletes 8 entities
    /// </summary>
    public interface Deletes<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8> : Deletes<T1, T2, T3, T4, T5, T6, T7> { }
 

    /// <summary>
    /// Service tooling that updates entities
    /// </summary>
    public interface Updates : Tooling { }

    /// <summary>
    /// Service tooling that updates 1 entity
    /// </summary>
    public interface Updates<out T1> : Updates { }

 

    /// <summary>
    /// Service tooling that updates 2 entities
    /// </summary>
    public interface Updates<out T1, out T2> : Updates<T1> { }
 

    /// <summary>
    /// Service tooling that updates 3 entities
    /// </summary>
    public interface Updates<out T1, out T2, out T3> : Updates<T1, T2> { }
 

    /// <summary>
    /// Service tooling that updates 4 entities
    /// </summary>
    public interface Updates<out T1, out T2, out T3, out T4> : Updates<T1, T2, T3> { }
 

    /// <summary>
    /// Service tooling that updates 5 entities
    /// </summary>
    public interface Updates<out T1, out T2, out T3, out T4, out T5> : Updates<T1, T2, T3, T4> { }
 

    /// <summary>
    /// Service tooling that updates 6 entities
    /// </summary>
    public interface Updates<out T1, out T2, out T3, out T4, out T5, out T6> : Updates<T1, T2, T3, T4, T5> { }
 

    /// <summary>
    /// Service tooling that updates 7 entities
    /// </summary>
    public interface Updates<out T1, out T2, out T3, out T4, out T5, out T6, out T7> : Updates<T1, T2, T3, T4, T5, T6> { }
 

    /// <summary>
    /// Service tooling that updates 8 entities
    /// </summary>
    public interface Updates<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8> : Updates<T1, T2, T3, T4, T5, T6, T7> { }

    /// <summary>
    /// Service tooling that adds, deletes, updates entities
    /// </summary>
    public interface Modifies : Adds, Deletes, Updates { }
 
    
    /// <summary>
    /// Service tooling that deletes, updates 1 entities
    /// </summary>
    public interface Modifies<out T1> : 
		Adds<T1>, 
		Deletes<T1>, 
		Updates<T1> { }
 
    
    /// <summary>
    /// Service tooling that  deletes, updates 2 entities
    /// </summary>
    public interface Modifies<out T1, out T2> : 
		Adds<T1, T2>, 
		Deletes<T1, T2>, 
		Updates<T1, T2> { }
 
    
    /// <summary>
    /// Service tooling that  deletes, updates 3 entities
    /// </summary>
    public interface Modifies<out T1, out T2, out T3> : 
		Adds<T1, T2, T3>, 
		Deletes<T1, T2, T3>, 
		Updates<T1, T2, T3> { }
 
    
    /// <summary>
    /// Service tooling that  deletes, updates 4 entities
    /// </summary>
    public interface Modifies<out T1, out T2, out T3, out T4> : 
		Adds<T1, T2, T3, T4>, 
		Deletes<T1, T2, T3, T4>, 
		Updates<T1, T2, T3, T4> { }
 
    
    /// <summary>
    /// Service tooling that  deletes, updates 5 entities
    /// </summary>
    public interface Modifies<out T1, out T2, out T3, out T4, out T5> : 
		Adds<T1, T2, T3, T4, T5>, 
		Deletes<T1, T2, T3, T4, T5>, 
		Updates<T1, T2, T3, T4, T5> { }
 
    
    /// <summary>
    /// Service tooling that  deletes, updates 6 entities
    /// </summary>
    public interface Modifies<out T1, out T2, out T3, out T4, out T5, out T6> : 
		Adds<T1, T2, T3, T4, T5, T6>, 
		Deletes<T1, T2, T3, T4, T5, T6>, 
		Updates<T1, T2, T3, T4, T5, T6> { }
 
    
    /// <summary>
    /// Service tooling that  deletes, updates 7 entities
    /// </summary>
    public interface Modifies<out T1, out T2, out T3, out T4, out T5, out T6, out T7> : 
		Adds<T1, T2, T3, T4, T5, T6, T7>, 
		Deletes<T1, T2, T3, T4, T5, T6, T7>, 
		Updates<T1, T2, T3, T4, T5, T6, T7> { }
 
    
    /// <summary>
    /// Service tooling that adds, deletes, updates 8 entities
    /// </summary>
    public interface Modifies<out T1, out T2, out T3, out T4, out T5, out T6, out T7, out T8> : 
		Adds<T1, T2, T3, T4, T5, T6, T7, T8>, 
		Deletes<T1, T2, T3, T4, T5, T6, T7, T8>, 
		Updates<T1, T2, T3, T4, T5, T6, T7, T8> { }
}

