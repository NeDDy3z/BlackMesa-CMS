using System.Data.SqlClient;
using System.Text;

namespace DB_Projekt;

public class Message
{
    /// <summary>
    /// Soft wraps text so it looks good
    /// </summary>
    private static void SoftWrapPrint(string text, string clr)
    {
        var maxLineLength = 61;
        var words = text.Split(' ');
        var sb = new StringBuilder();
        var lineLength = 0;
        foreach (var word in words)
        {
            if (lineLength + word.Length + 1 > maxLineLength)
            {
                Console.WriteLine(sb.ToString());
                sb.Clear();
                lineLength = 0;
            }

            sb.Append(word + " ");
            lineLength += word.Length + 1;
        }

        Print.WriteLine(sb.ToString(), clr);
    }

    /// <summary>
    /// Insert new message into DB
    /// </summary>
    public static void SendMessage(string recipient, string title, string content)
    {
        int msgId;
        int recId;
        using (var conn = DatabaseManager.GetSqlConnection())
        {
            //Insert msg
            var query = "INSERT INTO msg (title, contents, timesent) VALUES ('" +
                        title + "','" +
                        content +
                        "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
            var command = new SqlCommand(query, conn);

            conn.Open();
            command.ExecuteNonQuery();


            //Select id of msg
            query = "SELECT id_ms FROM msg WHERE title = '" + title + "'";
            command = new SqlCommand(query, conn);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                msgId = (int)reader[0];
            }
            
            if (recipient.Contains(' '))
            {
                string[] recipientList = recipient.Split(' ');
            
                foreach (var rec in recipientList)
                {
                    query = "SELECT id_us FROM usr WHERE username = '" + rec + "'";
                    command = new SqlCommand(query, conn);

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        recId = (int)reader[0];
                    }
                
                    query = "INSERT INTO link (recipient, sender, msg, rec_del, sen_del) " +
                            "VALUES (" + recId + "," + Menu.currentUser.Id + "," + msgId + ", 0, 0);";
                    command = new SqlCommand(query, conn);

                    command.ExecuteNonQuery();
                }
            }
            else
            {
                //Select id of usr (for link table)
                query = "SELECT id_us FROM usr WHERE username = '" + recipient + "'";
                command = new SqlCommand(query, conn);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    recId = (int)reader[0];
                }
                
                //Insert into link
                query = "INSERT INTO link (recipient, sender, msg, rec_del, sen_del) " +
                        "VALUES (" + recId + "," + Menu.currentUser.Id + "," + msgId + ", 0, 0);";
                command = new SqlCommand(query, conn);

                command.ExecuteNonQuery();
            }
            
            
            /*
            
            */
        }
    }

    /// <summary>
    /// Print out all recieved messages
    /// </summary>
    public static void RecievedMsgs()
    {
        using (var conn = DatabaseManager.GetSqlConnection())
        {
            var query = "SELECT s.username, msg.title, msg.contents, msg.timesent " +
                        "FROM link " +
                        "INNER JOIN usr s ON link.sender = s.id_us " +
                        "INNER JOIN msg ON link.msg = msg.id_ms " +
                        "WHERE recipient = '" + Menu.currentUser.Id + "' AND rec_del = 0";
            var command = new SqlCommand(query, conn);

            conn.Open();
            using (var reader = command.ExecuteReader())
            {
                Print.WriteLine("|-----------------------------------------------------------|",
                    "Gray");
                while (reader.Read())
                {
                    Print.Write("From: ", "Yellow");
                    Print.Write((string)reader[0] + "\n", "");
                    Print.Write("Date: ", "Yellow");
                    Print.Write(reader[3] + "\n", "");
                    Print.Write("Title: ", "Yellow");
                    Print.Write((string)reader[1] + "\n", "");
                    Print.Write("Message: \n", "Yellow");
                    SoftWrapPrint((string)reader[2], "");
                    Print.WriteLine("|-----------------------------------------------------------|",
                        "Gray");
                }
            }
        }
    }

    /// <summary>
    /// Print out all sent messages
    /// </summary>
    public static void SentMsgs() //Prints out sent msgs
    {
        using (var conn = DatabaseManager.GetSqlConnection())
        {
            var query = "SELECT r.username, msg.title, msg.contents, msg.timesent " +
                        "FROM link " +
                        "INNER JOIN usr r ON link.recipient = r.id_us " +
                        "INNER JOIN msg ON link.msg = msg.id_ms " +
                        "WHERE sender = '" + Menu.currentUser.Id + "' AND sen_del = 0";
            var command = new SqlCommand(query, conn);

            conn.Open();
            using (var reader = command.ExecuteReader())
            {
                Print.WriteLine("|-----------------------------------------------------------|",
                    "Gray");
                while (reader.Read())
                {
                    Print.Write("To: ", "Yellow");
                    Print.Write((string)reader[0] + "\n", "");
                    Print.Write("Date: ", "Yellow");
                    Print.Write(reader[3] + "\n", "");
                    Print.Write("Title: ", "Yellow");
                    Print.Write((string)reader[1] + "\n", "");
                    Print.Write("Message: \n", "Yellow");
                    SoftWrapPrint((string)reader[2], "");
                    Print.WriteLine("|-----------------------------------------------------------|",
                        "Gray");
                }
            }
        }
    }

    public static void DeleteMsg(string title, bool rec)
    {
        using (var conn = DatabaseManager.GetSqlConnection())
        {
            try
            {
                var query = "";
                if (rec)
                {
                    query = "UPDATE link " +
                            "SET rec_del = 1 " +
                            "FROM link " +
                            "INNER JOIN usr u ON link.recipient = u.id_us " +
                            "INNER JOIN msg m ON link.msg = id.id_ms " +
                            "WHERE u.username = '" + Menu.currentUser + "' AND m.title = '" +
                            title + "'";
                }
                else
                {
                    query = "UPDATE link " +
                            "SET sen_del = 1 " +
                            "FROM link " +
                            "INNER JOIN usr u ON link.sender = u.id_us " +
                            "INNER JOIN msg m ON link.msg = m.id_ms " +
                            "WHERE u.username = '" + Menu.currentUser + "' AND m.title = '" +
                            title + "'";
                }

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