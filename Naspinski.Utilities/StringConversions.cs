using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naspinski.Utilities
{
    public static class StringConversions
    {
        public static T ToEnum<T>(this string s)
        {
            return (T)Enum.Parse(typeof(T), s);
        }

        public static Nullable<T> ToNullable<T>(this string s, TryParseDelegate<T> tryParse) where T : struct
        {
            T? tester;
            if (TryParseNullable<T>(s, out tester, tryParse)) return tester;
            else return null;
        }

        public delegate bool TryParseDelegate<T>(string s, out T result);
        public static bool TryParseNullable<T>(string s, out Nullable<T> result, TryParseDelegate<T> tryParse) where T : struct
        {
            if (string.IsNullOrEmpty(s))
            {
                result = null;
                return true;
            }
            else
            {
                T temp;
                bool success = tryParse(s, out temp);
                result = temp;
                return success;
            }
        }
    }
}
