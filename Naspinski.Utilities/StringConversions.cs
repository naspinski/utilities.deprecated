using System;
using System.ComponentModel;

namespace Naspinski.Utilities
{
    public static class StringConversions
    {
        public static T ToEnum<T>(this string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }

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
