using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.DynamicLinq;
using Utility;

namespace Core.Query
{
    public static class ReadStorage
    {
        public static DataSourceResult Get<T>(DataSourceRequest request, string storedProcedureName)
            where T : class, new()
        {
            return request.ToDataSourceResult<T>(storedProcedureName);
        }

        public static T GetById<T>(object id, string storedProcedureName) where T : class, new()
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@intId", id),
            };

            var ds = new SqlHelper().RunProcedure(
                storedProcedureName,
                parameters);

            return ds.Tables[0].Rows[0].ToObject<T>();
        }

        public static IEnumerable<T> Get<T>(Dictionary<string, object> param, string storedProcedureName)
            where T : class, new()
        {
            var parameters = param.Select(p => new SqlParameter(p.Key, p.Value)).ToList();

            var ds = new SqlHelper().RunProcedure(
                storedProcedureName,
                parameters);

            return ds.Tables[0].ToList<T>();
        }
    }
}