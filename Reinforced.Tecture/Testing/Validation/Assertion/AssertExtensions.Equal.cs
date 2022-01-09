using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Testing.Validation.Assertion
{
    public static partial class AssertExtensions
    {
        public static void Equal(this IAssert a, string actual, string expected, string comment = "")
        {
            if (actual == null && expected == null) return;
            if (actual != null && expected == null)
                throw new TectureValidationException(comment ??
                                                     $"{nameof(AssertExtensions.Equal)} failed:actual is not null whether expected is");
            if (actual == null && expected != null)
                throw new TectureValidationException(comment ??
                                                     $"{nameof(AssertExtensions.Equal)} failed:actual is null whether expected is not");

            if (string.Compare(actual, expected, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                actual = actual.Replace("\r\n", "\n");
                expected = expected.Replace("\r\n", "\n");
                if (string.Compare(actual, expected, StringComparison.InvariantCultureIgnoreCase) != 0)
                    throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                         $": expected \r\n [{expected}] \r\n but got \r\n [{actual}]");
            }
        }

        public static void Equal(this IAssert a, int actual, int expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }
        
        public static void Equal<T>(this IAssert a, T actual, T expected, string comment = "") where T:struct
        {
            if (!actual.Equals(expected))
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, object actual, object expected, string comment = "")
        {
            if (!object.ReferenceEquals(actual, expected))
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, double actual, double expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, byte actual, byte expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, sbyte actual, sbyte expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, short actual, short expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, ushort actual, ushort expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, uint actual, uint expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, long actual, long expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, ulong actual, ulong expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, float actual, float expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, decimal actual, decimal expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, byte? actual, byte? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, sbyte? actual, sbyte? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, short? actual, short? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, ushort? actual, ushort? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, int? actual, int? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, uint? actual, uint? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, long? actual, long? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, ulong? actual, ulong? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, float? actual, float? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, double? actual, double? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }

        public static void Equal(this IAssert a, decimal? actual, decimal? expected, string comment = "")
        {
            if (actual != expected)
                throw new TectureValidationException((comment ?? $"{nameof(AssertExtensions.Equal)} failed:") +
                                                     $": expected [{expected}] but got [{actual}]");
        }
    }
}