# **C# Console Banking Application**
This is a console banking application made with **C#** as a school project.

## **Features**
 - Movement with arrow keys.
 - Money transfers between own accounts
 - Money transfers to other users
 - To deposit & withdraw money
 - Creation of new accounts
 - Changing passwords of existing users
 - A login function that locks out a user after too many failed attempts. Other users are still able to log in.
 - Changes that occur to Users or their Funds are both saved in a text file


## Quick run down
At launch, you are presented with a quick welcome screen to the bank.
After that you are presented with a menu that allows you to either:
 - Log in
 - Reset pin code of an existing user
 - Create a new account
 - Exit the program
 
 And after you have logged in, you are presented with the main menu that all accounts have in common, with different choices that will call the appropriate method to run them. That includes:
 
 - Check your accounts and their balances.
 - Transfers between your accounts.
 - Transfers to another user.
 - Ability to withdraw money from any of your accounts.
 - Ability to deposit money into any of your accounts.
 - Log out and return to the menu above.
 
 And each of the base users will have varying amounts of accounts.

## **Code overview**

`Account.cs` -- A static class that holds the jagged array containing the different types of accounts a user can have. Has two simple methods to *get an account at a given index and return it*. Also, a method that creates an array with the menu items at a given index, and adds a go back option. To be used with the `Menu.cs` class.

`AccountCreator.cs` -- A class that allows the user to create new accounts. Set their username, pin code, and account type. And then adds it to the `Users.txt` and `Funds.txt`

**`AccountType.cs`** -- An enum that is used to identify what accounts to display to each user.

CustomerFunds.cs -- A class that has methods to handle reading data from text files and putting those into usable arrays, and also writing data to text files. Also includes methods to retrieve funds for specific users.

`InputHandler.cs` -- A *"class"* that has one method and that is to validate user input and return the valid key press.

Login.cs -- A class that handles all tasks related to the User Login process. The method  `UserLogin` validates user input, and keeps track of failed attempts per user by storing them in an array. The second method is then called after three failed login attempts. And that method keeps unique timers per user, so you are only locked out of that specific account for three minutes.

`Menu.cs` -- A class with the purpose of creating menus from regular string arrays. Allows the user to move freely in the menu with their arrow keys.

`PrintSystems.cs` -- Class with the purpose of providing some basic methods for console output. Such as, `PrintTransaction` which prints out a loading bar, that gets called when transactions are being processed.

`Program.cs` -- Includes everything that makes the application tick. Methods for all the banks functions, such as:

 - CheckAccountFunds -- Which allows a user to see funds to all their different accounts.
 - AccountTransfer -- Which allows the user to send money between their personal accounts.
 - UserTransfers -- Which allows transfers between users.
 - Withdraw -- Allows the user to withdraw money from their accounts.
 - Deposit -- Allows the user to deposit money into their accounts.
 All these different menu options are printed out with the help of `Menu.cs` since it takes an array, and turns it into a interactable menu.

`User.cs` -- A class that reads from the Users.txt, parses it and then stores it as a 2d string array to be used by other methods and classes. Also, has a method to write any changes back to Users.txt
## Reflections and other thoughts
**Many** of the choices made in the creation of this program, was to experiment more with classes and their functions. And in a way, that has made many of the classes to be designed more in the way of *methods* instead of typical classes.
But overall, I feel like this project has thought me a lot and solidified more of my knowledge in the way arrays work. But there is one specific class that turned out as planned, and that is `Menu.cs` as it provides me a easily expandable system to create new menus of varying sizes.
And as earlier mentioned, the design of many other classes are more similar to that of methods. As such, many of the classes could have been made static instead of having to create new instances. 
In the future, I would probably create all the different Accounts and their functionalities in a different way. Taking more of an *OOP* approach, by **utilizing inheritance** when creating the different accounts. In such a way I could create an **Abstract** account that covers all the basic necessities of an account, with the possibility to add more in other types of account classes.
And, as I have still not learned databases *(At the time of writing this)* that is a **big change** I would make in the way of storing the data. Right now things are stored in a plain text file, which is not optimal.
One of the current limitations of the program is that you can enter as many decimals as you wish, none of which is shown to the user. But is kept stored in the text file. That is also something I would limit in any future programs.

## The base users 

 1. Username: Chrille -- Pin: 54321 -- Two accounts
 2. Username: Bob -- Pin: 7331 -- Three accounts
 3. Username: Billy -- Pin: 25354555 -- Four accounts
 4. Username: Kalle -- Pin: 313 -- Five accounts
 5. Username: Herman -- Pin: 1337 -- Six accounts
