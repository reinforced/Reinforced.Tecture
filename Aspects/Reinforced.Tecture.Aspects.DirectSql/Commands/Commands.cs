
using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Aspects.DirectSql.Toolings;
using _ = Reinforced.Tecture.Services.Tooling;

namespace Reinforced.Tecture.Aspects.DirectSql.Commands
{
    public static partial class Extensions
    {
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1>(this Write<CommandChannel<Command>, MakesSqlCommands> c, Expression<Func<E1, string>> stroke)
        {
            return Before(c, stroke, typeof(E1));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2>(this Write<CommandChannel<Command>, MakesSqlCommands> c, Expression<Func<E1, E2, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3>(this Write<CommandChannel<Command>, MakesSqlCommands> c, Expression<Func<E1, E2, E3, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4>(this Write<CommandChannel<Command>, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7), typeof(E8));
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1>(this Write<CommandChannel<Command>,_, MakesSqlCommands> c, Expression<Func<E1, string>> stroke)
        {
            return Before(c, stroke, typeof(E1));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2>(this Write<CommandChannel<Command>,_, MakesSqlCommands> c, Expression<Func<E1, E2, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3>(this Write<CommandChannel<Command>,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7), typeof(E8));
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1>(this Write<CommandChannel<Command>,_,_, MakesSqlCommands> c, Expression<Func<E1, string>> stroke)
        {
            return Before(c, stroke, typeof(E1));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2>(this Write<CommandChannel<Command>,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3>(this Write<CommandChannel<Command>,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7), typeof(E8));
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1>(this Write<CommandChannel<Command>,_,_,_, MakesSqlCommands> c, Expression<Func<E1, string>> stroke)
        {
            return Before(c, stroke, typeof(E1));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2>(this Write<CommandChannel<Command>,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7), typeof(E8));
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1>(this Write<CommandChannel<Command>,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, string>> stroke)
        {
            return Before(c, stroke, typeof(E1));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2>(this Write<CommandChannel<Command>,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7), typeof(E8));
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1>(this Write<CommandChannel<Command>,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, string>> stroke)
        {
            return Before(c, stroke, typeof(E1));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2>(this Write<CommandChannel<Command>,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7), typeof(E8));
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1>(this Write<CommandChannel<Command>,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, string>> stroke)
        {
            return Before(c, stroke, typeof(E1));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2>(this Write<CommandChannel<Command>,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7), typeof(E8));
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, string>> stroke)
        {
            return Before(c, stroke, typeof(E1));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7));
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Sql Sql<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, MakesSqlCommands> c, Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, string>> stroke)
        {
            return Before(c, stroke, typeof(E1), typeof(E2), typeof(E3), typeof(E4), typeof(E5), typeof(E6), typeof(E7), typeof(E8));
        }
 
 
    }
}

