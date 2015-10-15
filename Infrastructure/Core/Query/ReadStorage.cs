using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Kendo.DynamicLinq;
using Utility;

namespace Core.Query
{
    public class ReadStorage : IReadStorage
    {
        public DataSourceResult Get<T>(DataSourceRequest request, string storedProcedureName)
            where T : class, new()
        {
            return request.ToDataSourceResult<T>(storedProcedureName);
        }

        public T GetById<T>(object id, string storedProcedureName) where T : class, new()
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

        public IEnumerable<T> Get<T>(Dictionary<string, object> param, string storedProcedureName)
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