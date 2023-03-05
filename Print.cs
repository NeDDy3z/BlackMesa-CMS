namespace DB_Projekt;

public static class Print
{
    /// <summary>
    /// Color switcher
    /// </summary>
    /// <returns>
    /// Changes color of text in console
    /// </returns>
    private static void ChooseClr(string clr) 
    {
        switch (clr.ToLower())
        {
            case "cyan":
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case "dark cyan":
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                break;
            case "dark gray":
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
            case "gray":
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            case "green":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case "red":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "yellow":
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case "dark yellow":
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                break;
            default:
                Console.ResetColor();
                break;
        }
    }

    /// <summary>
    /// Modified Console.Clear() method so there is always a custom text on top
    /// </summary>
    public static void Clear()
    {
        Console.Clear();
        WriteLine("|----------[ Black Mesa Central Messaging System ]----------|\n", "Yellow");
    }

    /// <summary>
    /// Modified Console.WriteLine() - Make the printed string temporarily different color
    /// </summary>
    public static void WriteLine(string text, string clr) //Modified Console.WriteLine
    {
        ChooseClr(clr);
        Console.WriteLine(text);
        Console.ResetColor();
    }
    
    /// <summary>
    /// Modified Console.Write() - Make the printed string temporarily different color
    /// </summary>
    public static void Write(string text, string clr) //Modified Console.Write
    {
        ChooseClr(clr);
        Console.Write(text);
        Console.ResetColor();
    }
}