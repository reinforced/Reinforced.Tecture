

using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using _ = Reinforced.Tecture.Services.Tooling;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Update
{
    public static partial class Extensions
    {
 
        

 
        /// <summary>
        /// Updates entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E1>
            Update<E1>(
            this Write<CommandChannel<Command>, Updates<E1>> c, E1 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E2>
            Update<E1, E2>(
            this Write<CommandChannel<Command>, Updates<E1, E2>> c, E2 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E3>
            Update<E1, E2, E3>(
            this Write<CommandChannel<Command>, Updates<E1, E2, E3>> c, E3 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E4>
            Update<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4>> c, E4 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E5>
            Update<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E6>
            Update<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E7>
            Update<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E8>
            Update<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
 
        

 
        /// <summary>
        /// Updates entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E1>
            Update<E1>(
            this Write<CommandChannel<Command>,_, Updates<E1>> c, E1 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E2>
            Update<E1, E2>(
            this Write<CommandChannel<Command>,_, Updates<E1, E2>> c, E2 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E3>
            Update<E1, E2, E3>(
            this Write<CommandChannel<Command>,_, Updates<E1, E2, E3>> c, E3 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E4>
            Update<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4>> c, E4 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E5>
            Update<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E6>
            Update<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E7>
            Update<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E8>
            Update<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
 
        

 
        /// <summary>
        /// Updates entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E1>
            Update<E1>(
            this Write<CommandChannel<Command>,_,_, Updates<E1>> c, E1 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E2>
            Update<E1, E2>(
            this Write<CommandChannel<Command>,_,_, Updates<E1, E2>> c, E2 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E3>
            Update<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3>> c, E3 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E4>
            Update<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4>> c, E4 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E5>
            Update<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E6>
            Update<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E7>
            Update<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E8>
            Update<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
 
        

 
        /// <summary>
        /// Updates entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E1>
            Update<E1>(
            this Write<CommandChannel<Command>,_,_,_, Updates<E1>> c, E1 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E2>
            Update<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2>> c, E2 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E3>
            Update<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3>> c, E3 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E4>
            Update<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E5>
            Update<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E6>
            Update<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E7>
            Update<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E8>
            Update<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
 
        

 
        /// <summary>
        /// Updates entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E1>
            Update<E1>(
            this Write<CommandChannel<Command>,_,_,_,_, Updates<E1>> c, E1 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E2>
            Update<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2>> c, E2 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E3>
            Update<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3>> c, E3 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E4>
            Update<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E5>
            Update<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E6>
            Update<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E7>
            Update<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E8>
            Update<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
 
        

 
        /// <summary>
        /// Updates entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E1>
            Update<E1>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1>> c, E1 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E2>
            Update<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2>> c, E2 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E3>
            Update<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3>> c, E3 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E4>
            Update<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E5>
            Update<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E6>
            Update<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E7>
            Update<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E8>
            Update<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
 
        

 
        /// <summary>
        /// Updates entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E1>
            Update<E1>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1>> c, E1 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E2>
            Update<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2>> c, E2 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E3>
            Update<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3>> c, E3 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E4>
            Update<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E5>
            Update<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E6>
            Update<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E7>
            Update<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E8>
            Update<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
 
        

 
        /// <summary>
        /// Updates entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E1>
            Update<E1>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1>> c, E1 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E2>
            Update<E1, E2>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2>> c, E2 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E3>
            Update<E1, E2, E3>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3>> c, E3 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E4>
            Update<E1, E2, E3, E4>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4>> c, E4 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E5>
            Update<E1, E2, E3, E4, E5>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E6>
            Update<E1, E2, E3, E4, E5, E6>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E7>
            Update<E1, E2, E3, E4, E5, E6, E7>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
        /// <summary>
        /// Updates entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Fluent</returns>
        public static Update<E8>
            Update<E1, E2, E3, E4, E5, E6, E7, E8>(
            this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Updates<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return UpdateCore(c, entity);
        }
       
 
 
    }
}

