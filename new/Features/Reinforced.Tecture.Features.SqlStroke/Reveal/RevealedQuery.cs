using System;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal
{

    public class RevealedQuery
    {
        public string CommandText { get; }
        public object[] CommandParameters { get; }
        public Type[] UsedTypes { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal RevealedQuery(string commandText, object[] commandParameters, Type[] usedTypes)
        {
            CommandText = commandText;
            CommandParameters = commandParameters;
            UsedTypes = usedTypes;
        }
    }
}
