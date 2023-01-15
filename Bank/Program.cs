using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Bank;
class Program
{
    static void Main(string[] args)
    {
        Bank();
    }

    private static void Bank()
    {
        // CLASS INITIALIZATION
        //Menu class allows for easy creation of console menus. 
        Menu CustomerMenu = new Menu(new string[] { "Check Accounts and Balance", "Transfer between accounts", "Transfer to another user", "Withdraw funds", "Deposit funds", "Log out" });
        Menu BankMenu = new Menu(new string[] { "Log in", "Reset Pincode", "Create new Account", "Exit" });
        CustomerFunds FundManager = new CustomerFunds(); // Handles the user funds - Saving and reading from Funds.txt
        User UserManager = new User(); // Handles the User data - Saving and reading from User.txt
        Login LoginSystem = new Login(); // Handles the Login system, returns true if a user logs in correctly
        PrintSystem Printer = new PrintSystem(); // A print system with different print options for console output
        AccountCreator AccountCreator = new AccountCreator(); // Handles the creation of accounts
        bool IsRunning = true;

        Printer.PrintWelcome(); // Prints a welcome message to the user
        Console.ReadKey(true);
        // A menu with 3 options that allows the user to login, reset pincode or exit the program
        do
        {
            UserManager.LoadUser(); // Load in users from User.txt
            FundManager.LoadFunds(); // Load in funds from Funds.txt
            BankMenu.PrintSystem();
            int index = BankMenu.UseMenu(); // Returns index of selected menu choice
            switch (index)
            {
                case 0:
                    string[,] userList = UserManager.Users;
                    if (LoginSystem.UserLogin(userList))
                    {
                        AccountsMenu(CustomerMenu, LoginSystem.UserIndex, FundManager);
                    }
                    break;
                case 1:
                    UserManager.ResetPin();
                    UserManager.UpdateList();
                    break;
                case 2:
                    AccountCreator.CreateCustomer();
                    break;
                case 3: // Program exit
                    FundManager.UpdateFunds();
                    Console.WriteLine("You have exited the program. Good bye!!");
                    IsRunning = false;
                    break;
            }
        } while (IsRunning);
    }

