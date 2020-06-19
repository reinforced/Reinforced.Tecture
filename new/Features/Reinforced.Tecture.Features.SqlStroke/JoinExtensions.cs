using System;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public enum Join
    {
        Inner,
        Left,
        Right,
        Outer,
        Cross,
        Default
    }
    internal static class JoinExtensions
    {
        public static string ToSql(this Join j)
        {
            switch (j)
            {
                case Join.Cross: return "CROSS";
                case Join.Inner: return "INNER";
                case Join.Left: return "LEFT";
                case Join.Right: return "RIGHT";
                case Join.Outer: return "OUTER";
                case Join.Default: return string.Empty;
            }

            throw new Exception("Unknown join type: " + j);
        }
    }
}