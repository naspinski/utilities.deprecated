using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naspinski.Utilities
{
    public static class EnumUtilities
    {
        /// <summary>
        /// Gets a collection of enum values based on the enum provided
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        /// <returns>IEnumerable of all enums type T</returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
