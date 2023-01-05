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
            int userLog;
            int[] userLogIndex = new int[users.Length / 2];
            do
            {
                Console.Clear();
                Console.Write("Enter your username: ");
                userLog = -1;
                string userName = Console.ReadLine().ToUpper();
                for (int i = 0; i < users.Length / 2; i++)
                {
                    if (users[i, 0] == userName)
                    {
                        userLog = i;
                        Console.Write("Password: ");
                        string password = Console.ReadLine();
                        if (users[i, 1] == password)
                        {
                            PrintSystem print = new PrintSystem();
                            print.Delay("Du loggas in", 3);
                            userLogIndex[i] = 0;
                            UserIndex = i;
                            isTrue = false;
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password. Try again!");
                            userLogIndex[i] += 1;
                            Console.ReadKey();
                        }
                        if (userLogIndex[i] >= 3)
                        {
                            Console.WriteLine("Too many attempts have been made..");
                            Console.ReadKey();
                            Environment.Exit(0);
                            isTrue = false;
                        }
                    }
                }
                if(userLog < 0)
                {
                    Console.WriteLine("Username not found. Press any key to try again.");
                    Console.ReadKey();
                }
            } while (isTrue);
            return false;

        }
    }
}