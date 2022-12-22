using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class InputHandler
    {
        ConsoleKey keyPressed;
        ConsoleKey key1, key2, key3, key4;

        public InputHandler()
        {
            //Allowed input
            key1 = ConsoleKey.UpArrow;
            key2 = ConsoleKey.DownArrow;
            key3 = ConsoleKey.Enter;
            key4 = ConsoleKey.Spacebar;
        }
        // Asks for input until valid, then returns it
        public ConsoleKey ReadInput()
        {
            do
            {
                keyPressed = Console.ReadKey(true).Key;
            } while (keyPressed != key1 && keyPressed != key2 && keyPressed != key3 && keyPressed != key4);
            return keyPressed;
        }
    }
}