    // The menu that runs when a user has managed to login
    // Then calls for all the different methods that run all the banks different use cases
    private static void AccountsMenu(Menu CustomerMenu, int userIndex, CustomerFunds FundManager)
    {
        bool IsRunning = true;
        CustomerMenu.SelectIndex = 0;
        while (IsRunning)
        {
            CustomerMenu.PrintSystem();
            int index = CustomerMenu.UseMenu(); // Returns index of selected menu choice
            switch (index)
            {
                case 0:
                    CheckAccountFunds(userIndex, FundManager);
                    break;
                case 1:
                    AccountTransfer(userIndex, FundManager);
                    FundManager.UpdateFunds();
                    break;
                case 2:
                    UserTransfers(userIndex, FundManager);
                    FundManager.UpdateFunds();
                    break;
                case 3:
                    Withdraw(userIndex, FundManager);
                    FundManager.UpdateFunds();
                    break;
                case 4:
                    Deposit(userIndex, FundManager);
                    FundManager.UpdateFunds();
                    break;
                case 5: // Log out - Sends user back to main menu
                    IsRunning = false;
                    break;
            }
        };
    }
    // Allows user to see their account balance
    private static void CheckAccountFunds(int userIndex, CustomerFunds Funds)
    {
        Menu CheckFundsMenu = new Menu();
        string[] userAccount = Account.ShowAccount(userIndex); // Gets a string array containing the current users accounts
        CheckFundsMenu.SetMenu(userAccount); // Sets the menuArray to show userAccounts 
        bool IsRunning = true;

        while (IsRunning)
        {
            CheckFundsMenu.PrintSystem();
            // Returns index of selected menu choice
            int index = CheckFundsMenu.UseMenu();
            // Checks if user has selected last option in accounts menu 
            // Which will always be an option to go back, if selected - breaks the loop 
            if (index == Account.GetAccount(userIndex).Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("Current balance: {0:N2} SEK.", Funds.GetFundsAt(userIndex, index));
                Console.ReadKey(true);
            }
        };
    }
    // Allows the user to transfer money between their own accounts
    // User can select an account with their keys, and then enter the amount to transfer
    // Then the user can select an account to recieve the funds (Will prompt again if selecting the same account)
    private static void AccountTransfer(int userIndex, CustomerFunds Funds)
    {
        Menu AccountTransferMenu = new Menu();
        decimal[] fundList = Funds.GetUserFunds(userIndex); // Access to logged in users funds
        string[] accounts = Account.GetAccount(userIndex); // Account.GetAccount(userIndex) <-- istället?
        string[] userAccount = Account.ShowAccount(userIndex); // Gets a string array containing the current users accounts
        bool IsRunning = true;
        int recieverIndex;

        AccountTransferMenu.SetMenu(userAccount);
        while (IsRunning)
        {
            AccountTransferMenu.SetMenu(userAccount);
            AccountTransferMenu.PrintSystem(); // Returns index of selected menu choice
            Console.WriteLine("Select an account to transfer money from...");
            int index = AccountTransferMenu.UseMenu();
            // Checks if user has selected last option in accounts menu 
            // Which will always be an option to go back, if selected - breaks the loop 
            if (index == Account.GetAccount(userIndex).Length)
            {
                break;
            }
            else
            {
                Console.Write("Input amount to transfer: ");
                bool success = decimal.TryParse(Console.ReadLine(), out decimal userInput); // Validates user input
                if (success)
                {
                    if (userInput <= fundList[index] && userInput > 0)
                    {
                        // Prompts the user to select ANOTHER account than the previously chosen one
                        do
                        {
                            AccountTransferMenu.PrintSystem();
                            Console.WriteLine("Choose another account to transfer money to.");
                            recieverIndex = AccountTransferMenu.UseMenu(); // Returns index of selected menu choice
                        } while (recieverIndex == index);
                        // Checks if user has selected last option in accounts menu 
                        // Which will always be an option to go back, if selected - breaks the loop 
                        if (recieverIndex == Account.GetAccount(userIndex).Length)
                        {
                            break;
                        }
                        else 
                        {
                            fundList[index] -= userInput;
                            fundList[recieverIndex] += userInput;
                            PrintSystem.PrintTransaction();
                            Console.WriteLine("You have transfered: {0:N2} SEK from {1} to {2}", userInput, accounts[index], accounts[recieverIndex]);
                            Console.WriteLine("The remaining balance on your {0} is now: {1:N2} SEK", accounts[index], fundList[index]);
                            Console.WriteLine("The new balance on your {0} is now: {1:N2} SEK", accounts[recieverIndex], fundList[recieverIndex]);
                            Console.ReadKey(true);
                        }
                    }
                    else if (userInput > fundList[index])
                    {
                        Console.WriteLine("Not enough funds on account. Try again with a lower value.");
                    }
                }
                else
                {
                    Console.WriteLine("Something went wrong with your input. Please try again.");
                    Console.ReadKey(true);
                }
            }
        }
    }
    // Allows transfers between different users
    // First prompts user to enter which user to transfer funds to
    // After user has:
    // > Selected an account to transfer money from,
    // > Entered a valid amount,
    // > Entered their pincode correctly
    // Then this will transfer money into recieving users Private account - Aka their 0-index account
    private static void UserTransfers(int userIndex, CustomerFunds Funds)
    {
        Menu UserTransferMenu = new Menu();
        User users = new User();
        string[,] userList = users.Users;
        decimal[][] fundList = Funds.UserFunds;
        string[] userAccount = Account.ShowAccount(userIndex); // Gets a string array containing the current users accounts
        bool IsRunning = true;
        UserTransferMenu.SetMenu(userAccount);

        while (IsRunning)
        {
            int attempts = 3;
            int transferIndex = CheckUsername(userList, userIndex);
            UserTransferMenu.PrintSystem();
            Console.WriteLine("Choose account to transfer from");
            int index = UserTransferMenu.UseMenu(); // Returns index of selected menu choice
            // Checks if user has selected last option in accounts menu 
            // Which will always be an option to go back, if selected - breaks the loop 
            if (index == Account.GetAccount(userIndex).Length)
            {
                break;
            }
            else 
            {
                Console.WriteLine("How much do you want to transfer?");
                bool success = decimal.TryParse(Console.ReadLine(), out decimal answer); // Validates user input
                if (success)
                {
                    if (answer <= fundList[userIndex][index] && answer > 0)
                    {
                        bool transfer = true;
                        while (attempts > 0 && transfer)
                        {
                            Console.Write("Enter your pin to confirm the transaction:");
                            string pin = Console.ReadLine();
                            if (pin == userList[userIndex, 1])
                            {
                                fundList[transferIndex][0] += answer;
                                fundList[userIndex][index] -= answer;
                                PrintSystem.PrintTransaction();
                                Console.WriteLine("You have now transfered {0:N2} SEK to {1}", answer, userList[transferIndex, 0]);
                                Console.WriteLine("The remaining balance on your {0} is now: {1:N2} SEK", userAccount[userIndex], fundList[userIndex][index]);
                                Console.ReadKey(true);
                                IsRunning = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong pin. {0} attempts remaining.", --attempts);
                            }
                        }
                        if(attempts <= 0)
                        {
                            Console.WriteLine("You failed too many attempts at validating your pin.");
                            Console.ReadKey(true);
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds.");
                        Console.ReadKey(true);
                        break;
                    }
                }
            }
        }
    }

    // A method that checks if the username exists and is eligible 
    // If username exists, returns their index-value 
    // Else prompts user to enter a valid username
    private static int CheckUsername(string[,] userList, int userIndex)
    {
        int transferIndex = -1;
        while (true)
        {
            Console.Write("Enter the username who you want to transfer money to: ");
            string userName = Console.ReadLine().ToUpper();
            for (int i = 0; i < userList.Length / 3; i++)
            {
                if (userName == userList[i, 0] && i != userIndex)
                {
                    transferIndex = i;
                    return transferIndex;
                }
            }
            if (transferIndex == -1)
            {
                Console.WriteLine("Enter a valid username");
                Console.ReadKey(true);
            }
        }
    }
    // Allows user to withdraw funds from their account
    // After selecting an account, the user will be prompted with the amount
    // If the amount and pincode are valid, withdraw is success
    // Else asks user to try again
    // After 3 failed pincode attempts, throws user back to menu
    private static void Withdraw(int userIndex, CustomerFunds Funds)
    {
        Menu WithdrawMenu = new Menu();
        User users = new User();
        decimal[] fundList = Funds.GetUserFunds(userIndex); // Access to logged in users funds
        string[] userAccount = Account.ShowAccount(userIndex); // Gets a string array containing the current users accounts
        string[,] userList = users.Users; // Used to validate pincode
        bool IsRunning = true;

        WithdrawMenu.SetMenu(userAccount);
        do
        {
            WithdrawMenu.PrintSystem();
            int index = WithdrawMenu.UseMenu();
            int attempts = 3;
            // Checks if user has selected last option in accounts menu 
            // Which will always be an option to go back, if selected - breaks the loop 
            if (index == Account.GetAccount(userIndex).Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("How much money do you want to withdraw?");
                bool success = int.TryParse(Console.ReadLine(), out int answer); // Validates user input
                if (success)
                {
                    if (answer <= fundList[index] && answer > 0)
                    {
                        bool transfer = true;
                        while(attempts > 0 && transfer) // Fixa så att användaren loggas ut istället
                        {
                            Console.WriteLine("Enter pincode to confirm withdrawal.");
                            string pin = Console.ReadLine();
                            if (pin == userList[userIndex, 1])
                            {
                                fundList[index] -= answer;
                                Console.WriteLine("You have withdrawn: {0:N2} SEK", answer);
                                Console.WriteLine("Remaining balance: {0:N2} SEK", fundList[index]);
                                transfer = false;
                                Console.ReadKey(true);
                            }
                            else
                            {
                                Console.WriteLine("Wrong pincode. {0} attempts remaining.", --attempts);
                                Console.ReadKey(true);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds. Try again.");
                        Console.ReadKey(true);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Please enter a valid number. Press any key to continue");
                    Console.ReadKey(true);
                }
                if(attempts >= 3) // Fixa så att användaren loggas ut istället
                {
                    IsRunning = false;
                }
            }
        } while (IsRunning);
    }
    // Allows user to deposit money into specified accounts
    // After user selects an account, asks for how much to deposit
    private static void Deposit(int userIndex, CustomerFunds Funds)
    {
        Menu DepositMenu = new Menu();
        decimal[] fundList = Funds.GetUserFunds(userIndex); // Access to logged in users funds
        string[] userAccount = Account.ShowAccount(userIndex); // Gets a string array containing the current users accounts
        bool IsRunning = true;

        DepositMenu.SetMenu(userAccount);
        do
        {
            DepositMenu.PrintSystem();
            int index = DepositMenu.UseMenu();
            // Checks if user has selected last option in accounts menu 
            // Which will always be an option to go back, if selected - breaks the loop 
            if (index == Account.GetAccount(userIndex).Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("How much money do you want to deposit?");
                bool success = decimal.TryParse(Console.ReadLine(), out decimal answer); // Validates user input
                if (success && answer > 0)
                {
                    fundList[index] += answer;
                    Console.WriteLine("You have deposited: {0:N2} SEK", answer);
                    Console.WriteLine("Updated balance is now: {0:N2} SEK", fundList[index]);
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Something went wrong.");
                    Console.ReadKey(true);
                }

            }
        } while (IsRunning);
    }
}


