using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Channels;
using _ = Reinforced.Tecture.Services.Tooling;
namespace Reinforced.Tecture.Aspects.Orm.Commands.Relate
{
    public static partial class Extensions
    {
 

 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E1"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, TField>(this Write<CommandChannel<Command>, Updates<E1>> c, E1 entity, Expression<Func<E1,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E2"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, TField>(this Write<CommandChannel<Command>, Updates<E1, E2>> c, E2 entity, Expression<Func<E2,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E3"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, TField>(this Write<CommandChannel<Command>, Updates<E1, E2, E3>> c, E3 entity, Expression<Func<E3,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E4"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, TField>(this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4>> c, E4 entity, Expression<Func<E4,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E5"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, TField>(this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4, E5>> c, E5 entity, Expression<Func<E5,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E6"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, TField>(this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity, Expression<Func<E6,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E7"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, TField>(this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity, Expression<Func<E7,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E8"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, E8, TField>(this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity, Expression<Func<E8,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
 

 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E1"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, TField>(this Write<CommandChannel<Command>,_, Updates<E1>> c, E1 entity, Expression<Func<E1,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E2"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, TField>(this Write<CommandChannel<Command>,_, Updates<E1, E2>> c, E2 entity, Expression<Func<E2,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E3"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, TField>(this Write<CommandChannel<Command>,_, Updates<E1, E2, E3>> c, E3 entity, Expression<Func<E3,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E4"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, TField>(this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4>> c, E4 entity, Expression<Func<E4,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E5"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, TField>(this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity, Expression<Func<E5,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E6"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, TField>(this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity, Expression<Func<E6,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E7"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, TField>(this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity, Expression<Func<E7,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E8"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, E8, TField>(this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity, Expression<Func<E8,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
 

 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E1"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, TField>(this Write<CommandChannel<Command>,_,_, Updates<E1>> c, E1 entity, Expression<Func<E1,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E2"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, TField>(this Write<CommandChannel<Command>,_,_, Updates<E1, E2>> c, E2 entity, Expression<Func<E2,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E3"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, TField>(this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3>> c, E3 entity, Expression<Func<E3,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E4"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, TField>(this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4>> c, E4 entity, Expression<Func<E4,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E5"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, TField>(this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity, Expression<Func<E5,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E6"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, TField>(this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity, Expression<Func<E6,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E7"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, TField>(this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity, Expression<Func<E7,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E8"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, E8, TField>(this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity, Expression<Func<E8,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
 

 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E1"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, TField>(this Write<CommandChannel<Command>,_,_,_, Updates<E1>> c, E1 entity, Expression<Func<E1,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E2"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, TField>(this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2>> c, E2 entity, Expression<Func<E2,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E3"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, TField>(this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3>> c, E3 entity, Expression<Func<E3,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E4"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, TField>(this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity, Expression<Func<E4,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E5"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, TField>(this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity, Expression<Func<E5,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E6"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, TField>(this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity, Expression<Func<E6,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E7"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, TField>(this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity, Expression<Func<E7,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E8"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, E8, TField>(this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity, Expression<Func<E8,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
 

 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E1"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, TField>(this Write<CommandChannel<Command>,_,_,_,_, Updates<E1>> c, E1 entity, Expression<Func<E1,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E2"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, TField>(this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2>> c, E2 entity, Expression<Func<E2,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E3"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, TField>(this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3>> c, E3 entity, Expression<Func<E3,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E4"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, TField>(this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity, Expression<Func<E4,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E5"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, TField>(this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity, Expression<Func<E5,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E6"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, TField>(this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity, Expression<Func<E6,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E7"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, TField>(this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity, Expression<Func<E7,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E8"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, E8, TField>(this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity, Expression<Func<E8,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
 

 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E1"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, TField>(this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1>> c, E1 entity, Expression<Func<E1,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E2"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, TField>(this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2>> c, E2 entity, Expression<Func<E2,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E3"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, TField>(this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3>> c, E3 entity, Expression<Func<E3,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E4"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, TField>(this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity, Expression<Func<E4,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E5"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, TField>(this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity, Expression<Func<E5,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E6"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, TField>(this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity, Expression<Func<E6,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E7"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, TField>(this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity, Expression<Func<E7,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E8"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, E8, TField>(this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity, Expression<Func<E8,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
 

 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E1"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1>> c, E1 entity, Expression<Func<E1,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E2"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2>> c, E2 entity, Expression<Func<E2,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E3"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3>> c, E3 entity, Expression<Func<E3,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E4"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity, Expression<Func<E4,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E5"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity, Expression<Func<E5,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E6"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity, Expression<Func<E6,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E7"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity, Expression<Func<E7,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E8"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, E8, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity, Expression<Func<E8,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
 

 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E1"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1>> c, E1 entity, Expression<Func<E1,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E2"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2>> c, E2 entity, Expression<Func<E2,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E3"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3>> c, E3 entity, Expression<Func<E3,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E4"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity, Expression<Func<E4,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E5"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity, Expression<Func<E5,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E6"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity, Expression<Func<E6,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E7"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity, Expression<Func<E7,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
        /// <summary>
        /// Creates one-to-many relation between entity <typeparamref name="E8"/> and <typeparamref name="TField"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity subject</param>
        /// <param name="key">Relation navigation property</param>
        /// <param name="related">Related entity</param>
        /// <returns>Relate command</returns>
        public static Relate
            Relate<E1, E2, E3, E4, E5, E6, E7, E8, TField>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity, Expression<Func<E8,TField>> key, TField related)
        {
            return RelateCore(c, entity, GetKey(key), related);
        }
 
 
    }
}

