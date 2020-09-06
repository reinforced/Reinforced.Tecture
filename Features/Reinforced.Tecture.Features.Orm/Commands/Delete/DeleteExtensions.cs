using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Commands;
using System.Collections.Generic;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Commands.Delete
{
    public static partial class Extensions
    {
 
         
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T1"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1>(this Write<CommandChannel<Command>,T1> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T1"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2>(this Write<CommandChannel<Command>,T1, T2> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T2"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2>(this Write<CommandChannel<Command>,T1, T2> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T1"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T2"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T3"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T1"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T2"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T3"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T4"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T1"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T2"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T3"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T4"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T5"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T5 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T1"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T2"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T3"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T4"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T5"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T5 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T6"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T6 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T1"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T2"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T3"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T4"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T5"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T5 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T6"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T6 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T7"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T7 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T1"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T2"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T3"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T4"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T5"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T5 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T6"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T6 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T7"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T7 entity)
        {
            return DeleteCore(c, entity);
        }
       
      
    

        /// <summary>
        /// Deletes entity of type <typeparamref name="T8"/>
        /// </summary> 
        /// <typeparam name="T1"></typeparam>  
        /// <typeparam name="T2"></typeparam>  
        /// <typeparam name="T3"></typeparam>  
        /// <typeparam name="T4"></typeparam>  
        /// <typeparam name="T5"></typeparam>  
        /// <typeparam name="T6"></typeparam>  
        /// <typeparam name="T7"></typeparam>  
        /// <typeparam name="T8"></typeparam> 
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T8 entity)
        {
            return DeleteCore(c, entity);
        }
       
            }
}

