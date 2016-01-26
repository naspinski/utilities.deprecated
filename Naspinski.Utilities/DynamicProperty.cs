using System;
using System.Reflection;

namespace Naspinski.Utilities
{
    public static class DynamicProperty
    {
        /// <summary>
        /// Changes the property 'propertyName' of object 'o' to 'newValue' if possible
        /// </summary>
        /// <param name="o">Object you wish to change the property value of</param>
        /// <param name="propertyName">property name to change (case sensitive)</param>
        /// <param name="newValue">new value of the property</param>
        public static void SetPropertyValue(this object o, string propertyName, object newValue)
        {
            PropertyInfo pi;
            pi = o.GetType().GetProperty(propertyName);
            if (pi == null)
                throw new Exception("No Property [" + propertyName + "] in Object [" + o.GetType().ToString() + "]");
            if (!pi.CanWrite)
                throw new Exception("Property [" + propertyName + "] in Object [" + o.GetType().ToString() + "] does not allow writes");
            pi.SetValue(o, newValue, null);
        }

        /// <summary>
        /// Get typed  property based on string name and Object type
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="o">object to grab property from</param>
        /// <param name="propertyName">name of property</param>
        /// <returns>property value</returns>
        public static T GetPropertyValue<T>(this object o, string propertyName)
        {
            return (T) o.GetPropertyValue(propertyName);
        }

        /// <summary>
        /// Get property based on string name
        /// </summary>
        /// <param name="o">object to grab property from</param>
        /// <param name="propertyName">name of property</param>
        /// <returns>property value</returns>
        public static object GetPropertyValue(this object o, string propertyName)
        {
            return o.GetType().GetProperty(propertyName).GetValue(o, null);
        }
    }
}
