using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.DynamicLinq;

namespace Core.Query
{
    public interface IReadStorage
    {
        DataSourceResult Get<T>(DataSourceRequest request, string storedProcedureName)
            where T : class, new();

        T GetById<T>(object id, string storedProcedureName)
            where T : class, new();

        IEnumerable<T> Get<T>(Dictionary<string, object> param, string storedProcedureName)
            where T : class, new();
    }
}