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
    //TODO:
    // Läs bara in två decimaler - N2
    private static void Bank()
    {
        // CLASS INITIALIZATION
        Menu customerMenu = new Menu(new string[] { "Check Accounts and Balance", "Transfer between accounts", "Transfer to another user", "Withdraw funds", "Deposit funds", "Log out" });
        Menu mainMenu = new Menu("Log in", "Reset Pincode", "Create new Account", "Exit");
        CustomerFunds funds = new CustomerFunds();
        User users = new User();
        Login logIn = new Login();
        Reset resetPass = new Reset(users);
        PrintSystem print = new PrintSystem();
        AccountCreator userCreate = new AccountCreator();

        int index;
        bool isTrue = true;

        print.Print();
        Console.ReadKey();
        // A menu with 3 options that allows the user to login, reset pincode or exit the program
        do
        {
            //Load user
            string[,] userList = users.Users;
            mainMenu.PrintSystem();
            index = mainMenu.UseMenu();
            switch (index)
            {
                case 0:
                    bool loginSuccess = logIn.UserLogin(userList);
                    Console.Clear();
                    if(loginSuccess)
                    {
                        AccountsMenu(customerMenu, logIn.UserIndex, funds);
                    }
                    break;
                case 1:
                    resetPass.ResetPin();
                    users.UpdateList();
                    Console.ReadKey();
                    break;
                case 2:
                    userCreate.CreateCustomer();
                    //users.UpdateList();
                    break;
                case 3:
                    funds.UpdateFunds();
                    Console.WriteLine("You have exited the program. Good bye!!");
                    isTrue = false;
                    break;
            }
        } while (isTrue);
    }
    // The menu that is ran when a user has managed to login
    // Then calls for all the different methods that run all the banks different use cases
    private static void AccountsMenu(Menu customMenu, int userIndex, CustomerFunds funds)
    {
        bool isTrue = true;
        customMenu.SelectIndex = 0;
        do
        {
            customMenu.PrintSystem();
            int index = customMenu.UseMenu();
            switch (index)
            {
                case 0:
                    CheckAccountFunds(userIndex, funds);
                    break;
                case 1:
                    AccountTransfer(userIndex, funds);
                    funds.UpdateFunds();
                    break;
                case 2:
                    UserTransfers(userIndex, funds);
                    funds.UpdateFunds();
                    break;
                case 3:
                    Withdraw(userIndex, funds);
                    funds.UpdateFunds();
                    break;
                case 4:
                    Deposit(userIndex, funds);
                    funds.UpdateFunds();
                    break;
                case 5:
                    Console.WriteLine("You have exited the program. Good bye!!");
                    isTrue = false;
                    break;
            }
        } while (isTrue);
    }
    // Allows user to see their balance
    private static void CheckAccountFunds(int userIndex, CustomerFunds funds)
    {
        Menu userMenu = new Menu();
        string[][] accounts = Account.Accounts;
        string[] userAccount = Account.ShowAccount(userIndex);
        userMenu.SetMenu(userAccount);
        bool isTrue = true;

        do
        {
            userMenu.PrintSystem();
            int index = userMenu.UseMenu();
            if (index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("Current balance: {0:N2} SEK.", funds.GetFundsAt(userIndex, index));
                Console.ReadKey();
            }
        } while (isTrue);
    }
    // Allows the user to transfer money between their own accounts
    // User can select an account with their keys, and then enter the amount to transfer
    // Then the user can select an account to recieve the funds (Will prompt again if selecting the same account)
    private static void AccountTransfer(int userIndex, CustomerFunds funds)
    {
        Menu userMenu = new Menu();
        decimal[][] fundList = funds.UserFunds;
        string[][] accounts = Account.Accounts;
        string[] userAccount = Account.ShowAccount(userIndex);
        int recieverIndex;
        bool isTrue = true;

        userMenu.SetMenu(userAccount);
        do
        {
            userMenu.SetMenu(userAccount);
            userMenu.PrintSystem();
            Console.WriteLine("Select an account to transfer money from...");
            int index = userMenu.UseMenu();
            if (index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.Write("Input amount to transfer: ");
                bool success = decimal.TryParse(Console.ReadLine(), out decimal userInput);
                if (success)
                {
                    if (userInput <= fundList[userIndex][index] && userInput > 0)
                    {
                        do
                        {
                            userMenu.PrintSystem();
                            Console.WriteLine("Choose another account to transfer money to.");
                            recieverIndex = userMenu.UseMenu();
                        } while (recieverIndex == index);
                        if (recieverIndex == accounts[userIndex].Length)
                        {
                            break;
                        }
                        else
                        {
                            fundList[userIndex][index] -= userInput;
                            fundList[userIndex][recieverIndex] += userInput;
                            PrintSystem.PrintTransaction();
                            Console.WriteLine("You have transfered: {0} SEK from {1} to {2}", userInput, accounts[userIndex][index], accounts[userIndex][recieverIndex]);
                            Console.WriteLine("The balance on your {0} is now: {1} SEK", accounts[userIndex][recieverIndex], fundList[userIndex][recieverIndex]);
                            Console.ReadKey();
                        }
                    }
                    else if (userInput > fundList[userIndex][index])
                    {
                        Console.WriteLine("Not enough funds on account. Try again with a lower value.");
                    }
                }
                else
                {
                    Console.WriteLine("Something went wrong with your input. Please try again.");
                    Console.ReadKey();
                }
            }
            //recieverIndex = -1;
        } while (isTrue);
    }
    // Allows transfers between different users
    // First prompts user to enter which user to transfer funds to
    // After user has:
    // > Selected an account to transfer money from,
    // > Entered a valid amount,
    // > Entered their pincode correctly
    // Then this will transfer money into recieving users Private account - Aka their 0-index account
    private static void UserTransfers(int userIndex, CustomerFunds funds)
    {
        Menu userMenu = new Menu();
        User users = new User();
        string[,] userList = users.Users;
        decimal[][] fundList = funds.UserFunds;
        string[][] accounts = Account.Accounts;
        string[] userAccount = Account.ShowAccount(userIndex);
        bool isTrue = true;
        userMenu.SetMenu(userAccount);

        do
        {
            int transferIndex = CheckUsername(userList, userIndex);
            userMenu.PrintSystem();
            Console.WriteLine("Choose account to transfer from");
            int index = userMenu.UseMenu();
            if (index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("How much do you want to transfer?");
                bool success = decimal.TryParse(Console.ReadLine(), out decimal answer);
                if (success)
                {
                    if (answer <= fundList[userIndex][index] && answer > 0)
                    {
                        fundList[transferIndex][0] += answer;
                        fundList[userIndex][index] -= answer;
                        PrintSystem.PrintTransaction();
                        Console.WriteLine();
                        Console.WriteLine("You have now transfered {0} SEK to {1}", answer, userList[transferIndex, 0]);
                        Console.ReadKey();
                        isTrue = false;
                    }
                }
            }
        } while (isTrue);
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
            for (int i = 0; i < userList.Length / 2; i++)
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
                Console.ReadKey();
            }
        }
    }
    // Allows user to withdraw funds from their account
    // After selecting an account, the user will be prompted with the amount
    // If the amount and pincode are valid, withdraw is success
    // Else asks user to try again
    // After 3 failed pincode attempts, throws user back
    private static void Withdraw(int userIndex, CustomerFunds funds)
    {
        Menu userMenu = new Menu();
        User users = new User();
        decimal[][] fundList = funds.UserFunds;
        string[][] accounts = Account.Accounts;
        string[] userAccount = Account.ShowAccount(userIndex);
        string[,] userList = users.GetUsers(); // Used to validate pincode
        bool isTrue = true;
        string? pin;

        userMenu.SetMenu(userAccount);
        do
        {
            userMenu.PrintSystem();
            int index = userMenu.UseMenu();
            int attempts = 0;
            if (index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("How much money do you want to withdraw?");
                bool success = int.TryParse(Console.ReadLine(), out int answer);
                if (success)
                {
                    if (answer <= fundList[userIndex][index] && answer > 0)
                    {
                        bool transfer = true;
                        while(attempts < 3 && transfer) // Fixa så att användaren loggas ut istället
                        {
                            Console.WriteLine("Enter pincode to confirm withdrawal.");
                            pin = Console.ReadLine();
                            if (pin == userList[userIndex, 1])
                            {
                                fundList[userIndex][index] -= answer;
                                Console.WriteLine("You have withdrawn: {0} SEK", answer);
                                Console.WriteLine("Remaining balance: {0} SEK", fundList[userIndex][index]);
                                transfer = false;
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Wrong pincode. Please try again.");
                                attempts++;
                                Console.ReadKey();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds. Try again.");
                        Console.ReadKey();
                    }
                    
                }
                else
                {
                    Console.WriteLine("Please enter a valid number. Press any key to continue");
                    Console.ReadKey();
                }
                if(attempts >= 3) // Fixa så att användaren loggas ut istället
                {
                    isTrue = false;
                }
            }
        } while (isTrue);
    }
    // Allows user to deposit money into specified accounts
    // After user selects an account, asks for how much to deposit
    private static void Deposit(int userIndex, CustomerFunds funds)
    {
        Menu userMenu = new Menu();
        decimal[][] fundList = funds.UserFunds;
        string[][] accounts = Account.Accounts;
        string[] userAccount = Account.ShowAccount(userIndex);
        bool isTrue = true;

        userMenu.SetMenu(userAccount);
        do
        {
            userMenu.PrintSystem();
            int index = userMenu.UseMenu();
            if (index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("How much money do you want to deposit?");
                bool success = decimal.TryParse(Console.ReadLine(), out decimal answer);
                if (success)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    fundList[userIndex][index] += answer;
                    Console.WriteLine("You have deposited: {0} SEK", answer);
                    Console.WriteLine("Updated balance is now: {0} SEK", fundList[userIndex][index]);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Something went wrong.");
                    Console.ReadKey();
                }

            }
        } while (isTrue);
    }
}

