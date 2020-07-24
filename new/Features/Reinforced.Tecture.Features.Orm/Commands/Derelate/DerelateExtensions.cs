using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Commands;
using System.Collections.Generic;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Commands.Derelate
{
    public static partial class Extensions
    {
 
         
             
        public static Derelate Derelate<T1,TField>(this Write<CommandChannel<Command>,T1> c, T1 entity, Expression<Func<T1,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1,TField>(this Write<CommandChannel<Command>,T1> c, T1 entity, Expression<Func<T1,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
         
             
        public static Derelate Derelate<T1, T2,TField>(this Write<CommandChannel<Command>,T1, T2> c, T1 entity, Expression<Func<T1,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2,TField>(this Write<CommandChannel<Command>,T1, T2> c, T1 entity, Expression<Func<T1,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2,TField>(this Write<CommandChannel<Command>,T1, T2> c, T2 entity, Expression<Func<T2,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2,TField>(this Write<CommandChannel<Command>,T1, T2> c, T2 entity, Expression<Func<T2,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
         
             
        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T1 entity, Expression<Func<T1,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T1 entity, Expression<Func<T1,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T2 entity, Expression<Func<T2,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T2 entity, Expression<Func<T2,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T3 entity, Expression<Func<T3,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3,TField>(this Write<CommandChannel<Command>,T1, T2, T3> c, T3 entity, Expression<Func<T3,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
         
             
        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T1 entity, Expression<Func<T1,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T1 entity, Expression<Func<T1,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T2 entity, Expression<Func<T2,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T2 entity, Expression<Func<T2,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T3 entity, Expression<Func<T3,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T3 entity, Expression<Func<T3,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T4 entity, Expression<Func<T4,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4> c, T4 entity, Expression<Func<T4,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
         
             
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T1 entity, Expression<Func<T1,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T1 entity, Expression<Func<T1,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T2 entity, Expression<Func<T2,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T2 entity, Expression<Func<T2,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T3 entity, Expression<Func<T3,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T3 entity, Expression<Func<T3,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T4 entity, Expression<Func<T4,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T4 entity, Expression<Func<T4,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T5 entity, Expression<Func<T5,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5> c, T5 entity, Expression<Func<T5,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
         
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T1 entity, Expression<Func<T1,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T1 entity, Expression<Func<T1,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T2 entity, Expression<Func<T2,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T2 entity, Expression<Func<T2,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T3 entity, Expression<Func<T3,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T3 entity, Expression<Func<T3,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T4 entity, Expression<Func<T4,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T4 entity, Expression<Func<T4,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T5 entity, Expression<Func<T5,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T5 entity, Expression<Func<T5,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T6 entity, Expression<Func<T6,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6> c, T6 entity, Expression<Func<T6,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
         
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T1 entity, Expression<Func<T1,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T1 entity, Expression<Func<T1,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T2 entity, Expression<Func<T2,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T2 entity, Expression<Func<T2,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T3 entity, Expression<Func<T3,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T3 entity, Expression<Func<T3,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T4 entity, Expression<Func<T4,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T4 entity, Expression<Func<T4,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T5 entity, Expression<Func<T5,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T5 entity, Expression<Func<T5,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T6 entity, Expression<Func<T6,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T6 entity, Expression<Func<T6,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T7 entity, Expression<Func<T7,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7> c, T7 entity, Expression<Func<T7,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
         
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T1 entity, Expression<Func<T1,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T1 entity, Expression<Func<T1,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T2 entity, Expression<Func<T2,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T2 entity, Expression<Func<T2,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T3 entity, Expression<Func<T3,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T3 entity, Expression<Func<T3,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T4 entity, Expression<Func<T4,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T4 entity, Expression<Func<T4,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T5 entity, Expression<Func<T5,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T5 entity, Expression<Func<T5,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T6 entity, Expression<Func<T6,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T6 entity, Expression<Func<T6,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T7 entity, Expression<Func<T7,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T7 entity, Expression<Func<T7,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
             
        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T8 entity, Expression<Func<T8,TField>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }

        public static Derelate Derelate<T1, T2, T3, T4, T5, T6, T7, T8,TField>(this Write<CommandChannel<Command>,T1, T2, T3, T4, T5, T6, T7, T8> c, T8 entity, Expression<Func<T8,IEnumerable<TField>>> key, TField derelated)
        {
            return DeRelateCore(c, entity, GetKey(key), derelated);
        }
       
            }
}

