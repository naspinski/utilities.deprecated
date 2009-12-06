using System;
using System.ComponentModel;

namespace Naspinski.Utilities
{
    public static class StringConversions
    {
        /// <summary>
        /// Converts a string to an enum of Type T
        /// </summary>
        /// <typeparam name="T">enum Type to convert to</typeparam>
        /// <param name="s">string to convert</param>
        /// <returns>enum of Type T</returns>
        public static T ToEnum<T>(this string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }

        /// <summary>
        /// Converts a string to a nullable[T]
        /// </summary>
        /// <typeparam name="T">Type to convert to; this is the non-nullable version of the nullable return that will be returned</typeparam>
        /// <param name="s">string to convert</param>
        /// <returns>Nullable[T]</returns>
        public static Nullable<T> ToNullable<T>(this string s) where T : struct
        {
            T? result = null; 
            if (!string.IsNullOrEmpty(s.Trim())) 
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T?)); 
                result = (T?)converter.ConvertFrom(s); 
            }
            return result;
        }
    }
}
