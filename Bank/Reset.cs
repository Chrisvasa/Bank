using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    // A class that allows the user to reset passwords of different users
    internal class Reset
    {
        string[,] users = new string[0,0];
        public Reset(User userList)
        {
            users = userList.Users;
        }

        // Allows the user to change the password of an existing account
        public void ResetPass()
        {
            Console.Write("Username: ");
            string anwser = Console.ReadLine().ToUpper();
            for (int i = 0; i < users.Length / 2; i++)
            {
                if (users[i,0] == anwser)
                {
                    Console.Write("Enter a new pincode: ");
                    bool success = int.TryParse(Console.ReadLine(), out int newPass);
                    if(success)
                    {
                        users[i, 1] = newPass.ToString();
                        Console.WriteLine("Pincode was changed! Press any key to continue.");
                    }
                    else
                    {
                        Console.WriteLine("Pincode must contain numbers only! Try again.");
                    }
                }
            }
        }
    }
}
