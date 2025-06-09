using Microsoft.Data.SqlClient;

namespace OnlinePaymentsApp.Repository
{
    public static class ConnectionFactory
    {
        private static string connectionString;

        public static void Initialize(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public static async Task<SqlConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
