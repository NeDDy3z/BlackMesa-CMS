using System.Data.SqlClient;

namespace DB_Projekt;

public class DatabaseManager
{
    private static SqlConnection sqlconn;
    private static SqlConnectionStringBuilder csb;

    /// <summary>
    /// Set DB connection details
    /// </summary>
    public static void setConectionDetails(string userid, string password, string initialCat,
        string dataSrc, bool intergratedSec, int connTimeout)
    {
        csb = new SqlConnectionStringBuilder();

        csb.UserID = userid;
        csb.Password = password;
        csb.InitialCatalog = initialCat;
        csb.DataSource = dataSrc;
        csb.IntegratedSecurity = intergratedSec;
        csb.ConnectTimeout = connTimeout;
    }

    /// <summary>
    /// Takes sql connectionstring and creates new sql connection with it
    /// </summary>
    /// <reutrns>
    /// SQL Connection
    /// </returns>
    public static SqlConnection GetSqlConnection()
    {
        return new SqlConnection(csb.ConnectionString);
    }

    /// <summary>
    /// Check if value exists in DB
    /// </summary>
    /// <reutrns>
    /// true / false
    /// </returns>
    public static bool Exists(string table, string column, string value)
    {
        var exists = false;
        using (var conn = GetSqlConnection())
        {
            var query = "SELECT " + column + " FROM " + table;
            var command = new SqlCommand(query, conn);

            conn.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    if ((string)reader[0] == value)
                        exists = true;
            }
        }

        return exists;
    }
}