/* Jobba på:
 * ----------------------------------------------------
 * KOPPLA PINKOD VID PENGAHANTERING TILL ANVÄNDAREN -- DONE [X]
 * > Kolla ifall pinkoden stämmer med den sparad för användaren
 * >> Om den gör det, tillåt transaktionen
 * >> Annars, be användaren försöka igen 
 * >>> Ifall användaren misslyckas fler än tre gånger, loggas ut.
 * ----------------------------------------------------
 * ÖVERFÖRING MELLAN KONTON -- BASICS DONE [X]
 * > Ange "kod"
 * > Meddelande som ska skickas med överföringen << FRIVILLIG
 * -
 * Ifall man får en betalning från ett annat konto
 * > Få en "notis"?
 * > Se pengarna samt meddelandet och vem som skickat pengarna
 *  ----------------------------------------------------
 *  GAMBLING/CRYPTO
 *  > Slumpat nummer som hoppar upp och ner
 *  >> Större chans att valutan går nedåt än uppåt
 *  > Möjlighet att investera pengar
 *  >> Kan ta ut investeringen/Pengarna 
 *   ----------------------------------------------------
 *    
 */


/*-----------------------------------------------------------------------------------
 *0. - START AV PROGRAM
 *1. - Välkomna användaren
 *-----------------------------------------------------------------------------------
 *2. - Be om användarnamn och pinkod 
 *2.1. - Om försöket misslyckades, hoppa till steg 2. 
 *2.1.1. -   Lägg till 1 på "login counter"
 *2.1.2. -   Vid tre ogiltiga försök, lås ut det användarnamnet i tre minuter
 *2.2. - Om försöket lyckades - Hoppa vidare till steg 3
 *-----------------------------------------------------------------------------------
 *3. - Skriv ut menyalternativ
 *4. - Tillåt användaren att röra sig i menyn med piltangenter
 *4.1. - Håll koll på användarens index i menyn
 *4.1.1. -   Index-- Ifall användaren går uppåt, Index++ ifall användaren går nedåt
 *4.1.2. -   Spara index när användaraen väljer ett val från menyn
 *4.1.3. -   Skicka det sparade indexet till steg 5
 *-----------------------------------------------------------------------------------
 *5. - Använd steg 3 (Printa menyvalen)
 *5.1. - Använd steg 4 (Tillåt användaren att röra sig i menyn)
 *5.2. - Baserat på användarens inmatning, vilket är en av dessa:
 *5.2.1. -   Kör funktion för att visa användarens konton och saldo
 *5.2.2. -   Kör funktion för överföring mellan konton
 *5.2.3. -   Kör funktion för att ta ut pengar
 *5.3. - Invänta knapptryck eller Enter
 *5.4. - Hoppa till Steg 3
 *-----------------------------------------------------------------------------------
 */

