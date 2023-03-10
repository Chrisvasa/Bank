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
    internal class Menu
    {
        string[] menuArr = new string[3];
        int selectedIndex = 0;
        public Menu() 
        {
            selectedIndex = 0;
        }
        public Menu(string[] items)
        {
            menuArr = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                menuArr[i] = items[i];
            }
        }

        // A method that prints the menu when called
        public void PrintSystem()
        {
            Console.Clear();
            // Prints out the menu items in the console, and puts brackets around the selected item.
            for (int i = 0; i < menuArr.Count(); i++)
            {
                // Checks if the current menu choice is the selected item 
                // Makes the text green and puts brackets around it
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[ {0} ]", menuArr[i]);
                }
                else 
                {
                    Console.ResetColor();
                    Console.WriteLine("  {0}  ", menuArr[i]);
                }
                Console.ResetColor();
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

        public void SetMenu(string[] menu)
        {
            menuArr = menu;
        }

        // A method that allows the user to orientate around the menu
        public int UseMenu() 
        {
            InputHandler menuInput = new InputHandler();

            ConsoleKey userInput;
            bool usingMenu = true;
            do
            {
                userInput = menuInput.ReadInput();
                // Moves up and down in the array, depending on the input
                // If user presses enter, breaks the loop and returns currenty selected index
                if (userInput == ConsoleKey.UpArrow)
                {
                    SelectIndex--;
                }
                else if (userInput == ConsoleKey.DownArrow)
                {
                    SelectIndex++;
                }
                else if (userInput == ConsoleKey.Enter || userInput == ConsoleKey.Spacebar)
                {
                    break;
                }
                Console.Clear();
                PrintSystem();
            } while (usingMenu);
            return selectedIndex;
        }
    }
}