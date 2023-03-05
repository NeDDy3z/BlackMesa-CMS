using System.Data.SqlClient;

namespace DB_Projekt;

public class User
{
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public int Id { get; set; }
    
    
    
    /// <summary>
    /// Insert new user into DB
    /// </summary>
    public static void RegisterUser(string username, string password)
    {
        using (var conn = DatabaseManager.GetSqlConnection())
        {
            var query = "INSERT INTO usr (username, pass) VALUES ('" + username + "','" +
                        PasswordEncryption.Enrypt(password) + "');";
            var command = new SqlCommand(query, conn);

            conn.Open();
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Check if username and password match in DB
    /// </summary>
    /// <returns>
    /// true / false
    /// </returns>
    public static bool LoginUser(string username, string password)
    {
        var good = false;
        using (var conn = DatabaseManager.GetSqlConnection())
        {
            try
            {
                var query = "SELECT * FROM usr WHERE username = '" + username + "'";
                var command = new SqlCommand(query, conn);

                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    if ((string)reader["username"] == username &&
                        PasswordEncryption.Decrypt((string)reader["pass"]) == password)
                    {
                        Menu.currentUser = new User(username, password);
                        Menu.currentUser.Id = (int)reader["id_us"];
                        good = true;
                    }
                }
            }
            catch (Exception e)
            {
                good = false;
            }
        }

        return good;
    }

    /// <summary>
    /// Check if password matches
    /// </summary>
    /// Check password (i know i have the similar thing in LoginUser(), however, IDC!)
    public bool CheckPass(string passIn)
    {
        var good = false;
        using (var conn = DatabaseManager.GetSqlConnection())
        {
            try
            {
                var query = "SELECT * FROM usr WHERE username = '" + Username + "'";
                var command = new SqlCommand(query, conn);

                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    if ((string)reader["pass"] == PasswordEncryption.Enrypt(passIn)) good = true;
                }
            }
            catch (Exception e)
            {
            }
        }

        return good;
    }

    /// <summary>
    /// Remove user from DB
    /// </summary>
    public void DeleteUser()
    {
        using (var conn = DatabaseManager.GetSqlConnection())
        {
            try
            {
                var query = "DELETE FROM usr WHERE username = '" + Username + "'";
                var command = new SqlCommand(query, conn);

                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
        }
    }
}