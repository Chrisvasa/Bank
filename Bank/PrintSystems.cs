using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class PrintSystem
    {
        string text;
        public PrintSystem()
        {
            text = File.ReadAllText(@"CreatorTitle.txt");
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
            DrawLength();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
            DrawLength();
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

        private void PrintTransaction()
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
                box += "#";
                Console.WriteLine(loadBars);
                if (i > 20)
                {
                    Thread.Sleep(30);
                }
                else
                {
                    Thread.Sleep(150);
                }
            }
            Console.WriteLine("Transaction completed. Great success!");
            Console.ReadLine();
        }
    }
}