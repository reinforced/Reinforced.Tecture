using System.Collections.Generic;
using System.Linq;

namespace Reinforced.Tecture.Features.SqlStroke
{
    /// <summary>
    /// Stroke relations class. Currently is under development
    /// </summary>
    public static class StrokeRelations
    {
        /// <summary>
        /// Produces FROM clause for specified entity.
        /// Nested joins can still be overriden with .Overjoin
        /// </summary>
        /// <param name="type">Nested entities join type</param>
        /// <param name="entity">Entity to produce from clause</param>
        /// <returns></returns>
        public static bool From(object entity, Join type = Join.Inner)
        {
            return false;
        }

        /// <summary>
        /// Takes evey record from associated collection of <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Associated entity type</typeparam>
        /// <param name="source">Associated collection</param>
        /// <returns>Dummy</returns>
        public static T Every<T>(this IEnumerable<T> source)
        {
            return source.FirstOrDefault();
        }

        /// <summary>
        /// Exposes relation table between <typeparamref name="T"/> and its parent entity.
        /// Relation can be obtained only for * - * relations
        /// </summary>
        /// <typeparam name="T">Child entity</typeparam>
        /// <param name="source">Relation collection</param>
        /// <returns>Dummy</returns>
        public static bool Relation<T>(this IEnumerable<T> source)
        {
            return false;
        }
    }

   
}
