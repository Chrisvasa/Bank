using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class PrintSystem
    {
        string text;
        // Reads in the welcome message from CreatorTitle.txt
        public PrintSystem()
        { 
            // Works for powershell and on windows, hopefully works on mac
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "CreatorTitle.txt")))
            {
                text = File.ReadAllText((Path.Combine(Directory.GetCurrentDirectory(), "CreatorTitle.txt")));

            }
            else if(File.Exists("../../../CreatorTitle.txt")) // Works on windows if ran from visual studio
            {
                text = File.ReadAllText("../../../CreatorTitle.txt");
            }
            else // Hopefully this works on mac if those above does not
            {
                text = File.ReadAllText("..\\..\\..\\CreatorTitle.txt");
            }
        }
        // Prints out the welcome message when called
        public void PrintWelcome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        // A simple function that prints out three dots after a given userInput
        // Also takes input on how many times to execute
        public static void Delay(string textInput, int timesToRun)
        {
            for(int i = 0; i < timesToRun; i++)
            {
                Console.Clear();
                Console.Write(textInput);
                for (int j = 0; j < 3; j++)
                {
                    Thread.Sleep(250);
                    Console.Write(".");
                }
                Thread.Sleep(250);
                Console.Clear();
            }
        }
        // Prints out a progress bar in the console when called for
        public static void PrintTransaction()
        {
            string loadBars = "------------------------------------";
            string box = "#";
            for (int i = 0; i < loadBars.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("Transaction in progress. Please hold.");
                Console.WriteLine(loadBars);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(box);
                Console.ResetColor();
                Console.WriteLine(loadBars);
                box += "#";
                if (i > 20)
                {
                    Thread.Sleep(30);
                }
                else
                {
                    Thread.Sleep(120);
                }
            }
            Console.WriteLine("Transaction completed. Great success!");
        }
    }
}