/* Klasser och liknande
 *-----------------------------------------------------------------------------------
 * AccountType.cs
 * - Enum för AccountTypes
 * >> Lagra typer av konton, som: PrivatKonto, SparKonto.. osv osv i en Enum
 * (Alla användare ska ha minst 1 konto - Vilket är PrivatKonto)
 *-----------------------------------------------------------------------------------
 * Login.cs
 * - Klass vars uppgift är att sköta inloggningen till banken
 * > Metod för att logga in, där man har tre försök på sig innan en användare låses ut
 * >> Håll reda på detta index, och tillåt ej nya försök på den användaren (3 minuter)
 * >> Ifall inloggning lyckas, spara detta index 
 * > Metod som returnerar index på den inloggade användaren
 * -----------------------------------------------------------------------------------
 * Reset.cs
 * - Klass vars uppgift är att sköta återställning av lösenord
 * > Metod som frågar efter användarnamnet
 * >> Kolla om användarnamnet finns, 
 * >>> Om användarnamnet inte finns, ge användaren ett felmeddelande och försök igen
 * >>> Om användarnamnet finns, tillåt användaren att ge ett nytt lösenord
 * >>>> Be användaren att upprepa lösenordet
 * >>>>> Kolla ifall lösenorden matchar, ifall dom gör det - Spara
 * >>>>> Ifall lösenorden inte matchar, be användaren skriva in lösenordet på nytt.
 * >> Spara det nya lösenordet i textfilen (Så det sparas även utanför körning)
 * -----------------------------------------------------------------------------------
 * Menu.cs
 * - Klass vars uppgift är att sköta printande av menyn samt navigering utav menyer
 * > Metod som tar in tre menyval och sedan kallar på Print.cs funktionen med dessa
 * >> MethodOverload som tar in 2,4,5,6 menyval och sedan kallar på print.cs med dessa
 * > Getter-Setter för indexen i menyn
 * > Metod för att använda/navigera menyn som kallar på InputHandler för användarinput
 * > 
 * -----------------------------------------------------------------------------------
 * Print.cs
 * - Klass för att printa ut meddelanden med olika stiler
 * - Alla metoder tar emot en string - För att sedan printa ut denna med specifik stil
 * > Metod som printar ut Titeln till banken (Förbestämt)
 * > Metod som printar ut en meny med tre val
 * >> MethodOverload som printar ut 2,4,5,6 val
 * > Getter/Setter som tar emot en string för att printas ut
 * > Metod som printar ut när något lyckas - Överföring, login osv
 * > Metod som använder sig av thread.sleep för att be användaren vänta
 * > Metod som printar ut ett hejdå-meddelande till användaren
 * -----------------------------------------------------------------------------------
 * InputHandler.cs
 * - Klass vars uppgift är att hantera användarinput - T.ex. Läsa av piltangenterna för menyn
 * > Metod för att ta emot userInput och sedan returnera knappen som trycktes på
 * -----------------------------------------------------------------------------------
 * User.cs
 * - Klass vars uppgift är att hantera AnvändarArrayn - Läsa, och skriva över 
 * > Constructor för att läsa in Users.txt och skapa en array
 * > Metod för att returnera arrayn
 * > Getter och Setter för lösenordet
 * > metod för att ändra lösenordet som tar in ett index
 * > metod för att uppdatera listan, efter att ändringar gjorts och sedan spara dessa i textfilen
 * -----------------------------------------------------------------------------------
 * Users.txt
 * - En textfil som bevarar användarnamn samt lösenord
 * - Tillåter ändringar utav lösenord som sparas mellan körningar
 * -----------------------------------------------------------------------------------
 */

