using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
            int[] userLogIndex = new int[users.Length / 3]; // Array to keep track of login-attempts per user
            DateTime[] userTimers = new DateTime[userLogIndex.Length]; // Array to keep track of timeouts per user
            int userLog; // A variable used to check if username was found
            bool isTrue = true;
            
            do
            {
                string userName = "";
                while (String.IsNullOrEmpty(userName)) 
                {
                    Console.Clear();
                    Console.Write("Enter your username: ");
                    userName = Console.ReadLine().ToUpper();
                }
                userLog = 0;
                for (int i = 0; i < users.Length / 3; i++)
                {
                    if (users[i, 0] == userName)
                    {
                        userLog = 1; // To see if username exists
                        if (userLogIndex[i] >= 3)
                        {
                            bool login = TimeOut(i, userTimers); // Calls the method that handles timeouts
                            if(login)
                            {
                                userLogIndex[i] = 0;
                            }
                            else
                            {
                                Console.WriteLine("Try again later..");
                                Console.ReadKey();
                            }
                        }
                        if (userLogIndex[i] < 3)
                        {
                            Console.Write("Password: ");
                            string? password = Console.ReadLine();
                            if (users[i, 1] == password)
                            {
                                PrintSystem.Delay("You are being logged in", 3);
                                UserIndex = i; // Setter for the User Index to track which user has logged in
                                return true;
                            }
                            else
                            {
                                userLogIndex[i] += 1;
                                if (userLogIndex[i] != 3)
                                {
                                    Console.WriteLine("Wrong password. Try again!");
                                    Console.ReadKey();
                                }
                            }
                            if (userLogIndex[i] >= 3)
                            {
                                Console.WriteLine("Too many attempts have been made. Please try again in 3 minutes");
                                Console.ReadKey();
                            }
                        }
                    }
                }
                if(userLog == 0)
                {
                    Console.WriteLine("Username not found. Press any key to try again.");
                    Console.ReadKey();
                }
            } while (isTrue);
            return false;
        }

        // Method that handles the timeouts that users get after 3 failed login attempts
        // A DateTime array that handles different lockouts for different users
        // Once 3 minutes have passed, this returns true
        private static bool TimeOut(int userIndex, DateTime[] userTimers)
        {
            // Check if empty else set time once this method runs
            if (userTimers[userIndex] == new DateTime()) 
            {
                userTimers[userIndex] = DateTime.Now;
            }
            DateTime currentTime = DateTime.Now;
            TimeSpan timeRemaining = currentTime - userTimers[userIndex];
            switch(timeRemaining.Minutes)
            {
                case 0:
                    Console.WriteLine("3 minutes remaining..");
                    break;
                case 1:
                    Console.WriteLine("2 minutes remaining..");
                    break;
                case 2:
                    Console.WriteLine("1 minute remaining..");
                    break;
                default:
                    return true;
            }
            return false;
        }
    }
}