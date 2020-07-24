using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    /// <summary>
    /// Visitor-like class that allows to override particular query expressions translation
    /// </summary>
    public abstract class QueryFiller
    {
        /// <summary>
        /// String containing query with cut off all the parameters replaced by empty strings.
        /// So if original query was x=>"SELECT {x.Name} FROM {x}", the query structure will be
        /// "SELECT   FROM   "
        /// </summary>
        protected string QueryStructureText { get; set; }

        /// <summary>
        /// Pairs of index -> SqlQueryExpression where key is index in <see cref="QueryStructureText"/> where
        /// expression must be embedded at and value is expression itself
        /// </summary>
        protected Dictionary<int, SqlQueryExpression> Expressions { get; private set; }

        /// <summary>
        /// Access to the Mapper instance
        /// </summary>
        public IMapper Mapper { get; internal set; }

        /// <summary>
        /// Determines whether search string appears immediately before the index. Ignores case.
        /// </summary>
        /// <param name="search">Search string</param>
        /// <param name="index">Index to look up for search string before</param>
        /// <returns>True when search string appears before the index, false otherwise</returns>
        protected bool Precends(string search, int index)
        {
            var master = QueryStructureText;
            index--;
            while (index >= 0 && Char.IsWhiteSpace(master, index)) index--;
            if (index - search.Length + 1 < 0) return false;
            index = index - search.Length + 1;
            return master.IndexOf(search, index, StringComparison.InvariantCultureIgnoreCase) == index;
        }

    }
}
