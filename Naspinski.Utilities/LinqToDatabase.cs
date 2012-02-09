using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Collections.Generic;

namespace Naspinski.Utilities
{
    public static class LinqToDatabase
    {
        /// <summary>
        /// Universal Get accessor for any Linq-to-SQL DataContext; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">Type of the object you wish to return, must be a table in the DataContext</typeparam>
        /// <param name="dataContext">DataContext that holds the table you wish to query</param>
        /// <param name="primaryKey">Dicitionary with a pair of names from primary key and values for query</param>
        /// <returns>T</returns>
        public static T Get<T>(this DataContext dataContext, Dictionary<string, string> primaryKey) where T : class, INotifyPropertyChanged
        {
                List<string> where = new List<string>();
                foreach (PropertyInfo info in GetPrimaryKeys<T>().ToArray())
                    where.Add(info.Name + ".Equals(" + primaryKey[info.Name] + ")");
                return dataContext.GetTable(typeof(T)).Cast<T>().Where(string.Join(" AND ", where.ToArray())).FirstOrDefault();
        }

        /// <summary>
        /// Gets the primary key of a Linq-to-SQL table; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">Type that you wish to find the primary key of</typeparam>
        /// <returns>List of PropertyInfo of the Primary Key of the supplied Type</returns>
        public static List<PropertyInfo> GetPrimaryKeys<T>() where T : class, INotifyPropertyChanged
        {
            PropertyInfo[] infos = typeof(T).GetProperties();
            List<PropertyInfo> PKProperty = new List<PropertyInfo>();
            foreach (PropertyInfo info in infos)
            {
                var column = info.GetCustomAttributes(false)
                    .Where(x => x.GetType() == typeof(ColumnAttribute))
                    .FirstOrDefault(x => ((ColumnAttribute)x).IsPrimaryKey && ((ColumnAttribute)x).DbType.Contains("NOT NULL"));
                if (column != null)
                {
                    PKProperty.Add(info);
                }
            }
            if (PKProperty == null)
                throw new NotSupportedException(typeof(T).ToString() + " has no Primary Key");
            return PKProperty;
        }

        /// <summary>
        /// Gets the primary key of a Linq-to-SQL table; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">Type that you wish to find the primary key of</typeparam>
        /// <returns>PropertyInfo of the Primary Key of the supplied Type</returns>
        public static PropertyInfo GetPrimaryKey<T>() where T : class, INotifyPropertyChanged
        {
            return GetPrimaryKeys<T>().First();
        }
    }
}
