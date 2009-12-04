using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace Naspinski.Utilities
{
    public static class LinqToSql
    {
        public static PropertyInfo GetPrimaryKey<T>()
        {
            PropertyInfo[] infos = typeof(T).GetProperties();
            PropertyInfo PKProperty = null;
            foreach (PropertyInfo info in infos)
            {
                var column = info.GetCustomAttributes(false)
                    .Where(x => x.GetType() == typeof(ColumnAttribute))
                    .SingleOrDefault(x => ((ColumnAttribute)x).IsPrimaryKey);
                if (column != null)
                {
                    PKProperty = info;
                    break;
                }
            }
            if (PKProperty == null) 
                throw new NotSupportedException(typeof(T).ToString() + " has no Primary Key");
            return PKProperty;
        }

        public static T Get<T>(this DataContext dataContext, object primaryKey) where T : class, INotifyPropertyChanged
        {
            var table = dataContext.GetTable(typeof(T)).Cast<T>();
            return table.Get<T>(primaryKey);
        }

        public static T Get<T>(this IQueryable<T> table, object primaryKey) where T : class, INotifyPropertyChanged
        {
            return table.Where(GetPrimaryKey<T>().Name + "==@0", primaryKey).First();
        }
    }
}
