using System.Configuration;
using DB_Projekt;

/// <summary>
/// Reads data from App.config
/// </summary>
/// <returns>
/// Value of parameters
/// </returns>
static string ReadSettings(string key)
{
    var appSettings = ConfigurationManager.AppSettings;
    var result = appSettings[key] ?? "Not found";
    return result;
}

/// <summary>
/// Set DB connection details
/// </summary>
DatabaseManager.setConectionDetails(
    ReadSettings("userId"),
    ReadSettings("password"),
    ReadSettings("initialCat"),
    ReadSettings("dataSrc"),
    Convert.ToBoolean(ReadSettings("integratedSec")),
    Convert.ToInt32(ReadSettings("connTimeout")));


var menu = new Menu();
menu.MainMenu();

for (var i = 0; i < 3; i++)
{
    Console.Clear();
    Print.WriteLine("\nThank you for using Black Mesa CMS.", "Yellow");
    Print.Write("Shutting down", "Red");
    for (var j = 0; j < 3; j++)
    {
        Thread.Sleep(280);
        Print.Write(".", "Red");
        Thread.Sleep(280);
    }

    Console.Clear();
}