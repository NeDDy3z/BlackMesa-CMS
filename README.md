# BlackMesa-CMS

Project for PV subject on SPSE Jenčná, it is a straightforward console messenger program.
Users can send messages to other users using this CLI, these messages are immediately saved to an MSSQL database. Security and real life usage are very very slim.

<sub>
The name Black Mesa - CMS (Central Messaging System) suggest it is used by the "fictional" research corporation Black Mesa, from the Half-Life video game series made by Valve.
</sub>

## Functions

The application features few menus from which user can manage some basic messaging tasks.

**Main menu** = 3 options
- Login = login into existing user
- Register = register new user
- Exit = exit the application

**User menu** = 5 options
- Inbox = displays all recieved messages
- Sent = displays all sent messages
- New message = send new message
- Delete user = delete signed in user
- Exit = log out to and return back to main menu

*In Inbox and Sent menu, user can delete any message of their choice*

## Used tools:
- C# .Net7
- IntelliJ Rider
- SQL Server Management Studio 19
