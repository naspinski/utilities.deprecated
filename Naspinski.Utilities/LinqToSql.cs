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
    public static class LinqToSql
    { 
        /// <summary>
        /// Universal Get accessor for any Linq-to-SQL DataContext; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">Type of the object you wish to return, must be a table in the DataContext</typeparam>
        /// <param name="dataContext">DataContext that holds the table you wish to query</param>
        /// <param name="primaryKey">Primary Key of the object</param>
        /// <returns>T</returns>
        public static T Get<T>(this DataContext dataContext, object primaryKey) where T : class, INotifyPropertyChanged
        {
            List<string> where = new List<string>();
            var a = GetPrimaryKey<T>();

            if (a.GetType().Name == "TableCompositeKey")
            {
                throw new Exception("Table " + typeof(T).ToString() + " has only Multiple key, excepecting only 1 key");
            }

            if (((TableKey)a).PropertyInfo.PropertyType != primaryKey.GetType())
                throw new ArgumentException("Primary Key of Table and primaryKey argument are not of the same Type; Primary Key of Table is of Type: " + ((TableKey)a).PropertyInfo.ToString() + ", primaryKey argument supplied is of Type: " + primaryKey.GetType().ToString());
            return dataContext.GetTable(typeof(T)).Cast<T>().Where(((TableKey)a).PropertyInfo.Name + ".Equals(@0)", primaryKey).FirstOrDefault();
        }


        /// <summary>
        /// Universal Get accessor for any Linq-to-SQL DataContext; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">Type of the object you wish to return, must be a table in the DataContext</typeparam>
        /// <param name="dataContext">DataContext that holds the table you wish to query</param>
        /// <param name="primaryKey">Dicitionary with a pair of names from primary key and values for query</param>
        /// <returns>T</returns>
        /// <returns>T</returns>
        public static T Get<T>(this DataContext dataContext, Dictionary<string, object> primaryKey) where T : class, INotifyPropertyChanged
        {
            List<string> where = new List<string>();
            var a = GetPrimaryKey<T>();
            if (a.GetType().Name == "TableKey")
            {
                throw new Exception("Table " + typeof(T).ToString() + " has only 1 key, excepecting multiple composite Keys");
            }
            int i = 0;
            foreach (TableKey tbk in ((TableCompositeKey)a).Keys)
            {
                if (tbk.PropertyInfo.PropertyType != primaryKey[tbk.PropertyInfo.Name].GetType())
                {
                    throw new ArgumentException("Primary Key of Table and primaryKey argument are not of the same Type; Primary Key name" + tbk.PropertyInfo.Name + " of Table is of Type: " + tbk.PropertyInfo.PropertyType.ToString() + ", primaryKey argument supplied is of Type: " + primaryKey[tbk.PropertyInfo.Name].GetType().ToString());
                }
                where.Add(tbk.PropertyInfo.Name + ".Equals(@" + i + ")");
                i++;
            }
            return dataContext.GetTable(typeof(T)).Cast<T>().Where(string.Join(" AND ", where.ToArray()), primaryKey.Select(p => p.Value).ToArray()).FirstOrDefault();
        }


        /// <summary>
        /// Gets the primary key of a Linq-to-SQL table; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">Type that you wish to find the primary key of</typeparam>
        /// <returns>List of PropertyInfo of the Primary Key of the supplied Type</returns>
        public static IPrimaryKey GetPrimaryKey<T>() where T : class, INotifyPropertyChanged
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

            if (PKProperty.Count == 1)
            {
                TableKey t = new TableKey();
                t.PropertyInfo = PKProperty[0];
                return t;
            }
            else
            {
                TableCompositeKey mt = new TableCompositeKey();
                mt.AddKeys(PKProperty);
                return mt;
            }

        }

        /// <summary>
        /// To see if an IPrameryKey is a composite key or not
        /// </summary>
        /// <param name="key">IPrimary key to explore</param>
        /// <returns>True if it is a Composite Key, false if not</returns>
        public static bool IsCompositeKey(this IPrimaryKey key)
        {
            try
            {
                TableKey temp = (TableKey)key;
                return false;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Returns a single Primary Key for type T, erroring if it is not a single key
        /// </summary>
        /// <typeparam name="T">Type you are searching for a single primary key on</typeparam>
        /// <returns>TableKey with the PropertyInfo of the primary key</returns>
        public static TableKey GetSinglePrimaryKey<T>() where T : class, INotifyPropertyChanged
        {
            IPrimaryKey key = GetPrimaryKey<T>();
            if (!key.IsCompositeKey()) return (TableKey)key;
            throw new InvalidOperationException(typeof(T).GetType().Name + " does not have a single Primary Key");
        }

        /// <summary>
        /// Returns a composite key for type T, erroring if it is not a composite key
        /// </summary>
        /// <typeparam name="T">Type you are searching for a composite key on</typeparam>
        /// <returns>TableCompositeKey with a PropertyInfo[] array Keys</returns>
        public static TableCompositeKey GetCompositeKey<T>() where T : class, INotifyPropertyChanged
        {
            IPrimaryKey key = GetPrimaryKey<T>();
            if (key.IsCompositeKey()) return (TableCompositeKey)key;
            throw new InvalidOperationException(typeof(T).GetType().Name + " does not have a composite Key");
        }
    }
}
