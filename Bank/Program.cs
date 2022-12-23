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
        MenuSystem subMenu = new MenuSystem("Se dina konton och saldo", "Överföring mellan konton", "Logga ut");
        MenuSystem mainMenu = new MenuSystem(subMenu);
        Login log = new Login();
        Reset res = new Reset();

        do
        {
            mainMenu.PrintSystem();
            index = mainMenu.UseMenu(); // mainMenu, log, res
            switch (index)
            {
                case 0:
                    log.UserLogin();
                    subMenu.PrintSystem();
                    subMenu.UseMenu(); // subMenu, log, res
                    break;
                case 1:
                    res.ResetPass();
                    break;
                case 2:
                    Console.WriteLine("You have exited the program. Good bye!!");
                    isTrue = false;
                    break;
            }
            Console.ReadKey();
        } while (isTrue);


    }
}
