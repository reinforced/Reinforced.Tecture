namespace Reinforced.Tecture.Aspects.DirectSql
{
    /// <summary>
    /// SQL stroke query helper methods
    /// </summary>
    public static class StrokeJoins
    {
        /// <summary>
        /// Instructs nested aggregate of entity to be joined using particular join type
        /// </summary>
        /// <param name="entity">Entity table reference</param>
        /// <param name="joinType">Join type</param>
        /// <returns>Stroke</returns>
        public static string JoinedAs(this object entity, Join joinType)
        {
            return "what?";
        }

        /// <summary>
        /// Forces table to be taken itself, without declaration of all possible related joins
        /// </summary>
        /// <param name="entity">Entity table reference</param>
        /// <returns>Stroke</returns>
        public static string NoExpand(this object entity)
        {
            return "what?";
        }
        
        /// <summary>
        /// Forces table to be taken as itself, without alias
        /// </summary>
        /// <param name="entity">Entity table reference</param>
        /// <returns>Stroke</returns>
        public static string NoAlias(this object entity)
        {
            return "what?";
        }

        /// <summary>
        /// Changes nested aggregate join type
        /// </summary>
        /// <param name="type">Join type</param>
        /// <param name="entity">Nested aggregate (e.g. o.User)</param>
        /// <returns>Stroke</returns>
        public static string Overjoin(Join type, object entity)
        {
            return "what?";
        }
    }
}