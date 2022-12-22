using System.Dynamic;

namespace Bank;
class Program
{
    static void Main(string[] args)
    {
        MenuSystem subMenu = new MenuSystem("name", "Class", "Go back");
        MenuSystem subMenu2 = new MenuSystem("Char1", "Char2", "Char3");
        MenuSystem mainMenu = new MenuSystem(subMenu);
        Login log = new Login();
        Reset res = new Reset();
        mainMenu.PrintSystem();
        mainMenu.UseMenu(mainMenu, log, res);

    }
}
