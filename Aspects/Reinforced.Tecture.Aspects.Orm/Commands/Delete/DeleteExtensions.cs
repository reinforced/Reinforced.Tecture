

using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Services;
using _ = Reinforced.Tecture.Services.Tooling;
using o = System.Object;
using static Reinforced.Tecture.Aspects.Orm.Orm;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Delete
{
    public static partial class Extensions
    {
 
        

 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1>(
            this Write<CommandChannel<Command>, Deletes<E1>> c, E1 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,Deletes<E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2>(
            this Write<CommandChannel<Command>, Deletes<E1, E2>> c, E2 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,Deletes<o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3>(
            this Write<CommandChannel<Command>, Deletes<E1, E2, E3>> c, E3 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,Deletes<o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>, Deletes<E1, E2, E3, E4>> c, E4 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,Deletes<o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>, Deletes<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,Deletes<o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>, Deletes<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,Deletes<o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>, Deletes<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,Deletes<o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>, Deletes<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,Deletes<o,o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
 
        

 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1>(
            this Write<CommandChannel<Command>,_, Deletes<E1>> c, E1 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,Deletes<E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2>(
            this Write<CommandChannel<Command>,_, Deletes<E1, E2>> c, E2 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,Deletes<o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3>(
            this Write<CommandChannel<Command>,_, Deletes<E1, E2, E3>> c, E3 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,Deletes<o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_, Deletes<E1, E2, E3, E4>> c, E4 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,Deletes<o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_, Deletes<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,Deletes<o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_, Deletes<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,Deletes<o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_, Deletes<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,Deletes<o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_, Deletes<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,Deletes<o,o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
 
        

 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1>(
            this Write<CommandChannel<Command>,_,_, Deletes<E1>> c, E1 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,Deletes<E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2>(
            this Write<CommandChannel<Command>,_,_, Deletes<E1, E2>> c, E2 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,Deletes<o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_, Deletes<E1, E2, E3>> c, E3 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,Deletes<o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_, Deletes<E1, E2, E3, E4>> c, E4 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,Deletes<o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_, Deletes<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,Deletes<o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_, Deletes<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,Deletes<o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,Deletes<o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,Deletes<o,o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
 
        

 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1>(
            this Write<CommandChannel<Command>,_,_,_, Deletes<E1>> c, E1 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,Deletes<E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_, Deletes<E1, E2>> c, E2 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,Deletes<o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_, Deletes<E1, E2, E3>> c, E3 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,Deletes<o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_, Deletes<E1, E2, E3, E4>> c, E4 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,Deletes<o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_, Deletes<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,Deletes<o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_, Deletes<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,Deletes<o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,Deletes<o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,Deletes<o,o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
 
        

 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1>(
            this Write<CommandChannel<Command>,_,_,_,_, Deletes<E1>> c, E1 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,Deletes<E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_,_, Deletes<E1, E2>> c, E2 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,Deletes<o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_,_, Deletes<E1, E2, E3>> c, E3 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,Deletes<o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_,_, Deletes<E1, E2, E3, E4>> c, E4 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,Deletes<o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_,_, Deletes<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,Deletes<o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,Deletes<o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,Deletes<o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,Deletes<o,o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
 
        

 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Deletes<E1>> c, E1 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,Deletes<E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Deletes<E1, E2>> c, E2 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,Deletes<o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Deletes<E1, E2, E3>> c, E3 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,Deletes<o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Deletes<E1, E2, E3, E4>> c, E4 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,Deletes<o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,Deletes<o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,Deletes<o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,Deletes<o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,Deletes<o,o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
 
        

 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Deletes<E1>> c, E1 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,Deletes<E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Deletes<E1, E2>> c, E2 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,Deletes<o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Deletes<E1, E2, E3>> c, E3 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,Deletes<o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Deletes<E1, E2, E3, E4>> c, E4 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,Deletes<o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,Deletes<o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,Deletes<o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,Deletes<o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,Deletes<o,o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
 
        

 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Deletes<E1>> c, E1 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_,Deletes<E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Deletes<E1, E2>> c, E2 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_,Deletes<o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Deletes<E1, E2, E3>> c, E3 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_,Deletes<o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Deletes<E1, E2, E3, E4>> c, E4 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_,Deletes<o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_,Deletes<o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_,Deletes<o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_,Deletes<o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
        /// <summary>
        /// Deletes entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Delete 
            Delete<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Deletes<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return DeleteCore(c, entity);
        }

        /// <summary>
        /// Deletes entity by primary key
        /// </summary>
        /// <typeparam name="E">Entity to be deleted</typeparam>
        /// <param name="channel">Channel</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<DeletePk.DeletePk, E> Delete<E>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_,Deletes<o,o,o,o,o,o,o,E>> channel)
            where E : IPrimaryKey
        {
            return new DeletePkOperation<E>() { Write = channel, EntityType = typeof(E) };
        }

       
 
 
    }
}

