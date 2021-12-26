
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Services;
using _ = Reinforced.Tecture.Services.Tooling;
using static Reinforced.Tecture.Aspects.Orm.Orm;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Add
{
    public static partial class Extensions
    {
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E1> 
            Add<E1>(this Write<CommandChannel<Command>, Adds<E1>> c, E1 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E2> 
            Add<E1, E2>(this Write<CommandChannel<Command>, Adds<E1, E2>> c, E2 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E3> 
            Add<E1, E2, E3>(this Write<CommandChannel<Command>, Adds<E1, E2, E3>> c, E3 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E4> 
            Add<E1, E2, E3, E4>(this Write<CommandChannel<Command>, Adds<E1, E2, E3, E4>> c, E4 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E5> 
            Add<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>, Adds<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E6> 
            Add<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>, Adds<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E7> 
            Add<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>, Adds<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E8> 
            Add<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>, Adds<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return AddCore(c, entity);
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E1> 
            Add<E1>(this Write<CommandChannel<Command>,_, Adds<E1>> c, E1 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E2> 
            Add<E1, E2>(this Write<CommandChannel<Command>,_, Adds<E1, E2>> c, E2 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E3> 
            Add<E1, E2, E3>(this Write<CommandChannel<Command>,_, Adds<E1, E2, E3>> c, E3 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E4> 
            Add<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_, Adds<E1, E2, E3, E4>> c, E4 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E5> 
            Add<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_, Adds<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E6> 
            Add<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_, Adds<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E7> 
            Add<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_, Adds<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E8> 
            Add<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_, Adds<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return AddCore(c, entity);
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E1> 
            Add<E1>(this Write<CommandChannel<Command>,_,_, Adds<E1>> c, E1 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E2> 
            Add<E1, E2>(this Write<CommandChannel<Command>,_,_, Adds<E1, E2>> c, E2 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E3> 
            Add<E1, E2, E3>(this Write<CommandChannel<Command>,_,_, Adds<E1, E2, E3>> c, E3 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E4> 
            Add<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_, Adds<E1, E2, E3, E4>> c, E4 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E5> 
            Add<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_, Adds<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E6> 
            Add<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_, Adds<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E7> 
            Add<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_, Adds<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E8> 
            Add<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_, Adds<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return AddCore(c, entity);
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E1> 
            Add<E1>(this Write<CommandChannel<Command>,_,_,_, Adds<E1>> c, E1 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E2> 
            Add<E1, E2>(this Write<CommandChannel<Command>,_,_,_, Adds<E1, E2>> c, E2 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E3> 
            Add<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_, Adds<E1, E2, E3>> c, E3 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E4> 
            Add<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_, Adds<E1, E2, E3, E4>> c, E4 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E5> 
            Add<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_, Adds<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E6> 
            Add<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_, Adds<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E7> 
            Add<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E8> 
            Add<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return AddCore(c, entity);
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E1> 
            Add<E1>(this Write<CommandChannel<Command>,_,_,_,_, Adds<E1>> c, E1 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E2> 
            Add<E1, E2>(this Write<CommandChannel<Command>,_,_,_,_, Adds<E1, E2>> c, E2 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E3> 
            Add<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_,_, Adds<E1, E2, E3>> c, E3 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E4> 
            Add<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_,_, Adds<E1, E2, E3, E4>> c, E4 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E5> 
            Add<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_,_, Adds<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E6> 
            Add<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E7> 
            Add<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E8> 
            Add<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return AddCore(c, entity);
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E1> 
            Add<E1>(this Write<CommandChannel<Command>,_,_,_,_,_, Adds<E1>> c, E1 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E2> 
            Add<E1, E2>(this Write<CommandChannel<Command>,_,_,_,_,_, Adds<E1, E2>> c, E2 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E3> 
            Add<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_,_,_, Adds<E1, E2, E3>> c, E3 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E4> 
            Add<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_,_,_, Adds<E1, E2, E3, E4>> c, E4 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E5> 
            Add<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_,_,_, Adds<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E6> 
            Add<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E7> 
            Add<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E8> 
            Add<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return AddCore(c, entity);
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E1> 
            Add<E1>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Adds<E1>> c, E1 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E2> 
            Add<E1, E2>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Adds<E1, E2>> c, E2 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E3> 
            Add<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Adds<E1, E2, E3>> c, E3 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E4> 
            Add<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Adds<E1, E2, E3, E4>> c, E4 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E5> 
            Add<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Adds<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E6> 
            Add<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E7> 
            Add<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E8> 
            Add<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return AddCore(c, entity);
        }
 
 

 
        /// <summary>
        /// Adds entity of type <typeparamref name="E1"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E1> 
            Add<E1>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Adds<E1>> c, E1 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E2"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E2> 
            Add<E1, E2>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Adds<E1, E2>> c, E2 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E3"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E3> 
            Add<E1, E2, E3>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Adds<E1, E2, E3>> c, E3 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E4"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E4> 
            Add<E1, E2, E3, E4>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Adds<E1, E2, E3, E4>> c, E4 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E5"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E5> 
            Add<E1, E2, E3, E4, E5>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Adds<E1, E2, E3, E4, E5>> c, E5 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E6"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E6> 
            Add<E1, E2, E3, E4, E5, E6>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6>> c, E6 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E7"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E7> 
            Add<E1, E2, E3, E4, E5, E6, E7>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7>> c, E7 entity)
        {
            return AddCore(c, entity);
        }
 
        /// <summary>
        /// Adds entity of type <typeparamref name="E8"/>
        /// </summary>
        /// <param name="c">ORM Channel Write End</param>
        /// <param name="entity">Entity to add</param>
        /// <returns>Fluent</returns>
        public static Add<E8> 
            Add<E1, E2, E3, E4, E5, E6, E7, E8>(this Write<CommandChannel<Command>,_,_,_,_,_,_,_, Adds<E1, E2, E3, E4, E5, E6, E7, E8>> c, E8 entity)
        {
            return AddCore(c, entity);
        }
 
 
    }
}

