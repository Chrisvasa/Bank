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
    }
}