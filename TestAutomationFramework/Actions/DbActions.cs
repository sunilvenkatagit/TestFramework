using System;
using System.Data;
using System.Data.SqlClient;

namespace TestAutomationFramework.Actions
{
    public class DbActions
    {
        /// <summary>
        /// Executes the sqlQuery with the provided open sqlConnection and all the queried rows will be loaded into a DataTable. <br />
        /// <b>sqlConnection: </b>Provide a opened SqlConnection <br />
        /// <b>sqlQuery: </b>Provide a SQL query <br />
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteQuery(SqlConnection sqlConnection, string sqlQuery)
        {
            var dataTable = new DataTable();

            try
            {
                using var connection = sqlConnection;
                using var sqlCommand = new SqlCommand(sqlQuery, connection);
                using var sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                    dataTable.Load(sqlDataReader);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to Get Data. Please check the Sql Connection or sqlQuery.\nErrorLogging:\n{ex.Message}");
            }

            return dataTable;
        }

        /// <summary>
        /// Gets the total number of records in a given table. <br />
        /// <b>sqlConnection: </b>Provide a opened SqlConnection <br />
        /// <b>tableName: </b>Provide a tableName <br />
        /// The argument <paramref name="sqlQuery"/> is optional.
        /// </summary>
        /// <returns>Record Count</returns>
        public static int GetRecordCount(SqlConnection sqlConnection, string tableName = "", string sqlQuery = null)
        {
            int recordCount = 0;
            string cmdText = $"SELECT COUNT(*) FROM [{sqlConnection.Database}].[dbo].[{tableName}]";

            if (!string.IsNullOrEmpty(sqlQuery)) cmdText = sqlQuery;

            try
            {
                using (var connection = sqlConnection)
                using (var sqlCommand = new SqlCommand(cmdText, connection))
                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.Read())
                        recordCount = (int)sqlDataReader[0];
                }
                return recordCount;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to Get Record Count for the table: [{sqlConnection.Database}].[dbo].[{tableName}]\nErrorLogging:\n{ex.Message}");
            }
        }
    }
}
