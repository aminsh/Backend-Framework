using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Core.DataAccess;
using DataAccess;
using Domain;
using Kendo.DynamicLinq;
using ReadModels;
using Utility;

namespace Queries
{
    public class EmployeeQuery
    {
        public DataSourceResult All(DataSourceRequest request)
        {
          return null;
        }

        public EmployeeSingleView ById(int id)
        {
           return null;
        }
    }
}
