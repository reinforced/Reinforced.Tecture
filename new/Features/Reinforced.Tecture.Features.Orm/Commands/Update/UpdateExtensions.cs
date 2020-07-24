
using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Commands.Update
{
    public static partial class Extensions
    {
 
         
             
        public static Update Update<T1>(this Write<CommandChannel<Command>,T1> c, T1 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1>(this Write<CommandChannel<Command>,T1> c, T1 entity, params Expression<Func<T1, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
         
             
        public static Update Update<T1, T2>(this Write<CommandChannel<Command>,T1, T2> c, T1 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2>(this Write<CommandChannel<Command>,T1, T2> c, T1 entity, params Expression<Func<T1, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2>(this Write<CommandChannel<Command>,T1, T2> c, T2 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2>(this Write<CommandChannel<Command>,T1, T2> c, T2 entity, params Expression<Func<T2, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
         
             
        public static Update Update<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T1 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T1 entity, params Expression<Func<T1, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T2 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T2 entity, params Expression<Func<T2, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T3 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3>(this Write<CommandChannel<Command>,T1, T2, T3> c, T3 entity, params Expression<Func<T3, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
         
             
        public static Update Update<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T1 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T1 entity, params Expression<Func<T1, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T2 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T2 entity, params Expression<Func<T2, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T3 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T3 entity, params Expression<Func<T3, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T4 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T4 entity, params Expression<Func<T4, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
         
             
        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T1 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T1 entity, params Expression<Func<T1, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T2 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T2 entity, params Expression<Func<T2, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T3 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T3 entity, params Expression<Func<T3, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T4 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T4 entity, params Expression<Func<T4, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T5 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T5 entity, params Expression<Func<T5, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
         
             
        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T1 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T1 entity, params Expression<Func<T1, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T2 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T2 entity, params Expression<Func<T2, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T3 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T3 entity, params Expression<Func<T3, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T4 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T4 entity, params Expression<Func<T4, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T5 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T5 entity, params Expression<Func<T5, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T6 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T6 entity, params Expression<Func<T6, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
         
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T1 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T1 entity, params Expression<Func<T1, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T2 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T2 entity, params Expression<Func<T2, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T3 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T3 entity, params Expression<Func<T3, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T4 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T4 entity, params Expression<Func<T4, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T5 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T5 entity, params Expression<Func<T5, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T6 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T6 entity, params Expression<Func<T6, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T7 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T7 entity, params Expression<Func<T7, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
         
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T1 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T1 entity, params Expression<Func<T1, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T2 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T2 entity, params Expression<Func<T2, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T3 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T3 entity, params Expression<Func<T3, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T4 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T4 entity, params Expression<Func<T4, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T5 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T5 entity, params Expression<Func<T5, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T6 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T6 entity, params Expression<Func<T6, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T7 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T7 entity, params Expression<Func<T7, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
             
        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T8 entity)
        {
            return UpdateCore(c, entity);
        }       

        public static Update Update<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T8 entity, params Expression<Func<T8, object>>[] properties)
        {
            return UpdateCore(c, entity);
        }
            
    }
}

