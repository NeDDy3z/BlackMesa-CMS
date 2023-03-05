namespace DB_Projekt;

public class Menu
{
    public static User currentUser;
    private readonly int delay = 1500;


    /// <summary>
    /// Main menu - switches to a different option based on user input
    /// </summary>
    public void MainMenu()
    {
        Print.Clear();

        Print.WriteLine("|---[ Main menu ]---|", "Cyan");
        Print.WriteLine("  [1] Login\n  [2] Register\n  [0] Exit\n", "Dark Cyan");

        switch (Console.ReadLine()) //Options
        {
            case "1":
                UserLogin();
                break;
            case "2":
                UserRegister();
                break;
            case "0":
                break;
            default: //Error check
                Print.WriteLine("This option does not exist.", "Red");
                Thread.Sleep(delay);
                MainMenu();
                break;
        }
    }

    /// <summary>
    /// Login - checks if username and password match in DB
    /// </summary>
    private void UserLogin()
    {
        Print.Clear();

        Print.Write("|---[ Login ]---|", "Cyan");
        Print.Write(" (0 - exit)\n", "Dark Gray");

        Print.Write("  Username: ", "Dark Cyan");
        var usr = Console.ReadLine();
        if (usr == "0") MainMenu(); //Type 0 to return back

        Print.Write("  Password: ", "Dark Cyan");
        var pass = Console.ReadLine();
        if (pass == "0") MainMenu();

        if (User.LoginUser(usr, pass)) //Check if username and password is correct.
        {
            Print.WriteLine("\nLogin successful.", "Green");
            Thread.Sleep(delay);
            UserMenu();
        }
        else //if not return error
        {
            Print.WriteLine("\nUsername or password is incorrect.", "Red");
            Thread.Sleep(delay);
            UserLogin();
        }
    }

    /// <summary>
    /// Register user - Inserts new user into DB
    /// </summary>
    private void UserRegister()
    {
        Print.Clear();

        Print.Write("|---[ Register ]---|", "Cyan");
        Print.Write(" (0 - exit)\n", "Dark Gray");

        Print.Write("  Username: ", "Dark Cyan");
        var usr = Console.ReadLine();
        if (usr == "0") MainMenu();
        if (usr.Length > 50) //Cant be longer than 50 chars.
        {
            Print.WriteLine("\nUsername is too long. It cannot exceed 50 characters", "Red");
            Thread.Sleep(delay);
            UserRegister();
        }
        if (usr.Contains(' '))
        {
            Print.WriteLine("\nUsername cannot contain space character", "Red");
            Thread.Sleep(delay);
            UserRegister();
        }

        if (DatabaseManager.Exists("usr", "username", usr))
        {
            Print.WriteLine("\nUsername is already registered.", "Red");
            Thread.Sleep(delay);
            UserRegister();
        }

        Print.Write("  Password: ", "Dark Cyan");
        var pass = Console.ReadLine();
        if (pass == "0") MainMenu();
        if (usr.Length > 500)
        {
            Print.WriteLine("\nPassword is too long. It cannot exceed 500 characters", "Red");
            Thread.Sleep(delay);
            UserRegister();
        }

        User.RegisterUser(usr, pass); //Inset into DB

        Print.WriteLine("\nUser registered.", "Green");
        Thread.Sleep(delay);
        MainMenu();
    }

    /// <summary>
    /// User menu - switches to a different submenu based on user input
    /// </summary>
    private void UserMenu()
    {
        Print.Clear();

        Print.WriteLine("|---[ User menu ]---|", "Cyan");
        Print.WriteLine("  [1] Inbox\n  [2] Sent\n  [3] New Message\n  [4] Delete User\n  [0] Exit\n",
            "Dark Cyan");

        switch (Console.ReadLine()) //Options
        {
            case "1":
                Inbox();
                break;
            case "2":
                Sent();
                break;
            case "3":
                NewMessage();
                break;
            case "4":
                Delete();
                break;
            case "0":
                MainMenu();
                break;
            default:
                Print.WriteLine("\nThis option does not exist.\n", "Red");
                Thread.Sleep(delay);
                UserMenu();
                break;
        }
    }

