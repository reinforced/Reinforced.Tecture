using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal
{
    partial class StrokeProcessor
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
}
