using System;
using System.ComponentModel;
using System.Web.Security;
using System.Text;
using System.Linq;

namespace Naspinski.Utilities
{
    public static class StringConversions
    {
        /// <summary>
        /// Generated a random string
        /// </summary>
        /// <param name="s">initialized string</param>
        /// <param name="length">length of finished string</param>
        /// <param name="isOnlyAlphaNumeric">if it is true, there will be no special characters</param>
        /// <param name="minSpecialCharacters">if they are required, you can specify a minimum</param>
        /// <returns>random string</returns>
        public static string ToRandomString(this string s, int length, bool isOnlyAlphaNumeric = true, int minSpecialCharacters = 1)
        {
            if (isOnlyAlphaNumeric) minSpecialCharacters = 0;
            s = Membership.GeneratePassword(length, minSpecialCharacters);
            if (!isOnlyAlphaNumeric) return s;
            

            char[] msSpecialCharacters = "!@#$%^&*()_-+=[{]};:<>|./?".ToCharArray();
            string filler = Membership.GeneratePassword(length, 0);
            int fillerIndex = 0;
            int fillerBuffer = 0;

            while(s.IndexOfAny(msSpecialCharacters) > -1 || s.Length < length)
            {
                s = s.RemoveCharacters(msSpecialCharacters);
                fillerBuffer = s.Length - length;
                if(fillerBuffer + fillerIndex > filler.Length)
                {   // filler would out-of-bounds, get a new one
                    filler = Membership.GeneratePassword(length, 0);
                    fillerIndex = 0;
                }
                s += filler.Substring(fillerIndex, fillerBuffer);
            }
            return s;
        }

        /// <summary>
        /// Removes all characters from the string s
        /// </summary>
        /// <param name="s">string to remove characters from</param>
        /// <param name="characters">array of characters to remove</param>
        /// <returns>string with characters removed</returns>
        public static string RemoveCharacters(this string s, char[] characters)
        {
            return s.ToCharArray().Where(c => !characters.Contains(c)).ToString();
        }

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
            if (s != null && !string.IsNullOrEmpty(s.Trim())) 
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T?)); 
                return (T?)converter.ConvertFrom(s); 
            }
            return null;
        }

        /// <summary>
        /// Converts a string to Type T
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="s">string to convert</param>
        /// <returns>Type T Conversion of s if possible</returns>
        public static T To<T>(this string s) where T : struct
        {
            if (string.IsNullOrEmpty(s)) throw new InvalidCastException("Cannot cast empty/null object to Type " + typeof(T).Name);
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T?));
            return (T)converter.ConvertFrom(s);
        }
    }
}
