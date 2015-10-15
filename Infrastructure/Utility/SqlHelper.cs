using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Utility
{
    public class SqlHelper
    {
        private readonly SqlConnection _connection;

        public SqlHelper()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ToString();
            _connection = new SqlConnection(connectionString);
        }

        public DataSet RunProcedure(string storedProcedureName, IEnumerable<SqlParameter> parameters)
        {
            _connection.Open();

            var command = new SqlCommand(storedProcedureName, _connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            parameters.ForEach(param => command.Parameters.Add(param));

            var da = new SqlDataAdapter {SelectCommand = command};
            var dataSet = new DataSet();
            da.Fill(dataSet);
            _connection.Close();

            return dataSet;
        }
    }
}