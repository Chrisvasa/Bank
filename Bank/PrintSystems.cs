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
        public PrintSystem()
        {
            if(File.Exists(".\\CreatorTitle.txt"))
            {
                text = File.ReadAllText(".\\CreatorTitle.txt");

            }
            else
            {
                text = File.ReadAllText("../../../CreatorTitle.txt");
            }
        }

        public PrintSystem(string Text)
        {
            text = Text;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public void Print()
        {
            //DrawLength();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
            //DrawLength();
        }
        // Gets text length and draws lines to match length of text
        private void DrawLength()
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
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