using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    // Class that handles the login logic
    public class Login
    {
        // Index of currently logged in user
        protected int userIndex = -1;
        public int UserIndex 
        { 
            get { return userIndex; } 
            set { userIndex = value; }
        }
        /*A method to handle user login
         *Takes the 2d array of users
         *First checks if username is on the list, and then prompts for the password
         *If password is correct, returns true - Else prompts user to try again and adds one to counter
         *Locks user out after three failed attempts */
        public bool UserLogin(string[,] users)
        {
            bool isTrue = true;
            int count = 0;
            do
            {
                Console.Clear();
                Console.Write("Enter your username: ");
                string userName = Console.ReadLine().ToUpper(); //toUpper
                for (int i = 0; i < users.Length / 2; i++)
                {
                    if (users[i, 0] == userName)
                    {
                        Console.Write("Password: ");
                        string password = Console.ReadLine();
                        if (users[i, 1] == password)
                        {
                            PrintSystem print = new PrintSystem();
                            print.Delay("Du loggas in", 3);
                            UserIndex = i;
                            isTrue = false;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password. Try again!");
                            count++;
                            Console.ReadKey();
                        }
                        if (count >= 3)
                        {
                            Console.WriteLine("Too many attempts have been made..");
                            Console.ReadKey();
                            isTrue = false;
                        }
                    }
                }
            } while (isTrue);
            return false;

        }
    }
}