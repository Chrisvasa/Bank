using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bank
{
    internal class MenuSystem
    {
        string[] menuArr = new string[3];
        //PrintSystem printer = new PrintSystem();
        //InputHandler menuInput = new InputHandler();
        int selectedIndex = 0;
        public MenuSystem(MenuSystem menu)
        {
            menuArr[0] = "Login";
            menuArr[1] = "Reset password";
            menuArr[2] = "Exit";
            //printer.Text = "Character Creator";
        }
        public MenuSystem(string item1, string item2, string item3)
        {
            menuArr[0] = item1;
            menuArr[1] = item2;
            menuArr[2] = item3;
            //printer.Text = Title;
        }
        // Prints the menu
        public void PrintSystem()
        {
            Console.Clear();
            for (int i = 0; i < menuArr.Count(); i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine("[ {0} ]", menuArr[i]);
                }
                else
                {
                    Console.WriteLine("  {0}  ", menuArr[i]);
                }
            }
        }

        public int SelectIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value >= 0 && value < 3)
                {
                    selectedIndex = value;
                }
            }
        }

        public string[] GetMenu()
        {
            return menuArr;
        }

        public void SetMenu(string[] menu)
        {
            menuArr = menu;
        }

        public int UseMenu() // MenuSystem currentMenu, Login log, Reset res
        {
            InputHandler menuInput = new InputHandler();

            ConsoleKey key;
            bool test = false;
            do
            {
                key = menuInput.ReadInput();
                if (key == ConsoleKey.UpArrow)
                {
                    SelectIndex--;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    SelectIndex++;
                }
                else if (key == ConsoleKey.Enter || key == ConsoleKey.Spacebar)
                {
                    test = true;
                    break;
                    //switch (currentMenu.SelectIndex)
                    //{
                    //    case 0:
                    //        test = log.UserLogin();
                    //        break;
                    //    case 1:
                    //        res.ResetPass();
                    //        break;
                    //    case 2:
                    //        Console.WriteLine("You chose exit. Press any key to close the program.");
                    //        Console.ReadKey();
                    //        test = true;
                    //        break;
                    //}
                }
                Console.Clear();
                //printer.Print();
                PrintSystem();
            } while (test == false);
            return selectedIndex;
        }
    }
}