using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Commands;
using System.Collections.Generic;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Command.Delete
{
    public static partial class Extensions
    {
 
         
             
        public static Delete Delete<T1>(this Write<CommandChannel<Orm>,T1> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
             
        public static Delete Delete<T1, T2>(this Write<CommandChannel<Orm>,T1, T2> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2>(this Write<CommandChannel<Orm>,T1, T2> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
             
        public static Delete Delete<T1, T2, T3>(this Write<CommandChannel<Orm>,T1, T2, T3> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3>(this Write<CommandChannel<Orm>,T1, T2, T3> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3>(this Write<CommandChannel<Orm>,T1, T2, T3> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
             
        public static Delete Delete<T1, T2, T3, T4>(this Write<CommandChannel<Orm>,T1, T2, T3, T4> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4>(this Write<CommandChannel<Orm>,T1, T2, T3, T4> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4>(this Write<CommandChannel<Orm>,T1, T2, T3, T4> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4>(this Write<CommandChannel<Orm>,T1, T2, T3, T4> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
             
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5> c, T5 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6> c, T5 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6> c, T6 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7> c, T5 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7> c, T6 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7> c, T7 entity)
        {
            return DeleteCore(c, entity);
        }
       
         
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7, T8> c, T1 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7, T8> c, T2 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7, T8> c, T3 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7, T8> c, T4 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7, T8> c, T5 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7, T8> c, T6 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7, T8> c, T7 entity)
        {
            return DeleteCore(c, entity);
        }
       
             
        public static Delete Delete<T1, T2, T3, T4, T5, T6, T7, T8>(this Write<CommandChannel<Orm>,T1, T2, T3, T4, T5, T6, T7, T8> c, T8 entity)
        {
            return DeleteCore(c, entity);
        }
       
            }
}

