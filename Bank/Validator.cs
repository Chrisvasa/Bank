using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Validator
    {
        // Only accepts input with letters, else keeps asking
        public static bool TextValidator(string? input, string printMessage) 
        {
            do
            {
                Console.WriteLine(printMessage);
                input = Console.ReadLine();

            } while(input.Any(c => !char.IsLetter(c)));
            return true;
        }

        public static int PincodeValidator(string input, string printMessage)
        {
            bool success = int.TryParse(input, out int result);
            if (success)
            {
                return result;
            }
            else
            {
                return -1;
            }
        }
    }
}
