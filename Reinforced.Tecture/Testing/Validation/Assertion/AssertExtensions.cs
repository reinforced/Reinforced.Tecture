using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Testing.Validation.Assertion
{
    public static partial class AssertExtensions
    {
        public static void True(this IAssert a,bool actual, string comment = "")
        {
            if (!actual)
                throw new TectureValidationException(comment ?? $"{nameof(AssertExtensions.True)} failed");
        }
        
        public static void False(this IAssert a,bool actual, string comment = "")
        {
            if (actual)
                throw new TectureValidationException(comment ?? $"{nameof(AssertExtensions.False)} failed");
        }
        
        
        public static void Null<T>(this IAssert a,T actual, string comment = "") where T:class
        {
            if (actual!=null)
                throw new TectureValidationException(comment ?? $"{nameof(AssertExtensions.Null)} failed");
        }
        
        public static void Null<T>(this IAssert a,T? actual, string comment = "") where T:struct
        {
            if (actual!=null)
                throw new TectureValidationException(comment ?? $"{nameof(AssertExtensions.Null)} failed");
        }
        
        public static void NotNull<T>(this IAssert a,T actual, string comment = "") where T:class
        {
            if (actual==null)
                throw new TectureValidationException(comment ?? $"{nameof(AssertExtensions.NotNull)} failed");
        }
        
        
       

        public static void Collection<T>(this IAssert a, IEnumerable<T> source, string explanation, params Action<T>[] validators)
        {
            var curValidatorIndex = 0;

            foreach (var item in source)
            {
                if (curValidatorIndex + 1 > validators.Length)
                    throw new TectureValidationException($"{explanation}{(string.IsNullOrEmpty(explanation)?"":": ")} Not all elements present in collection or order is different.");

                try
                {
                    validators[curValidatorIndex](item);
                }
                catch (Exception ex)
                {
                    throw new TectureValidationException($"{explanation}{(string.IsNullOrEmpty(explanation)?"":": ")} Validation for item #{curValidatorIndex} failed. See inner exception for details",ex);
                }

                curValidatorIndex++;
            }
            
            if (curValidatorIndex!=validators.Length)
                throw new TectureValidationException($"{explanation}{(string.IsNullOrEmpty(explanation)?"":": ")} Collection contains less than {validators.Length} items");
        }
    }
}