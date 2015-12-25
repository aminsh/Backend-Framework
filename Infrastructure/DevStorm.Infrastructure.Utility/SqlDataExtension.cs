using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevStorm.Infrastructure.Utility
{
    public static class SqlDataExtension
    {
        public static IEnumerable<T> ToList<T>(this DataTable dataTable) where T : class , new()
        {
            var list = (from DataRow row in dataTable.Rows select row.ToObject<T>()).ToList();
            return list;
        }

        public static T ToObject<T>(this DataRow row) where T : class , new()
        {
            var instance = new T();
            var properties = typeof (T).GetProperties();

            properties.ForEach(prop =>
            {
                var propName = prop.Name;

                prop.SetValue(instance, ChangeType(row[propName]));
            });

            return instance;
        }

        private static object ChangeType(object value)
        {
            if (value == DBNull.Value)
                return null;

            if (value is DateTime)
                return value.As<DateTime>().ToPersian();

            return value;
        }
    }
}