    /// <summary>
    /// Calls RecievedMsgs() from Message.cs
    /// </summary>
    private void Inbox()
    {
        Print.Clear();

        Message.RecievedMsgs();

        Print.WriteLine("[1] Delete message\n[0] Back\n", "Cyan");
        switch (Console.ReadLine())
        {
            case "1":
                Print.Write("Title of message you want to delete: ", "Cyan");
                string choice = Console.ReadLine();
                if (choice == "0") Inbox();
                Message.DeleteMsg(choice, true);
                
                Print.WriteLine("\nMessage deleted\n", "Green");
                Thread.Sleep(delay);
                Inbox();
                break;
            case "0": UserMenu();
                break;
            default:
                Print.WriteLine("\nThis option does not exist.\n", "Red");
                Thread.Sleep(delay);
                Inbox();
                break;
        }
    }

    /// <summary>
    /// Calls SentMsgs() from Message.cs
    /// </summary>
    private void Sent()
    {
        Print.Clear();

        Message.SentMsgs();

        Print.WriteLine("[1] Delete message\n[0] Back\n", "Cyan");
        switch (Console.ReadLine())
        {
            case "1":
                Print.Write("Title of message you want to delete: ", "Cyan");
                string choice = Console.ReadLine();
                if (choice == "0") Sent();
                Message.DeleteMsg(choice, false);
                
                Print.WriteLine("\nMessage deleted\n", "Green");
                Thread.Sleep(delay);
                Sent();
                break;
            case "0": UserMenu();
                break;
            default:
                Print.WriteLine("\nThis option does not exist.\n", "Red");
                Thread.Sleep(delay);
                Sent();
                break;
        }
    }

    /// <summary>
    /// Sent out new message
    /// </summary>
    private void NewMessage()
    {
        Print.Clear();

        Print.Write("|---[ New Message ]---|", "Cyan");
        Print.Write(" (0 - exit)\n", "Dark Gray");

        Print.Write("To: ", "Dark Cyan");
        var recipient = Console.ReadLine();
        if (recipient == "0") UserMenu();
        string[] recipientList = recipient.Split(' ');
        bool ok = true;
        foreach (var rec in recipientList)
        {
            if (DatabaseManager.Exists("usr", "username", rec)) ok = false;
        }
        if (ok)
        {
            Print.WriteLine("This user does not exist, try again.", "Red");
            Thread.Sleep(delay);
            NewMessage();
        }
        else
        {
            Print.Write("Title: ", "Dark Cyan");
            var title = Console.ReadLine();
            if (DatabaseManager.Exists("msg", "title", title))
            {
                Print.WriteLine("\nThis title has already been used.\n", "Red");
                Thread.Sleep(delay);
                NewMessage();
            }

            if (title.Length > 54)
            {
                Print.WriteLine("\nTitle is too long. It cannot exceed 54 characters.\n", "Red");
                Thread.Sleep(delay);
                NewMessage();
            }

            Print.WriteLine("Content: ", "Dark Cyan");
            var content = Console.ReadLine();
            if (content == "0") UserMenu();
            if (content.Length > 4000)
            {
                Print.WriteLine("\nContents is too long. It cannot exceed 4000 characters", "Red");
                Thread.Sleep(delay);
                NewMessage();
            }

            Message.SendMessage(recipient, title, content); //Insert msg into DB

            Print.WriteLine("\nMessage sent.", "Green");
            Thread.Sleep(delay);
            UserMenu();
        }
    }

    /// <summary>
    /// Delete logged in user
    /// </summary>
    private void Delete()
    {
        Print.Clear();
        Print.WriteLine("Are you sure about that?\n  [1] Yes\n  [0] No\n", "Cyan");
        switch (Console.ReadLine())
        {
            case "1":
                Print.Write("Password: ", "Cyan");
                if (currentUser.CheckPass(Console.ReadLine())) //If password checks out
                {
                    currentUser.DeleteUser();
                    Print.WriteLine("\nUser deleted successfully.", "Green");
                    Thread.Sleep(delay);
                    MainMenu();
                }
                else
                {
                    Print.WriteLine("\nPassword is incorrect.", "Red");
                    Thread.Sleep(delay);
                    UserMenu();
                }

                break;
            case "0":
                UserMenu();
                break;
            default:
                Print.WriteLine("\nThis option does not exist.", "Red");
                break;
        }
    }
}