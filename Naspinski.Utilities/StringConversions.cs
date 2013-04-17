using System;
using System.ComponentModel;
using System.Web.Security;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Naspinski.Utilities
{
    public class Strings
    {
        /// <summary>
        /// Generated a random string
        /// </summary>
        /// <param name="s">initialized string</param>
        /// <param name="length">length of finished string</param>
        /// <param name="isOnlyAlphaNumeric">if it is true, there will be no special characters</param>
        /// <param name="minSpecialCharacters">if they are required, you can specify a minimum</param>
        /// <returns>random string</returns>
        public static string Random(int length, int minSpecialCharacters = 0)
        {
            bool isOnlyAlphaNumeric = minSpecialCharacters == 0;
            string s = Membership.GeneratePassword(length, minSpecialCharacters);
            if (!isOnlyAlphaNumeric) return s;

            char[] msSpecialCharacters = "!@#$%^&*()_-+=[{]};:<>|./?".ToCharArray();
            string filler = Membership.GeneratePassword(length, 0);
            int fillerIndex = 0, fillerBuffer;

            while (s.IndexOfAny(msSpecialCharacters) > -1 || s.Length < length)
            {
                s = s.RemoveCharacters(msSpecialCharacters);
                fillerBuffer = length - s.Length;
                if ((fillerBuffer + fillerIndex) > filler.Length)
                {   // filler would out-of-bounds, get a new one
                    filler = Membership.GeneratePassword(length, 0);
                    fillerIndex = 0;
                }
                s += filler.Substring(fillerIndex, fillerBuffer);
                fillerIndex += fillerBuffer;
            }
            return s;
        }   
    }

    public static class StringConversions
    {
        /// <summary>
        /// Removes all characters from the string s
        /// </summary>
        /// <param name="s">string to remove characters from</param>
        /// <param name="characters">array of characters to remove</param>
        /// <returns>string with characters removed</returns>
        public static string RemoveCharacters(this string s, char[] characters)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return new string(s.ToCharArray().Where(c => !characters.Contains(c)).ToArray());
        }

        /// <summary>
        /// Converts a string to an enum of Type T
        /// </summary>
        /// <typeparam name="T">enum Type to convert to</typeparam>
        /// <param name="s">string to convert</param>
        /// <returns>enum of Type T</returns>
        public static T ToEnum<T>(this string s)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), s);
            }
            catch //tries to capitalize the first letter
            {
                CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
                TextInfo textInfo = cultureInfo.TextInfo;
                s = textInfo.ToTitleCase(s.ToLower());
                return (T)Enum.Parse(typeof(T), s);
            }
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

        /// <summary>
        /// Splits a CamelCase word into a human readable - deals effectively with most abbreviations
        /// </summary>
        /// <param name="str">CamelCase String</param>
        /// <returns>'Humanized' string</returns>
        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }
    }
}
