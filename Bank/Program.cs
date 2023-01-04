﻿using System;
using System.Dynamic;
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
        int index;
        bool isTrue = true;
        MenuSystem subMenu = new MenuSystem("Se dina konton och saldo", "Överföring mellan konton","Ta ut pengar", "Sätt in pengar", "Logga ut");
        MenuSystem mainMenu = new MenuSystem("Log in", "Reset Password", "Exit");
        CustomerFunds funds = new CustomerFunds();
        User users = new User();
        Login logIn = new Login();
        Reset resetPass = new Reset(users);
        string[,] userList = users.Users;
        PrintSystem print = new PrintSystem();

        print.Print();
        Console.ReadKey();
        do
        {
            mainMenu.PrintSystem();
            index = mainMenu.UseMenu(); // mainMenu, log, res
            bool test = false;
            switch (index)
            {
                case 0:
                    test = logIn.UserLogin(userList);
                    Console.Clear();
                    if(test == true)
                    {
                        AccountsMenu(subMenu, logIn.UserIndex, funds);
                    }
                    break;
                case 1:
                    resetPass.ResetPass();
                    Console.WriteLine("Password was changed! Press any key to continue.");
                    users.UpdateList();
                    Console.ReadKey();
                    break;
                case 2:
                    funds.UpdateFunds();
                    Console.WriteLine("You have exited the program. Good bye!!");
                    isTrue = false;
                    break;
            }
        } while (isTrue);
    }

    private static void AccountsMenu(MenuSystem sMenu, int userIndex, CustomerFunds funds)
    {
        int index = 0;
        bool isTrue = true;
        do
        {
            sMenu.PrintSystem();
            index = sMenu.UseMenu(); // subMenu, log, res
            switch (index)
            {
                case 0:
                    GetAccount(userIndex, funds);
                    Console.ReadKey();
                    break;
                case 1:
                    AccountTransfer(userIndex, funds);
                    Console.ReadKey();
                    break;
                case 2:
                    Withdraw(userIndex, funds);
                    break;
                case 3:
                    AddMoney(userIndex, funds);
                    break;
                case 4:
                    Console.WriteLine("You have exited the program. Good bye!!");
                    isTrue = false;
                    break;
            }
        } while (isTrue);
    }

    private static void AddMoney(int userIndex, CustomerFunds funds)
    {
        int index = 0;
        bool isTrue = true;
        MenuSystem userMenu = new MenuSystem();
        decimal[][] fundList = funds.UserFunds;
        string[][] accounts = new string[][]
        {
            new string[] {"Privatkonto", "Sparkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto", "Matkonto"}
        };

        string[] userAcc = new string[accounts[userIndex].Length + 1];
        for (int i = 0; i < accounts[userIndex].Length; i++)
        {
            userAcc[i] = accounts[userIndex][i];
        }
        userAcc[accounts[userIndex].Length] = "Gå tillbaka";

        userMenu.SetMenu(userAcc);
        decimal answer = 0;
        string pin;
        do
        {
            userMenu.PrintSystem();
            index = userMenu.UseMenu();
            if (index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("How much money do you want to deposit?");
                answer = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Skriv in din pinkod för att bekräfta.");
                pin = Console.ReadLine();
                if (answer <= fundList[userIndex][index] && answer > 0 && pin == "12345")
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    fundList[userIndex][index] += answer;
                    Console.WriteLine("Du har lagt in {0} SEK", answer);
                    Console.WriteLine("Ditt saldo är nu {0} SEK", fundList[userIndex][index]);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Something went wrong. Please try again.");
                }
            }
        } while (isTrue);
    }

    private static void Withdraw(int userIndex, CustomerFunds funds)
    {
        int index = 0;
        bool isTrue = true;
        MenuSystem userMenu = new MenuSystem();
        decimal[][] fundList = funds.UserFunds;
        string[][] accounts = new string[][]
        {
            new string[] {"Privatkonto", "Sparkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto", "Matkonto"}
        };

        string[] userAcc = new string[accounts[userIndex].Length + 1];
        for (int i = 0; i < accounts[userIndex].Length; i++)
        {
            userAcc[i] = accounts[userIndex][i];
        }
        userAcc[accounts[userIndex].Length] = "Gå tillbaka";

        userMenu.SetMenu(userAcc);
        decimal answer = 0;
        string pin;
        do
        {
            userMenu.PrintSystem();
            index = userMenu.UseMenu();
            if (index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("How much money do you want to withdraw?");
                answer = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Skriv in din pinkod för att bekräfta.");
                pin = Console.ReadLine();
                if (answer <= fundList[userIndex][index] && answer > 0 && pin == "12345")
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    fundList[userIndex][index] -= answer;
                    Console.WriteLine("Du har tagit ut {0} SEK", answer);
                    Console.WriteLine("Återstående saldo {0} SEK", fundList[userIndex][index]);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Something went wrong. Please try again.");
                }
            }
        } while (isTrue);
    }

    private static void GetAccount(int userIndex, CustomerFunds funds)
    {
        int index = 0;
        bool isTrue = true;
        MenuSystem userMenu = new MenuSystem();
        string[][] accounts = new string[][]
        {
            new string[] {"Privatkonto", "Sparkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto", "Matkonto"}
        };

        string[] userAcc = new string[accounts[userIndex].Length + 1];
        for(int i = 0; i < accounts[userIndex].Length; i++)
        {
            userAcc[i] = accounts[userIndex][i];
        }
        userAcc[accounts[userIndex].Length] = "Gå tillbaka";

        userMenu.SetMenu(userAcc);
        
        do
        {
            userMenu.PrintSystem();
            index = userMenu.UseMenu();
            if(index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.WriteLine("Du har {0} SEK.", funds.GetFundsAt(userIndex, index));
                Console.ReadLine();
            }
        } while (isTrue);
    }

    private static void AccountTransfer(int userIndex, CustomerFunds funds)
    {
        int index = 0;
        int testIndex = -1;
        bool isTrue = true;
        decimal[][] fundList = funds.UserFunds;
        MenuSystem userMenu = new MenuSystem();
        PrintSystem print = new PrintSystem();
        string[][] accounts = new string[][]
        {
            new string[] {"Privatkonto", "Sparkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto"},
            new string[] {"Privatkonto", "Sparkonto", "Lönekonto", "Spelkonto", "Aktiekonto", "Matkonto"}
        };

        string[] userAcc = new string[accounts[userIndex].Length + 1];
        for (int i = 0; i < accounts[userIndex].Length; i++)
        {
            userAcc[i] = accounts[userIndex][i];
        }
        userAcc[accounts[userIndex].Length] = "Gå tillbaka";
        userMenu.SetMenu(userAcc);

        do
        {
            userMenu.PrintSystem();
            Console.WriteLine("Select an account to transfer money from...");
            index = userMenu.UseMenu();
            if (index == accounts[userIndex].Length)
            {
                break;
            }
            else
            {
                Console.Write("Input amount to transfer: ");
                // Fix try catch / check so amount is not to big
                decimal userInput = decimal.Parse(Console.ReadLine());
                if(userInput <= fundList[userIndex][index] && userInput > 0)
                {
                    fundList[userIndex][index] -= userInput;
                    while (testIndex == -1)
                    {
                        testIndex = userMenu.UseMenu();
                    };
                    fundList[userIndex][testIndex] += userInput;
                    print.PrintTransaction();
                    Console.WriteLine("Du har fört över {0} SEK från {1}t till ditt {2}", userInput, accounts[userIndex][index], accounts[userIndex][testIndex]);
                    Console.WriteLine("Saldot på ditt {0} är nu {1} SEK", accounts[userIndex][testIndex], fundList[userIndex][testIndex]);
                }
                else if(userInput > fundList[userIndex][index])
                {
                    Console.WriteLine("Not enough funds on account. Try again with a lower value.");
                }
            }
            testIndex = -1;
            Console.ReadLine();
        } while (isTrue);
    }
}


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
 * CaseHandler.cs (?)
 * - Klass vars uppgift är att hantera olika switch cases
 * > Metod som tar emot olika antal metoder och har 
 */

/* Jobba på:
 * PrintSystems - Printa ut menyerna?
 * Fixa en simpel version på överföring / ta ut pengar
 */
