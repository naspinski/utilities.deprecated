using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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
            if (string.IsNullOrEmpty(s)) return null;
            T? nullable = null;
            object[] parameters = new object[] { s, nullable };
            MethodInfo tryParse = typeof(T).GetMethods()
                .Where(x => x.Name.Equals("TryParse") && 
                    x.GetParameters().Count() == 2)
                .SingleOrDefault();
            if(tryParse == null) throw new NotSupportedException(typeof(T).ToString() + " does not have the proper TryParse method for this extension");
            tryParse.Invoke(null, parameters);
            nullable = (T?)parameters[1];
            return nullable;
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
