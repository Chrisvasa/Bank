using System.Dynamic;

namespace Bank;
class Program
{
    static void Main(string[] args)
    {
        TestFunction();
    }

    private static void TestFunction()
    {
        int index;
        bool isTrue = true;
        MenuSystem subMenu = new MenuSystem("Se dina konton och saldo", "Överföring mellan konton","Ta ut pengar", "Logga ut");
        MenuSystem mainMenu = new MenuSystem(subMenu);
        User users = new User();
        Login log = new Login();
        Reset res = new Reset(users);
        string[,] userList = users.GetUsers();


        do
        {
            mainMenu.PrintSystem();
            index = mainMenu.UseMenu(); // mainMenu, log, res
            switch (index)
            {
                case 0:
                    log.UserLogin(userList);
                    TastyFunction(subMenu);
                    break;
                case 1:
                    res.ResetPass(0);
                    Console.WriteLine("Password was changed! Press any key to continue.");
                    users.UpdateList();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("You have exited the program. Good bye!!");
                    isTrue = false;
                    break;
            }
        } while (isTrue);
    }

    private static void TastyFunction(MenuSystem sMenu)
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
                    Console.WriteLine("lmao");
                    Console.ReadKey();
                    break;
                case 1:
                    Console.WriteLine("rofl");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("lel");
                    break;
                case 3:
                    Console.WriteLine("You have exited the program. Good bye!!");
                    isTrue = false;
                    break;
            }
        } while (isTrue);

    }
}
