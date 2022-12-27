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
    // A class that handles the Menu logic
    internal class MenuSystem
    {
        string[] menuArr = new string[3];
        int selectedIndex = 0;
        // CONSTRUCTORS for different sized menus
        public MenuSystem(string item1, string item2, string item3)
        {
            menuArr[0] = item1;
            menuArr[1] = item2;
            menuArr[2] = item3;
        }
        public MenuSystem(string item1, string item2)
        {
            menuArr = new string[2];
            menuArr[0] = item1;
            menuArr[1] = item2;
        }
        public MenuSystem(string item1, string item2, string item3, string item4)
        {
            menuArr = new string[4];
            menuArr[0] = item1;
            menuArr[1] = item2;
            menuArr[2] = item3;
            menuArr[3] = item4;
        }
        public MenuSystem(string item1, string item2, string item3, string item4, string item5)
        {
            menuArr = new string[5];
            menuArr[0] = item1;
            menuArr[1] = item2;
            menuArr[2] = item3;
            menuArr[3] = item4;
            menuArr[4] = item5;
        }
        public MenuSystem(string item1, string item2, string item3, string item4, string item5, string item6)
        {
            menuArr = new string[6];
            menuArr[0] = item1;
            menuArr[1] = item2;
            menuArr[2] = item3;
            menuArr[3] = item4;
            menuArr[4] = item5;
            menuArr[5] = item6;
        }
        // A method that prints the menu when called
        public void PrintSystem()
        {
            Console.Clear();
            // Prints out the menu items in the console, and puts brackets around the selected item.
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
        // Getter and setter for the selected index
        public int SelectIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value >= 0 && value < menuArr.Length)
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
        // A method that allows the user to orientate around the menu
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
                }
                Console.Clear();
                //printer.Print();
                PrintSystem();
            } while (test == false);
            return selectedIndex;
        }
    }
}