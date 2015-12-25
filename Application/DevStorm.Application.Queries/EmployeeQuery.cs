using System;
using DevStorm.Infrastructure.Core.DataAccess;
using Kendo.DynamicLinq;

namespace DevStorm.Application.Queries
{
    public class EmployeeQuery
    {
        private readonly IReadStorage _readStorage;

        public EmployeeQuery(IReadStorage readStorage)
        {
            _readStorage = readStorage;
        }

        public DataSourceResult All(DataSourceRequest request)
        {
            //return _readStorage.Get<EmployeeListView>(request, "spName");

            return null;
        }

        public object ById(Guid id)
        {
            //return _readStorage.GetById<EmployeeSingleView>(id, "spName");
            return null;
        }
    }
}