using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;
// ReSharper disable UnusedMember.Global

namespace Reinforced.Tecture.Features.Orm.Commands.Derelate
{
    /// <summary>
    /// Derelate command extensions
    /// </summary>
    public static partial class Extensions
    {
 
         
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1,TField>(this Write<CommandChannel<Command>,T1> c, T1 entity, Expression<Func<T1,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
         
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2,TField>(this Write<CommandChannel<Command>,T1, T2> c, T1 entity, Expression<Func<T1,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2,TField>(this Write<CommandChannel<Command>,T1, T2> c, T2 entity, Expression<Func<T2,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
         
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T1 entity, Expression<Func<T1,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T2 entity, Expression<Func<T2,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T3 entity, Expression<Func<T3,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
         
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T1 entity, Expression<Func<T1,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T2 entity, Expression<Func<T2,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T3 entity, Expression<Func<T3,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T4 entity, Expression<Func<T4,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
         
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T1 entity, Expression<Func<T1,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T2 entity, Expression<Func<T2,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T3 entity, Expression<Func<T3,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T4 entity, Expression<Func<T4,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T5 entity, Expression<Func<T5,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
         
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T1 entity, Expression<Func<T1,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T2 entity, Expression<Func<T2,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T3 entity, Expression<Func<T3,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T4 entity, Expression<Func<T4,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T5 entity, Expression<Func<T5,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T6 entity, Expression<Func<T6,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
         
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T1 entity, Expression<Func<T1,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T2 entity, Expression<Func<T2,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T3 entity, Expression<Func<T3,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T4 entity, Expression<Func<T4,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T5 entity, Expression<Func<T5,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T6 entity, Expression<Func<T6,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T7 entity, Expression<Func<T7,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
         
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T1 entity, Expression<Func<T1,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T2 entity, Expression<Func<T2,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T3 entity, Expression<Func<T3,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T4 entity, Expression<Func<T4,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T5 entity, Expression<Func<T5,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T6 entity, Expression<Func<T6,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T7 entity, Expression<Func<T7,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
          
    
        /// <summary>
        /// Breaks one-to-many relation between two entities
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <typeparam name="TField"></typeparam>
        /// <param name="c">ORM channel write end</param>
        /// <param name="entity">Entity to create relation from</param>
        /// <param name="key">Relation key property</param>
        /// <returns>Relate command</returns>
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T8 entity, Expression<Func<T8,TField>> key)
        {
            return DeRelateCore(c, entity, GetKey(key));
        }
       
            }
}

