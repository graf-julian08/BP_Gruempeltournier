

using Microsoft.Data.SqlClient;

namespace BP_Gruempeltournier.Data
{
    public static class Db
    {
        public static string ConnectionString { get; set; } = string.Empty;

        public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);
    }
}
